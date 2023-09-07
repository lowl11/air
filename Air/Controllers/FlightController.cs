using Air.Controllers.Base;
using Air.Data.Models.Flight;
using Aircraft.Data.Interfaces.Services;
using Auth.Data.Enums;
using Auth.Services;
using Exceptions.Data;
using Microsoft.AspNetCore.Mvc;

namespace Air.Controllers;

[ApiController]
[Route("api/v1/flight")]
public class FlightController : BaseController
{

    private readonly IFlightService _flightService;
    private readonly UserAccessService _accessService;
    
    public FlightController(IFlightService flightService, UserAccessService accessService)
    {
        _flightService = flightService;
        _accessService = accessService;
    }

    /// <summary>
    /// Получение списка полетов
    /// </summary>
    /// <param name="origin">Место вылета</param>
    /// <param name="destination">Место прибытия</param>
    /// <returns>Список полетов</returns>
    /// <response code="200">Успешное получение списка</response>
    /// <response code="500">Ошибка на стороне сервера</response>
    [HttpGet]
    [Route("get")]
    [ProducesResponseType(typeof(List<FlightModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public async Task<IActionResult> Get(string? origin = null, string? destination = null)
    {
        var flights = await _flightService.Get(origin, destination);
        return Ok(flights.Select(FlightModel.FromDto).ToList());
    }

    /// <summary>
    /// Создание нового рейса
    /// </summary>
    /// <param name="model">Модель создания рейса</param>
    /// <returns></returns>
    /// <response code="200">Успешное создание рейса</response>
    /// <response code="500">Ошибка на стороне сервера</response>
    [HttpPost]
    [Route("add")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
    [Consumes("application/json")]
    public async Task<IActionResult> Add(FlightAddModel model)
    {
        _accessService.Check(HttpContext, Role.Moderator);
        var newId = await _flightService.Add(model.ToDto());
        return Created(newId);
    }

    /// <summary>
    /// Изменение статуса рейса
    /// </summary>
    /// <param name="id">ID рейса</param>
    /// <param name="model">Модель изменения статуса</param>
    /// <returns></returns>
    /// <response code="200">Успешное изменение статуса рейса</response>
    /// <response code="404">Рейс не найден</response>
    /// <response code="500">Ошибка на стороне сервера</response>
    [HttpPut]
    [Route("update-status/{id:int:required}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
    [Consumes("application/json")]
    public async Task<IActionResult> UpdateStatus(int id, FlightUpdateStatusModel model)
    {
        _accessService.Check(HttpContext, Role.Moderator);
        await _flightService.UpdateStatus(id, model.ToDto());
        return Ok();
    }
    
}