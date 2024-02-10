using FrameWorkRHP_Mono.Core.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWorkRHP_Mono.Services.Interfaces
{
    public interface IGeneratedMenu
    {
        Task<string> generatedDynamicMenu(int paramRoleId);
        Task<string> generatedDynamicChildMenu(string paramHtmlMenu, IEnumerable<MMenu> paramAllMenu, MMenu paramModelParentMenu);
    }
}
