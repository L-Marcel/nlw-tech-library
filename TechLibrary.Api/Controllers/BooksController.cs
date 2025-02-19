using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechLibrary.Cases.Books.Filter;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;

namespace TechLibrary.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase {
        [HttpGet("Filter")]
        [ProducesResponseType(typeof(ResponseBooksJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Get(int pageNumber = 1, int pageSize = 10, string title = "") {
            RequestFilterBookJson request = new RequestFilterBookJson {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Title = title
            };
            
            FilterBookUseCase useCase = new FilterBookUseCase();
            ResponseBooksJson response = useCase.Execute(request);
            
            if(response.Books.Count > 0) return this.Ok(response);
            return this.NoContent();
        }
    }
}
