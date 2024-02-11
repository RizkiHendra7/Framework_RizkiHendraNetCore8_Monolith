using FrameWorkRHP_Mono.Core.Models.Custom;
using FrameWorkRHP_Mono.Core.Models.EF;
using FrameWorkRHP_Mono.Services.Interfaces;
using FrameWorkRHP_Mono.Services.Interfaces.GenericInterface;
using FrameWorkRHP_Mono.Services.ServicesImplement;
using Microsoft.AspNetCore.Mvc;

namespace FrameWorkRHP_Mono.Models
{
    public class DynamicMenu  :Controller
    {
        #region constructor
        public readonly IGeneratedMenu _MMenuService;
        public readonly IGenericService<MMenu> _MMenuService2;
        public DynamicMenu(IGeneratedMenu MUserService, IGenericService<MMenu> mMenuService2)
        {
            _MMenuService = MUserService;
            _MMenuService2 = mMenuService2;
        }
        #endregion



        [HttpPost]
        public async Task<ActionResult> GenerateMenu(int paramModuleExisting)
        {
            //var xxx = await _MMenuService2.GetAllData();
            var result = await _MMenuService.generatedDynamicMenu(1);
            return Json(result);
        }
         
    }
}
