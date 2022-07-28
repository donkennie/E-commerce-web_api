using E_CommerceAPI.Errors;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceAPI.Controllers
{
  
        [Route("errors/{code}")]
       // [ApiExplorerSettings(IgnoreApi = true)]

        public class ErrorController : BaseApiController
        {
            public IActionResult Error(int code)
            {
                return new ObjectResult(new ApiResponse(code));
            }
        }
  
}
    