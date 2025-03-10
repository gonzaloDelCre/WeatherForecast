using WeatherForecast.Domain.Entities.AccessLogs;
using WeatherForecast.Domain.Entities.Shared;
using WeatherForecast.Infrastructure.Persistence.AccessLogs.Entities;

namespace WeatherForecast.Infrastructure.Persistence.AccessLogs.Mapper
{
    public class AccessLogMapper
    {
        public AccessLog MapToDomain(AccessLogEntity entity)
        {
            return new AccessLog(
                entity.MemberID.HasValue ? new MemberID(entity.MemberID.Value) : null,
                new AccessDateTime(entity.AccessDateTime),
                new AccessType(entity.AccessType ?? string.Empty),
                new AccessStatus(entity.AccessStatus ?? string.Empty)
            );
        }

        public AccessLogEntity MapToEntity(AccessLog domainEntity)
        {
            return new AccessLogEntity
            {
                MemberID = domainEntity.MemberID?.Value,
                AccessDateTime = domainEntity.AccessDateTime.Value,
                AccessType = domainEntity.AccessType.Value,
                AccessStatus = domainEntity.AccessStatus.Value
            };
        }
    }
}
