using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace QA.Automation.UITests.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TestData
    {
        [JsonProperty]
        //public string Environment { get; set; }
        public string version { get; set; }
        [JsonProperty]
        public List<EnvironmentData> Environment { get; set; }
        //[JsonProperty]
        //public string SauceLabsKey { get; set; }
        //[JsonProperty]
        //public bool IsRemoteDriver { get; set; }
        //[JsonProperty]
        //public string BaseUrl { get; set; }
        //[JsonProperty]
        //public int WaitTimeInSeconds { get; set; }
        //[JsonProperty]
        //public int PageWaitTimeInSeconds { get; set; }
        //[JsonProperty]
        //public string LGUser { get; set; }
        //[JsonProperty]
        //public string LGPassword { get; set; }

        [JsonProperty]
        public List<EmailUsData> EmailDetails { get; set; }
    }

    public class EnvironmentData
    {
        [JsonProperty]
        //public string Environment { get; set; }
        public string Name { get; set; }
        [JsonProperty]
        //public string Environment { get; set; }
        public string Player { get; set; }
        // [JsonProperty]
        // public string FullName { get; set; }
    }
    public class EmailUsData
    {

        [JsonProperty]
        public string FullName { get; set; }
    }
}


