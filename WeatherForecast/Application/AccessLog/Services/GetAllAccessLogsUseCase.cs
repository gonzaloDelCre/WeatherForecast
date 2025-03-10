using WeatherForecast.Domain.Ports;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherForecast.Application.AccessLog.UseCases
{
    public class GetAllAccessLogsUseCase
    {
        private readonly IAccessLogsRepository _accessLogsRepository;

        public GetAllAccessLogsUseCase(IAccessLogsRepository accessLogsRepository)
        {
            _accessLogsRepository = accessLogsRepository;
        }

        public async Task<IEnumerable<Domain.Entities.AccessLog.AccessLog>> ExecuteAsync()
        {
            return await _accessLogsRepository.GetAllAsync();
        }
    }
}
