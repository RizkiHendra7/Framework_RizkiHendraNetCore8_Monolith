using FrameWorkRHP_Mono.Core.CustomAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWorkRHP_Mono.Core.Models.ViewModels
{
    public class VwMenu
    {
        public class indexDataTable
        {
            public int Intmenuid { get; set; }
            [NotMapped]
            public string TxtMenuId { get; set; }
            public string Txtmenuname { get; set; }
            [NotIncludeFilteredQuery]
            public string txtParentMenu { get; set; }
            public string Txtmenudisplay { get; set; } 
            public string Txturl { get; set; }
            public bool Bitactive { get; set; } 
        }
    }
}
