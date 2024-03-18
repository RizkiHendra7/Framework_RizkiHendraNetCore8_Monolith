using FrameWorkRHP_Mono.Core.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWorkRHP_Mono.Core.Models.Custom
{
    public class cstmSessionModel
    {
        public Muser userDt {  get; set; }
        public IEnumerable<Mrole> roleLt { get; set; }
    }
}
