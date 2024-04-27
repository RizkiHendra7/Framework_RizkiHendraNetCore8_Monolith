using FrameWorkRHP_Mono.Core.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWorkRHP_Mono.Core.Models.ViewModels
{
    public class VwMRoleXMenu
    {
        public class indexDataTable
        {
            [NotIncludeFilteredQuery]
            public string Id { get; set; }
        }
    }
}
