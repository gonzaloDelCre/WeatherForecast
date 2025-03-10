using WeatherForecast.Application.AccessLogs.DTOs;
using WeatherForecast.Domain.Entities.AccessLogs;
using WeatherForecast.Domain.Entities.Shared;
using WeatherForecast.Infrastructure.Persistence.AccessLogs.Entities;

namespace WeatherForecast.Application.AccessLogs.Mapper
{
    public static class AccessLogMapper
    {
        //Transform to DTO
        public static AccessLogDTO ToDTO(AccessLog accessLog)
        {
            if (accessLog == null)
            {
                throw new ArgumentNullException(nameof(accessLog), "El objeto AccessLog no puede ser null");
            }

            return new AccessLogDTO
            {
                AccessID = accessLog.AccessID?.Value ?? 0,
                MemberID = accessLog.MemberID?.Value,
                AccessDateTime = accessLog.AccessDateTime?.Value ?? DateTime.MinValue,
                AccessType = accessLog.AccessType?.Value ?? string.Empty,
                AccessStatus = accessLog.AccessStatus?.Value ?? string.Empty
            };
        }

        //Transform to Domain
        public static AccessLog ToDomain(AccessLogDTO dto)
        {
            var accessDateTime = dto.AccessDateTime ?? DateTime.Now;

            var accessType = dto.AccessType ?? "ERROR"; 
            var accessStatus = dto.AccessStatus ?? "Acceso fallido"; 

            var memberId = dto.MemberID.HasValue ? new MemberID(dto.MemberID.Value) : (MemberID?)null;

            return new AccessLog(
                memberId,
                new AccessDateTime(accessDateTime),
                new AccessType(accessType),
                new AccessStatus(accessStatus)
            );
        }

        //Transform to Persistence Entity
        public static AccessLogEntity ToPersistence(AccessLog domainEntity)
        {
            if (domainEntity == null)
            {
                throw new ArgumentNullException(nameof(domainEntity), "El objeto AccessLog no puede ser null");
            }

            return new AccessLogEntity
            {
                AccessID = domainEntity.AccessID?.Value ?? 0,
                MemberID = domainEntity.MemberID?.Value,
                AccessDateTime = domainEntity.AccessDateTime?.Value ?? DateTime.MinValue,
                AccessType = domainEntity.AccessType?.Value ?? string.Empty,
                AccessStatus = domainEntity.AccessStatus?.Value ?? string.Empty
            };
        }
    }
}
