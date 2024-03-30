using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrameWorkRHP_Mono.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {    

        public IActionResult Index()
        {
            return View();
        }
    }
}
