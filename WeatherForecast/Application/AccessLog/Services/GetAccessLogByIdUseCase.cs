using WeatherForecast.Domain.Ports;
using System.Threading.Tasks;

namespace WeatherForecast.Application.AccessLog.UseCases
{
    public class GetAccessLogByIdUseCase
    {
        private readonly IAccessLogsRepository _accessLogsRepository;

        public GetAccessLogByIdUseCase(IAccessLogsRepository accessLogsRepository)
        {
            _accessLogsRepository = accessLogsRepository;
        }

        public async Task<Domain.Entities.AccessLog.AccessLog> ExecuteAsync(int id)
        {
            return await _accessLogsRepository.GetByIdAsync(id);
        }
    }
}
