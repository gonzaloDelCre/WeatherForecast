using WeatherForecast.Domain.Ports;
using System.Threading.Tasks;
using WeatherForecast.Domain.Entities.AccessLogs;

namespace WeatherForecast.Application.AccessLogs.UseCases
{
    public class GetAccessLogByIdUseCase
    {
        private readonly IAccessLogsRepository _accessLogsRepository;

        public GetAccessLogByIdUseCase(IAccessLogsRepository accessLogsRepository)
        {
            _accessLogsRepository = accessLogsRepository;
        }

        public async Task<AccessLog> ExecuteAsync(int id)
        {
            return await _accessLogsRepository.GetByIdAsync(id);
        }
    }
}
