using Microsoft.AspNetCore.Mvc;
using SkiStore.API.Errors;
using SkiStore.Data;

namespace SkiStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : BaseController
    {
        private readonly SkiStoreDbContext _context;

        public BuggyController(SkiStoreDbContext context)
        {
            _context = context;
        }
        [HttpGet("notFound")]
        public ActionResult GetNotFoundRequest()
        {
            var thing = _context.Products.Find(555);
            if (thing is null)
            {
                return NotFound(new APIResponse(404));
            }
            return Ok();
        }
        [HttpGet("serverError")]
        public ActionResult GetServerError()
        {
            var thing = _context.Products.Find(555);
            var thingToRetrun = thing.ToString();
            return Ok();
        }
        [HttpGet("badRequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new APIResponse(400));
        } 
        [HttpGet("badRequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return Ok();
        }


    }
}
