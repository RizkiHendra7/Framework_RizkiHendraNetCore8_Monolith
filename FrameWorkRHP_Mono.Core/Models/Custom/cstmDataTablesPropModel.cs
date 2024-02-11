using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWorkRHP_Mono.Core.Models.Custom
{
    public class cstmDataTablesPropModel
    {
        [NotMapped]
        public string txtId { get; set; }   
        public double TotalData { get; set; } 
    }
}
