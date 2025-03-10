using WeatherForecast.Application.Members.DTOs;
using WeatherForecast.Application.Members.Mapper;
using WeatherForecast.Domain.Entities.Members;
using WeatherForecast.Domain.Ports;

namespace WeatherForecast.Application.Members.UseCases
{
    public class CreateMemberUseCase
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ILogger<CreateMemberUseCase> _logger;

        public CreateMemberUseCase(IMemberRepository memberRepository, ILogger<CreateMemberUseCase> logger)
        {
            _memberRepository = memberRepository;
            _logger = logger;
        }

        public async Task<MemberDTO> ExecuteAsync(Member member)
        {
            var newMember = await _memberRepository.AddAsync(member);
            _logger.LogInformation("Nuevo miembro creado: {@Member}", newMember);

            if (newMember.MemberID.Value != null)
            {
                return MemberMapper.ToDTO(newMember);
            }
            else
            {
                _logger.LogError("El ID del nuevo miembro es null");
                return null;
            }
        }
    }
}
