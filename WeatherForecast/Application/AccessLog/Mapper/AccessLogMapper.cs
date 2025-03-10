using WeatherForecast.Application.AccessLog.DTOs;
using WeatherForecast.Domain.Entities.Shared;
using WeatherForecast.Infrastructure.Persistence.AccessLog.Entities;

namespace WeatherForecast.Application.AccessLog.Mapper
{
    public static class AccessLogMapper
    {
        //Transform to DTO
        public static AccessLogDTO ToDTO(Domain.Entities.AccessLog.AccessLog accessLog)
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
        public static Domain.Entities.AccessLog.AccessLog ToDomain(AccessLogDTO dto)
        {
            var accessDateTime = dto.AccessDateTime ?? DateTime.Now;

            var accessType = dto.AccessType ?? "ERROR"; 
            var accessStatus = dto.AccessStatus ?? "Acceso fallido"; 

            var memberId = dto.MemberID.HasValue ? new MemberID(dto.MemberID.Value) : (MemberID?)null;

            return new Domain.Entities.AccessLog.AccessLog(
                memberId,
                new Domain.Entities.AccessLog.AccessDateTime(accessDateTime),
                new Domain.Entities.AccessLog.AccessType(accessType),
                new Domain.Entities.AccessLog.AccessStatus(accessStatus)
            );
        }

        //Transform to Persistence Entity
        public static AccessLogEntity ToPersistence(Domain.Entities.AccessLog.AccessLog domainEntity)
        {
            if (domainEntity == null)
            {
                throw new ArgumentNullException(nameof(domainEntity), "El objeto AccessLog no puede ser null");
            }

            return new Infrastructure.Persistence.AccessLog.Entities.AccessLogEntity
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
