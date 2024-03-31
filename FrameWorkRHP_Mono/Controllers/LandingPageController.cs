using FrameWorkRHP_Mono.Services.ServicesImplement.GenericServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrameWorkRHP_Mono.Controllers
{
    [Authorize]
    //[CustomAuthorize]
    public class LandingPageController : Controller
    {
        public IActionResult Index()
        {
            var dicobamauEggPakeStatic = SessionService.testDtLogin;
            return View();
        }
    }
}
