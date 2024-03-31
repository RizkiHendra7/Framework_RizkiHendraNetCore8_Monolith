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
        public string txtMenuName { get; set; }
        public string txtMenuDisplay { get; set; }
        public string TxtUrl { get; set; } = null!;
        public string txtMenuIcon { get; set; } 
        public bool? bitActive { get; set; }
    }
}
