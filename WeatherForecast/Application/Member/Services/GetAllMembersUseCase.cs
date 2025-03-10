using WeatherForecast.Application.Member.DTOs;
using WeatherForecast.Domain.Ports;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Application.Member.Mapper;

namespace WeatherForecast.Application.Member.UseCases
{
    public class GetAllMembersUseCase
    {
        private readonly IMemberRepository _memberRepository;

        public GetAllMembersUseCase(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<IEnumerable<MemberDTO>> ExecuteAsync()
        {
            var members = await _memberRepository.GetAllAsync();
            return members.Select(MemberMapper.ToDTO);  
        }
    }
}
