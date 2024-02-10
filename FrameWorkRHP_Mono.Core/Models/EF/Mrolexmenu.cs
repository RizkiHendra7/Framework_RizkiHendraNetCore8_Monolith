using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWorkRHP_Mono.Core.Models.EF
{ 
    public partial class MRoleXMenu
    {
        public int Intmrolexmenuid { get; set; }

        public int Intmenuid { get; set; }

        public int Introleid { get; set; }

        public bool? Bitactive { get; set; }

        public DateTime? Dtinserted { get; set; }

        public string? Txtinsertedby { get; set; }

        public DateTime? Dtupdated { get; set; }

        public string? Txtupdated { get; set; }

        public virtual MMenu Intmenu { get; set; } = null!;

        public virtual Mrole Introle { get; set; } = null!;
    }

}
