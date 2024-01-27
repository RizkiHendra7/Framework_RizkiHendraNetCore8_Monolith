using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWorkRHP_Mono.Core.Models.EF
{ 
    public partial class Mrole
    {
        public int Introleid { get; set; }

        public string Txtrolename { get; set; } = null!;

        public bool? Bitactive { get; set; }

        public DateTime? Dtinserted { get; set; }

        public string? Txtinsertedby { get; set; }

        public DateTime? Dtupdated { get; set; }

        public string? Txtupdated { get; set; }

        public virtual ICollection<Muserrole> Muserroles { get; set; } = new List<Muserrole>();
    }

}
