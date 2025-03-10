using WeatherForecast.Domain.Entities.Members;
using WeatherForecast.Domain.Entities.Shared;
using WeatherForecast.Infrastructure.Persistence.Members.Entities;

namespace WeatherForecast.Infrastructure.Persistence.Members.Mapper
{
    public class MemberMapper
    {
        public Member MapToDomain(MemberEntity entity)
        {
            return new Member(
                new MemberID(entity.MemberID),
                new MemberFullName(entity.FullName),
                new MemberEmail(entity.Email),
                new MemberPhone(entity.Phone),
                entity.JoinDate
            );
        }

        public MemberEntity MapToEntity(Member domain)
        {
            return new MemberEntity
            {
                MemberID = domain.MemberID.Value,
                FullName = domain.FullName.Value,
                Email = domain.Email.Value,
                Phone = domain.Phone?.Value, 
                JoinDate = domain.JoinDate
            };
        }
    }
}
