using Newtonsoft.Json;

namespace QA.Automation.APITests.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class APRIConfigSettings
    {
        [JsonProperty]
        //public string Environment { get; set; }
        public Common.EnvironmentType Environment { get; set; }

        [JsonProperty]
        public string UserName { get; set; }
        [JsonProperty]
        public string Password { get; set; }

        public string ServiceName { get; set; }
    }
}

    
