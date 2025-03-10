using WeatherForecast.Domain.Entities.AccessLogs;
using WeatherForecast.Domain.Ports;

namespace WeatherForecast.Application.AccessLogs.UseCases
{
    public class GetAllAccessLogsUseCase
    {
        private readonly IAccessLogsRepository _accessLogsRepository;

        public GetAllAccessLogsUseCase(IAccessLogsRepository accessLogsRepository)
        {
            _accessLogsRepository = accessLogsRepository;
        }

        public async Task<IEnumerable<AccessLog>> ExecuteAsync()
        {
            return await _accessLogsRepository.GetAllAsync();
        }
    }
}
