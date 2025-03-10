using WeatherForecast.Application.AccessLogs.DTOs;
using WeatherForecast.Application.AccessLogs.Mapper;
using WeatherForecast.Domain.Entities.AccessLogs;
using WeatherForecast.Domain.Ports;

namespace WeatherForecast.Application.AccessLogs.UseCases
{
    public class AccessErrorUseCase
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ILogger<AccessErrorUseCase> _logger;

        public AccessErrorUseCase(IMemberRepository memberRepository, ILogger<AccessErrorUseCase> logger)
        {
            _memberRepository = memberRepository;
            _logger = logger;
        }


        public async Task<AccessLogDTO> HandleErrorAsync(AccessLogDTO accessLogDTO)
        {
            var accessLog = AccessLogMapper.ToDomain(accessLogDTO);

            if (accessLog.MemberID == null)
            {
                _logger.LogWarning("El ID de miembro no fue proporcionado");
                UpdateAccessLogForError(accessLog);

                return AccessLogMapper.ToDTO(accessLog);
            }

            var memberExists = await _memberRepository.GetByIdAsync(accessLog.MemberID.Value);
            if (memberExists == null)
            {
                _logger.LogWarning("Miembro con el ID {MemberID} no existe");
                UpdateAccessLogForError(accessLog);

                return AccessLogMapper.ToDTO(accessLog);
            }

            return accessLogDTO;
        }


        private void UpdateAccessLogForError(AccessLog accessLog)
        {
            accessLog.UpdateAccessType(new AccessType("ERROR"));
            accessLog.UpdateAccessStatus(new AccessStatus("UNKNOWN"));
            accessLog.UpdateMemberID(null);  
        }

    }
}
