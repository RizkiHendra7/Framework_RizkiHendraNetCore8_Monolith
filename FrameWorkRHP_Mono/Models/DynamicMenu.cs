using FrameWorkRHP_Mono.Core.Models.EF;
using FrameWorkRHP_Mono.Services.Interfaces;
using FrameWorkRHP_Mono.Services.Interfaces.GenericInterface;

namespace FrameWorkRHP_Mono.Models
{
    public class DynamicMenu
    {
        #region constructor
        public readonly IGeneratedMenu _MMenuService;
        public DynamicMenu(IGeneratedMenu MUserService)
        {
            _MMenuService = MUserService;
        }
        #endregion




        public async Task<string> generateMenu(int paramModuleExisting)
        {
          
            return await _MMenuService.generatedDynamicMenu(1);
        }



    }
}
