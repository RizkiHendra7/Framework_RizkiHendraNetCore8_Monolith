using FrameWorkRHP_Mono.Core.Models.Custom;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWorkRHP_Mono.Core.Models.ViewModels
{
    public class VwUser
    {
        public class indexDataTable : cstmDataTablesPropModel
        {
            public string Txtfullname { get; set; } = null!;
            public string Txtusername { get; set; } = null!;
            public bool? Bitactive { get; set; }
            public int Intuserid { get; set; } 

        }
    }
}
