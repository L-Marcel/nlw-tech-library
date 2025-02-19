using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechLibrary.Cases.Checkouts.Register;
using TechLibrary.Communication.Responses;

namespace TechLibrary.Api.Controllers  {
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutsController : ControllerBase {
        [HttpPost]
        [Route("{bookId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status423Locked)]
        public IActionResult BookCheckout(Guid bookId) {
            RegisterBookCheckoutUseCase useCase = new RegisterBookCheckoutUseCase();
            useCase.Execute(bookId, this.HttpContext);
            return this.Ok();
        }
    }
}
