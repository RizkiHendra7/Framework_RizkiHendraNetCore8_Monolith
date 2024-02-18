using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWorkRHP_Mono.Core.Models.DTO
{
    public class DTOMusers
    {
        public string id { get; set; } 

        public string Txtusername { get; set; } = null!;

        public string Txtpassword { get; set; } = null!;

        public string Txtfullname { get; set; } = null!;

        public bool? Bitactive { get; set; }
    }
}
