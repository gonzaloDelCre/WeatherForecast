using WeatherForecast.Domain.Ports;

using System.Threading.Tasks;

namespace WeatherForecast.Application.AccessLogs.UseCases
{
    public class DeleteAccessLogByIdUseCase
    {
        private readonly IAccessLogsRepository _accessLogsRepository;

        public DeleteAccessLogByIdUseCase(IAccessLogsRepository accessLogsRepository)
        {
            _accessLogsRepository = accessLogsRepository;
        }

        public async Task<bool> ExecuteAsync(int id)
        {
            return await _accessLogsRepository.DeleteAsync(id);
        }
    }
}
