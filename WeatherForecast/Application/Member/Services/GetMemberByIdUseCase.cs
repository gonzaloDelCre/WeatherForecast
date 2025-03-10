using WeatherForecast.Application.Member.DTOs;
using WeatherForecast.Domain.Ports;
using System.Threading.Tasks;
using WeatherForecast.Application.Member.Mapper;

namespace WeatherForecast.Application.Member.UseCases
{
    public class GetMemberByIdUseCase
    {
        private readonly IMemberRepository _memberRepository;

        public GetMemberByIdUseCase(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<MemberDTO> ExecuteAsync(int memberId)
        {
            var member = await _memberRepository.GetByIdAsync(memberId);
            return member != null ? MemberMapper.ToDTO(member) : null;
        }
    }
}
