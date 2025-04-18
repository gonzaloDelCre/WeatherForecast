﻿
using WeatherForecast.Application.AccessLogs.DTOs;
using WeatherForecast.Application.AccessLogs.Mapper;
using WeatherForecast.Domain.Entities.AccessLogs;
using WeatherForecast.Domain.Ports;

namespace WeatherForecast.Application.AccessLogs.UseCases
{
    public class CreateAccessLogUseCase
    {
        private readonly IAccessLogsRepository _accessLogsRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly ILogger<CreateAccessLogUseCase> _logger;

        public CreateAccessLogUseCase(IAccessLogsRepository accessLogsRepository, IMemberRepository memberRepository, ILogger<CreateAccessLogUseCase> logger)
        {
            _accessLogsRepository = accessLogsRepository;
            _memberRepository = memberRepository;
            _logger = logger;
        }

        public async Task<AccessLog> ExecuteAsync(AccessLogDTO accessLogDTO)
        {
            var accessLog = AccessLogMapper.ToDomain(accessLogDTO);

            if (accessLog.MemberID.Value != null)
            {
                var memberExists = await _memberRepository.GetByIdAsync(accessLog.MemberID.Value);
                if (memberExists == null)
                {
                    _logger.LogWarning("Miembro con ID {MemberID} no encontrado", accessLog.MemberID.Value);
                    accessLog.UpdateAccessType(new AccessType("ERROR"));
                    accessLog.UpdateMemberID(null);
                }
            }

            if (string.IsNullOrEmpty(accessLog.AccessStatus.Value))
            {
                accessLog.UpdateAccessStatus(new AccessStatus("UNKNOWN"));
            }

            var newLog = await _accessLogsRepository.AddAsync(accessLog);

            _logger.LogInformation("Nuevo log de acceso creado: {@AccessLog}", newLog);

            return newLog;
        }
    }
}
