using WeatherForecast.Domain.Entities.AccessLog;

namespace WeatherForecast.Domain.Ports
{
    public interface IAccessLogsRepository
    {
        Task<IEnumerable<AccessLog>> GetAllAsync();
        Task<AccessLog?> GetByIdAsync(int id);
        Task<AccessLog> AddAsync(AccessLog accessLog);
        Task<bool> DeleteAsync(int id);
    }

}
