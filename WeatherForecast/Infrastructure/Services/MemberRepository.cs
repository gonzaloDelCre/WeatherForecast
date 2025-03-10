using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WeatherForecast.Domain.Ports;
using WeatherForecast.Infrastructure.Persistence.DBConexion;
using WeatherForecast.Domain.Entities.Member;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using WeatherForecast.Domain.Entities.Shared;

namespace WeatherForecast.Infrastructure.Services
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MemberRepository> _logger;

        public MemberRepository(ApplicationDbContext context, ILogger<MemberRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Get All Members
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

                return dbMembers.Select(dbMember =>
                    new Member(
                        new MemberID(dbMember.MemberID),
                        new MemberFullName(dbMember.FullName),
                        new MemberEmail(dbMember.Email),
                        new MemberPhone(dbMember.Phone),
                        dbMember.JoinDate
                    )).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de miembros");
                return new List<Member>();
            }
        }

        /// <summary>
        /// Get Members by ID
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
                return dbMember != null ?
                    new Member(
                        new MemberID(dbMember.MemberID),
                        new MemberFullName(dbMember.FullName),
                        new MemberEmail(dbMember.Email),
                        new MemberPhone(dbMember.Phone),
                        dbMember.JoinDate
                    ) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el miembro con ID {MemberID}", id);
                return null;
            }
        }

        /// <summary>
        /// Get Member by Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<Member?> GetByEmailAsync(string email)
        {
            try
            {
                string sql = "SELECT * FROM Members WHERE Email = @Email";
                var parameter = new SqlParameter("@Email", email);

                var dbMembers = await _context.Members
                    .FromSqlRaw(sql, parameter)
                    .ToListAsync();

                var dbMember = dbMembers.FirstOrDefault();
                return dbMember != null ?
                    new Member(
                        new MemberID(dbMember.MemberID),
                        new MemberFullName(dbMember.FullName),
                        new MemberEmail(dbMember.Email),
                        new MemberPhone(dbMember.Phone),
                        dbMember.JoinDate
                    ) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el miembro con email {Email}", email);
                return null;
            }
        }

        /// <summary>
        /// Create Member
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
                string insertSql = @"INSERT INTO Members (FullName, Email, Phone, JoinDate) 
                             VALUES (@FullName, @Email, @Phone, @JoinDate);";

                var parameters = new[]
                {
                    new SqlParameter("@FullName", member.FullName.Value),
                    new SqlParameter("@Email", member.Email.Value),
                    new SqlParameter("@Phone", (object?)member.Phone?.Value ?? DBNull.Value),
                    new SqlParameter("@JoinDate", member.JoinDate)
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
        /// Delete Member
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
