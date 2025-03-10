
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WeatherForecast.Domain.Entities.AccessLogs;
using WeatherForecast.Domain.Ports;
using WeatherForecast.Infrastructure.Persistence.AccessLogs.Mapper;
using WeatherForecast.Infrastructure.Persistence.DBConexion;

namespace WeatherForecast.Infrastructure.Persistence.AccessLogs.Repositories
{
    public class AccessLogsRepository : IAccessLogsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly AccessLogMapper _mapper;
        private readonly ILogger<AccessLogsRepository> _logger;

        public AccessLogsRepository(ApplicationDbContext context, AccessLogMapper mapper, ILogger<AccessLogsRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get all AccessLog
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<AccessLog>> GetAllAsync()
        {
            try
            {
                string sql = "SELECT * FROM AccessLogs";
                var accessLogEntities = await _context.AccessLogs
                    .FromSqlRaw(sql)
                    .ToListAsync();

                return accessLogEntities.Select(entity => _mapper.MapToDomain(entity)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los AccessLogs.");
                throw new Exception("Ocurrio un error al intentar obtener los registros de AccessLogs", ex);
            }
        }

        /// <summary>
        /// Get AccessLog by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<AccessLog?> GetByIdAsync(int id)
        {
            try
            {
                string sql = "SELECT * FROM AccessLogs WHERE AccessID = @AccessID";
                var parameter = new SqlParameter("@AccessID", id);

                var accessLogEntities = await _context.AccessLogs
                    .FromSqlRaw(sql, parameter)
                    .ToListAsync();

                return accessLogEntities.Select(entity => _mapper.MapToDomain(entity)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el AccessLog con ID {AccessID}", id);
                throw new Exception("Ocurrio un error al intentar obtener el AccessLog", ex);
            }
        }

        /// <summary>
        /// Create AccessLog 
        /// </summary>
        /// <param name="accessLog"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<AccessLog> AddAsync(AccessLog accessLog)
        {
            try
            {
                var entity = _mapper.MapToEntity(accessLog);

                _context.AccessLogs.Add(entity);
                await _context.SaveChangesAsync();

                return _mapper.MapToDomain(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar el AccessLog");
                throw new Exception("Ocurrió un error al agregar el AccessLog", ex);
            }
        }


        /// <summary>
        /// Delete AccessLog by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await _context.AccessLogs
                    .FirstOrDefaultAsync(a => a.AccessID == id);

                if (entity == null)
                    return false;

                _context.AccessLogs.Remove(entity);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el AccessLog con ID {AccessID}", id);
                throw new Exception("Ocurrio un error al eliminar el AccessLog", ex);
            }
        }
    }
}