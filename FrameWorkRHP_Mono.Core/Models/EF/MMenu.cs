using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWorkRHP_Mono.Core.Models.EF
{
    public partial class MMenu
    {
        public int Intmenuid { get; set; }

        public int? Intparentmenuid { get; set; }

        public string Txtmenuname { get; set; } = null!;

        public string Txtmenudisplay { get; set; } = null!;

        public string Txtmenuicon { get; set; } = null!;

        public string Txturl { get; set; } = null!; 

        public bool? Bitactive { get; set; }

        public DateTime? Dtinserted { get; set; }

        public string? Txtinsertedby { get; set; }

        public DateTime? Dtupdated { get; set; }

        public string? Txtupdated { get; set; }
        public virtual ICollection<MRoleXMenu> Mrolexmenus { get; set; } = new List<MRoleXMenu>();
    }

}
