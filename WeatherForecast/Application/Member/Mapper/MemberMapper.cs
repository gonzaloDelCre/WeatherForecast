using WeatherForecast.Application.Member.DTOs;
using WeatherForecast.Domain.Entities.Member;

namespace WeatherForecast.Application.Member.Mapper
{
    public static class MemberMapper
    {
        //Transform to Domain
        public static WeatherForecast.Domain.Entities.Member.Member ToDomain(MemberDTO memberDTO)
        {
            var memberId = new WeatherForecast.Domain.Entities.Shared.MemberID(memberDTO.MemberID);
            var fullName = new MemberFullName(memberDTO.FullName);
            var email = new MemberEmail(memberDTO.Email);
            var phone = new MemberPhone(memberDTO.Phone);
            var joinDate = memberDTO.JoinDate;

            return new WeatherForecast.Domain.Entities.Member.Member(
                memberId,
                fullName,
                email,
                phone,
                joinDate
            );
        }

        //Transform to DTO
        public static MemberDTO ToDTO(WeatherForecast.Domain.Entities.Member.Member member)
        {
            return new MemberDTO
            {
                MemberID = member.MemberID?.Value ?? 0, 
                FullName = member.FullName.Value,
                Email = member.Email.Value,
                Phone = member.Phone?.Value,  
                JoinDate = member.JoinDate
            };
        }

       
    }
}
