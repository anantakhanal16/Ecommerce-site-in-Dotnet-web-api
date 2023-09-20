using ApiFinal.Errors;
using Microsoft.AspNetCore.Mvc;

namespace ApiFinal.Controllers
{
    [Route("errors/{code}")]
    public class ErrorController: BaseApiController
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }

    }
}
