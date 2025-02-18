using Microsoft.AspNetCore.Mvc;
using TechLibrary.Cases.Users.Register;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;

namespace TechLibrary.Api.Controllers  {
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
        public IActionResult Create(RequestUserJson request) {
            RegisterUserUseCase useCase = new RegisterUserUseCase();
            ResponseRegisteredUserJson response = useCase.Execute(request);
            return this.Created(string.Empty, response);
        }

        [HttpGet]
        public IActionResult Get() {
            return this.Ok();
        }
    }
}
