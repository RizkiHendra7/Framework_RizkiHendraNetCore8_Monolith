using FrameWorkRHP_Mono.Core.CustomAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWorkRHP_Mono.Core.Models.ViewModels
{
    public class vwRole
    {
        public class indexDataTable
        {
            [NotIncludeFilteredQuery]
            public string Id { get; set; }
            public int Introleid { get; set; }
            public string Txtrolename { get; set; } = null!;
            public bool? Bitactive { get; set; }

        }
    }
}
