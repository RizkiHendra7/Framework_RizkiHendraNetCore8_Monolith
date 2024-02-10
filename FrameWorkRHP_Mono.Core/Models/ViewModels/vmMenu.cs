using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWorkRHP_Mono.Core.Models.ViewModels
{
    public class vmMenuIndex
    {
        public string Txtmenuid { get; set; }

        public string Txtparentmenuid { get; set; }
        public string Txtparentname { get; set; }

        public string Txtmenuname { get; set; } = null!;

        public string Txtmenudisplay { get; set; } = null!;

        public string Txtmenuicon { get; set; } = null!;

        public string Txturl { get; set; } = null!;

        public string Txtlink { get; set; } = null!;

        public bool? Bitactive { get; set; }
        public string Txtstatus {  get;set;}

    }
}
