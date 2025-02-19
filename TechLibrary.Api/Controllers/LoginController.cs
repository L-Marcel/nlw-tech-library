using Microsoft.AspNetCore.Mvc;
using TechLibrary.Cases.Login;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Exceptions;

namespace TechLibrary.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status401Unauthorized)]
        public IActionResult DoLogin(RequestLoginJson request) {
            DoLoginUseCase useCase = new DoLoginUseCase();
            ResponseRegisteredUserJson response = useCase.Execute(request);
            return this.Ok(response);
        }
    }
}
