using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WeatherForecast.Domain.Ports;
using WeatherForecast.Infrastructure.Persistence.DBConexion;
using WeatherForecast.Domain.Entities.Shared;
using WeatherForecast.Infrastructure.Persistence.Members.Mapper;
using WeatherForecast.Domain.Entities.Members;

namespace WeatherForecast.Infrastructure.Persistence.Members.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MemberRepository> _logger;
        private readonly MemberMapper _mapper;

        public MemberRepository(ApplicationDbContext context, ILogger<MemberRepository> logger, MemberMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get All member
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Member>> GetAllAsync()
        {
            try
            {
                string sql = "SELECT * FROM Members";
                var dbMembers = await _context.Members
                    .FromSqlRaw(sql)
                    .ToListAsync();

                return dbMembers.Select(dbMember => _mapper.MapToDomain(dbMember)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de miembros");
                return new List<Member>();
            }
        }

        /// <summary>
        /// Get member by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Member?> GetByIdAsync(int id)
        {
            try
            {
                string sql = "SELECT * FROM Members WHERE MemberID = @MemberID";
                var parameter = new SqlParameter("@MemberID", id);

                var dbMembers = await _context.Members
                    .FromSqlRaw(sql, parameter)
                    .ToListAsync();

                var dbMember = dbMembers.FirstOrDefault();
                return dbMember != null ? _mapper.MapToDomain(dbMember) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el miembro con ID {MemberID}", id);
                return null;
            }
        }

        /// <summary>
        /// Create member 
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<Member> AddAsync(Member member)
        {
            if (member == null)
            {
                throw new ArgumentNullException(nameof(member), "El miembro no puede ser null");
            }

            try
            {
                var memberEntity = _mapper.MapToEntity(member);

                string insertSql = @"INSERT INTO Members (FullName, Email, Phone, JoinDate) 
                             VALUES (@FullName, @Email, @Phone, @JoinDate);";

                var parameters = new[]
                {
                    new SqlParameter("@FullName", memberEntity.FullName),
                    new SqlParameter("@Email", memberEntity.Email),
                    new SqlParameter("@Phone", (object?)memberEntity.Phone ?? DBNull.Value),
                    new SqlParameter("@JoinDate", memberEntity.JoinDate)
                };

                await _context.Database.ExecuteSqlRawAsync(insertSql, parameters);

                string selectSql = "SELECT TOP 1 MemberID FROM Members ORDER BY MemberID DESC";
                var newMemberId = await _context.Members
                    .FromSqlRaw(selectSql)
                    .Select(m => m.MemberID)
                    .FirstOrDefaultAsync();

                return new Member(
                    new MemberID(newMemberId),
                    member.FullName,
                    member.Email,
                    member.Phone,
                    member.JoinDate
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar un nuevo miembro");
                throw;
            }
        }


        /// <summary>
        /// Delete member by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                string sql = "DELETE FROM Members WHERE MemberID = @MemberID";
                var parameter = new SqlParameter("@MemberID", id);

                int rowsAffected = await _context.Database.ExecuteSqlRawAsync(sql, parameter);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el miembro con ID {MemberID}", id);
                return false;
            }
        }
    }
}
