using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherForecast.Application.Member.UseCases;
using WeatherForecast.Application.Member.DTOs;
using WeatherForecast.Application.Member.Mapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherForecast.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly ILogger<MemberController> _logger;
        private readonly CreateMemberUseCase _createMemberUseCase;
        private readonly GetMemberByIdUseCase _getMemberByIdUseCase;
        private readonly GetAllMembersUseCase _getAllMembersUseCase;
        private readonly DeleteMemberByIdUseCase _deleteMemberByIdUseCase;

        public MemberController(
            ILogger<MemberController> logger,
            CreateMemberUseCase createMemberUseCase,
            GetMemberByIdUseCase getMemberByIdUseCase,
            GetAllMembersUseCase getAllMembersUseCase,
            DeleteMemberByIdUseCase deleteMemberByIdUseCase)
        {
            _logger = logger;
            _createMemberUseCase = createMemberUseCase;
            _getMemberByIdUseCase = getMemberByIdUseCase;
            _getAllMembersUseCase = getAllMembersUseCase;
            _deleteMemberByIdUseCase = deleteMemberByIdUseCase;
        }

        /// <summary>
        /// Create member
        /// </summary>
        /// <param name="memberDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MemberDTO memberDTO)
        {
            if (memberDTO == null)
                return BadRequest("Datos del miembro inválidos.");

            var member = MemberMapper.ToDomain(memberDTO);
            var newMember = await _createMemberUseCase.ExecuteAsync(member);

            if (newMember == null)
                return Conflict($"El miembro con el correo {member.Email.Value} ya existe");

            if (newMember.MemberID != null)
            {
                return CreatedAtAction(nameof(GetById), new { id = newMember.MemberID }, newMember);
            }
            else
            {
                return BadRequest("El ID del miembro no es valido");
            }
        }

        /// <summary>
        /// Get all members
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDTO>>> Get()
        {
            var members = await _getAllMembersUseCase.ExecuteAsync();
            return Ok(members); 
        }

        /// <summary>
        /// Get member by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<MemberDTO>> GetById(int id)
        {
            var memberDTO = await _getMemberByIdUseCase.ExecuteAsync(id);
            if (memberDTO == null)
                return NotFound($"No se encontro el miembro con ID {id}");
            return Ok(memberDTO);  
        }

        /// <summary>
        /// Delete member by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await _deleteMemberByIdUseCase.ExecuteAsync(id);
            if (!deleted)
                return NotFound($"No se pudo eliminar el miembro con ID {id}");

            return NoContent();
        }
    }
}
