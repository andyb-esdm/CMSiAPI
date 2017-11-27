using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMSiAPI.Controllers
{
    [Route("api/workrecords")]
    public class WorkRecordsController : Controller
    {
        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            return new ObjectResult(new[] {
                new { Id = 1, Description = "Work Record 1" },
                new { Id = 2, Description = "Work Record 2" }
            });
        }
    }
}