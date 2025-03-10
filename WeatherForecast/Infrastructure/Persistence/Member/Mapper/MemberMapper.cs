using WeatherForecast.Domain.Entities.Member;
using WeatherForecast.Domain.Entities.Shared;
using WeatherForecast.Infrastructure.Persistence.Member.Entities;

namespace WeatherForecast.Infrastructure.Persistence.Member.Mapper
{
    public class MemberMapper
    {
        public Domain.Entities.Member.Member MapToDomain(MemberEntity entity)
        {
            return new Domain.Entities.Member.Member(
                new MemberID(entity.MemberID),
                new MemberFullName(entity.FullName),
                new MemberEmail(entity.Email),
                new MemberPhone(entity.Phone),
                entity.JoinDate
            );
        }

        public MemberEntity MapToEntity(Domain.Entities.Member.Member domain)
        {
            return new MemberEntity
            {
                FullName = domain.FullName.Value,
                Email = domain.Email.Value,
                Phone = domain.Phone?.Value,
                JoinDate = domain.JoinDate
            };
        }
    }
}
