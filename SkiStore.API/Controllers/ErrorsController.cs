using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkiStore.API.Errors;

namespace SkiStore.API.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    public class ErrorsController : BaseController
    {
        [HttpGet]
        public ActionResult Error(int code)
        {
            return new ObjectResult(new APIResponse (code));
        }
    }
}
