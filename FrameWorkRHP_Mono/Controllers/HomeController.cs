using FrameWorkRHP_Mono.Core.Models.EF;
using FrameWorkRHP_Mono.Models;
using FrameWorkRHP_Mono.Services.Interfaces.GenericInterface;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FrameWorkRHP_Mono.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        public readonly IGenericService<Muser> _MUserService;
        public HomeController(IGenericService<Muser> MUserService)
        {
            _MUserService = MUserService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
