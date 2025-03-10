using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Application.AccessLog.UseCases;
using WeatherForecast.Application.AccessLog.DTOs;
using WeatherForecast.Application.AccessLog.Mapper;
using WeatherForecast.Domain.Ports;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class AccessLogsController : ControllerBase
{
    private readonly ILogger<AccessLogsController> _logger;
    private readonly CreateAccessLogUseCase _createAccessLogUseCase;
    private readonly GetAccessLogByIdUseCase _getAccessLogByIdUseCase;
    private readonly GetAllAccessLogsUseCase _getAllAccessLogsUseCase;
    private readonly DeleteAccessLogByIdUseCase _deleteAccessLogByIdUseCase;
    private readonly AccessErrorUseCase _accessErrorUseCase;

    public AccessLogsController(
        ILogger<AccessLogsController> logger,
        CreateAccessLogUseCase createAccessLogUseCase,
        GetAccessLogByIdUseCase getAccessLogByIdUseCase,
        GetAllAccessLogsUseCase getAllAccessLogsUseCase,
        DeleteAccessLogByIdUseCase deleteAccessLogByIdUseCase,
        AccessErrorUseCase accessErrorUseCase)
    {
        _logger = logger;
        _createAccessLogUseCase = createAccessLogUseCase;
        _getAccessLogByIdUseCase = getAccessLogByIdUseCase;
        _getAllAccessLogsUseCase = getAllAccessLogsUseCase;
        _deleteAccessLogByIdUseCase = deleteAccessLogByIdUseCase;
        _accessErrorUseCase = accessErrorUseCase;
    }

    /// <summary>
    /// Create accesslog 
    /// </summary>
    /// <param name="accessLogDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] AccessLogDTO accessLogDto)
    {
        if (accessLogDto == null)
            return BadRequest("Datos de acceso invalidos");

        var accessLogHandled = await _accessErrorUseCase.HandleErrorAsync(accessLogDto);

        if (accessLogHandled.AccessStatus == "UNKNOWN" || accessLogHandled.AccessType == "ERROR")
        {
            if (accessLogHandled.AccessStatus == "UNKNOWN")
            {
                return BadRequest("Error: El ID de miembro no fue proporcionado");
            }

            if (accessLogHandled.AccessType == "ERROR")
            {
                return BadRequest("Error al procesar el acceso: " + accessLogHandled.AccessStatus);
            }
        }

        var createdLog = await _createAccessLogUseCase.ExecuteAsync(accessLogDto);
        var createdDto = AccessLogMapper.ToDTO(createdLog);

        return CreatedAtAction(nameof(GetById), new { id = createdDto.AccessID }, createdDto);
    }

    /// <summary>
    /// Get access by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<AccessLogDTO>> GetById(int id)
    {
        var log = await _getAccessLogByIdUseCase.ExecuteAsync(id);
        if (log == null)
            return NotFound("No se encontro el AccessLog con ese ID");

        return Ok(AccessLogMapper.ToDTO(log));
    }

    /// <summary>
    /// Get all access
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AccessLogDTO>>> Get()
    {
        var logs = await _getAllAccessLogsUseCase.ExecuteAsync();
        return Ok(logs.Select(AccessLogMapper.ToDTO));
    }

    /// <summary>
    /// Delete by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        bool deleted = await _deleteAccessLogByIdUseCase.ExecuteAsync(id);
        if (!deleted)
            return NotFound("No se pudo eliminar el acceso con ese ID");

        return NoContent();
    }
}
