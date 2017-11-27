using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMSiAPI.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }

        [Authorize] 
        public IActionResult GetUserDetails() {
            return new ObjectResult(new
            {
                Username = User.Identity.Name
            });
        }
    }
}