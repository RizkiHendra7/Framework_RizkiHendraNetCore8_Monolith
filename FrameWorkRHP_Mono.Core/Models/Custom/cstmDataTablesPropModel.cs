using FrameWorkRHP_Mono.Core.CustomAttribute;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameWorkRHP_Mono.Core.Models.Custom
{
    public class cstmDataTablesPropModel
    {
        [NotMapped]
        public string txtId { get; set; }

        [NotIncludeFilteredQuery]
        public double TotalData { get; set; }

        [NotIncludeFilteredQuery]
        public double serialNumber { get; set; } 
    }
}
