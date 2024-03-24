using AutoMapper;
using FrameWorkRHP_Mono.Core.Models.Custom;
using FrameWorkRHP_Mono.Core.Models.DTO;
using FrameWorkRHP_Mono.Core.Models.EF;
using FrameWorkRHP_Mono.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrameWorkRHP_Mono.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly IMapper _Mapper;
        private readonly ILogin _ILogin;
        public LoginController(ILogin ILogin, IMapper Mapper)
        {
            _ILogin = ILogin;
            _Mapper = Mapper;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(DTOMusers paramData)
        {
            try
            {
                Muser MUserData = _Mapper.Map<Muser>(paramData);
                var result = await _ILogin.LoginAsync(MUserData);
                 
                return Json(result);
            }
            catch (Exception ex)
            {
                cstmResultModelDataTable result = new cstmResultModelDataTable();
                result.errorMessage = ex.Message;
                result.recordsTotal = 0;
                result.recordsFiltered = 0;
                return Json(result);
            }


        }

    }
}
