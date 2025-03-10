using WeatherForecast.Application.Member.DTOs;
using WeatherForecast.Domain.Ports;
using System.Threading.Tasks;

namespace WeatherForecast.Application.Member.UseCases
{
    public class DeleteMemberByIdUseCase
    {
        private readonly IMemberRepository _memberRepository;

        public DeleteMemberByIdUseCase(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<bool> ExecuteAsync(int memberId)
        {
            return await _memberRepository.DeleteAsync(memberId);
        }
    }
}
