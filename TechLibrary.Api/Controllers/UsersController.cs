using Microsoft.AspNetCore.Mvc;
using TechLibrary.Cases.Users.Register;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Exceptions;

namespace TechLibrary.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase {
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status400BadRequest)]
    public IActionResult Register(RequestUserJson request) {
        RegisterUserUseCase useCase = new();
        ResponseRegisteredUserJson response = useCase.Execute(request);
        return this.Created(string.Empty, response);
    }
}