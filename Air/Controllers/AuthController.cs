using Air.Controllers.Base;
using Air.Data.Models.User;
using Air.Data.Models.UserSession;
using Auth.Data.Interfaces.Services;
using Exceptions.Data;
using Microsoft.AspNetCore.Mvc;

namespace Air.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : BaseController
{

    private readonly IUserService _userService;
    
    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Авторизация по логину и паролю
    /// </summary>
    /// <param name="model">Модель авторизации</param>
    /// <returns>Объект сессии</returns>
    /// <response code="200">Успешная авторизация</response>
    /// <response code="401">Ошибка при авторизации</response>
    /// <response code="500">Ошибка на стороне сервера</response>
    [HttpPost]
    [Route("login/credentials")]
    [ProducesResponseType(typeof(UserSessionModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<IActionResult> LoginByCredentials(UserLoginByCredentialsModel model)
    {
        var session = await _userService.LoginByCredentials(model.ToDto());
        return Ok(UserSessionModel.FromDto(session));
    }

    /// <summary>
    /// Авторизация по токену сессии
    /// </summary>
    /// <param name="model">Модель авторизации</param>
    /// <returns>Объект сессии</returns>
    /// <response code="200">Успешная авторизация</response>
    /// <response code="401">Ошибка при авторизации</response>
    /// <response code="500">Ошибка на стороне сервера</response>
    [HttpPost]
    [Route("login/token")]
    [ProducesResponseType(typeof(UserSessionModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<IActionResult> LoginByToken(UserLoginByTokenModel model)
    {
        var session = _userService.LoginByToken(model.ToDto());
        return Ok(UserSessionModel.FromDto(session));
    }
    
}