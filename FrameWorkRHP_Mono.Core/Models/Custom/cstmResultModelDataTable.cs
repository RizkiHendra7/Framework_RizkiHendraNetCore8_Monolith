using Newtonsoft.Json;
 
namespace FrameWorkRHP_Mono.Core.Models.Custom
{
    public class cstmResultModelDataTable
    {
        [JsonProperty(PropertyName = "draw")]
        public object draw { get; set; }

        [JsonProperty(PropertyName = "recordsTotal")]
        public object recordsTotal { get; set; }

        [JsonProperty(PropertyName = "recordsFiltered")]
        public object recordsFiltered { get; set; }

        [JsonProperty(PropertyName = "data")]
        public object data { get; set; }

        [JsonProperty(PropertyName = "pages")]
        public object pages { get; set; }

        [JsonProperty(PropertyName = "allsubmit")]
        public object allsubmit { get; set; }

        [JsonProperty(PropertyName = "sessionEnabled")]
        public object sessionEnabled { get; set; }

        [JsonProperty(PropertyName = "errorMessage")]
        public object errorMessage { get; set; }
    }
     


}
