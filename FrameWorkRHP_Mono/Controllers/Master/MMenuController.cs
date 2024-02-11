using FrameWorkRHP_Mono.Core.Models.Custom;
using FrameWorkRHP_Mono.Core.Models.EF;
using FrameWorkRHP_Mono.Services.Interfaces.GenericInterface;
using FrameWorkRHP_Mono.Services.ServicesImplement;
using Microsoft.AspNetCore.Mvc;

namespace FrameWorkRHP_Mono.Controllers.Master
{
    public class MMenuController : Controller
    {
        #region constructor
        public readonly IGenericService<MMenu> _MMenuService;
        public MMenuController(IGenericService<MMenu> MUserService)
        {
            _MMenuService = MUserService;
        }
        #endregion


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(string id)
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> GetDataIndex(cstmFilterDataTable param)
        {
            try
            {
                var result = new cstmResultModelDataTable();
                //var result = await _MMenuService.getDataPaging(param);
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
