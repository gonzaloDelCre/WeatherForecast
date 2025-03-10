using WeatherForecast.Domain.Entities.AccessLog;
using WeatherForecast.Domain.Entities.Shared;
using WeatherForecast.Infrastructure.Persistence.AccessLog.Entities;

namespace WeatherForecast.Infrastructure.Persistence.AccessLog.Mapper
{
    public class AccessLogMapper
    {
        public Domain.Entities.AccessLog.AccessLog MapToDomain(AccessLogEntity entity)
        {
            return new Domain.Entities.AccessLog.AccessLog(
                entity.MemberID.HasValue ? new MemberID(entity.MemberID.Value) : null,
                new AccessDateTime(entity.AccessDateTime),
                new AccessType(entity.AccessType ?? string.Empty),
                new AccessStatus(entity.AccessStatus ?? string.Empty)
            );
        }

        public AccessLogEntity MapToEntity(Domain.Entities.AccessLog.AccessLog domainEntity)
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
