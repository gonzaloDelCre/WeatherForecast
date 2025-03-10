using WeatherForecast.Application.Member.DTOs;
using WeatherForecast.Application.Member.Mapper;
using WeatherForecast.Domain.Entities.Member;
using WeatherForecast.Domain.Entities.Shared;
using WeatherForecast.Domain.Ports;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace WeatherForecast.Application.Member.UseCases
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

        public async Task<MemberDTO> ExecuteAsync(WeatherForecast.Domain.Entities.Member.Member member)
        {
            if (member.Email?.Value != null)
            {
                var existingMember = await _memberRepository.GetByEmailAsync(member.Email.Value);
                if (existingMember != null)
                {
                    _logger.LogWarning("El miembro con el correo {Email} ya existe", member.Email.Value);
                    return null;
                }
            }

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
