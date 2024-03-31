using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWorkRHP_Mono.Core.Models.DTO
{
    public class DTOMMenus
    {
        public string id { get; set; }
        public string idParent { get; set; }
        public string Txtmenuname { get; set; }
        public string Txtmenudisplay { get; set; }
        public string Txtmenuicon { get; set; } 
        public bool? Bitactive { get; set; }
    }
}
