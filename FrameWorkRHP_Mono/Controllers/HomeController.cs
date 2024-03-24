using FrameWorkRHP_Mono.Core.CommonFunction;
using FrameWorkRHP_Mono.Core.Models.EF;
using FrameWorkRHP_Mono.Models;
using FrameWorkRHP_Mono.Services.Interfaces.GenericInterface;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FrameWorkRHP_Mono.Controllers
{
    public class HomeController : Controller
    {    

        public IActionResult Index()
        {
            return View();
        }
    }
}
