using FrameWorkRHP_Mono.Core.CustomAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWorkRHP_Mono.Core.Models.ViewModels
{
    public class VwMRoleXMenu
    {
        public class indexDataTable
        {
            [NotMapped]
            public string id { get; set; }
            public string txtMenuName { get; set; }
            public string txtRoleName { get; set; }
            public bool bitActive { get; set; }
            public int intMRolexMenuId { get; set; }

        }
    }
}
