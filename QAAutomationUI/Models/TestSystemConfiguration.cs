﻿using Newtonsoft.Json;

namespace QA.Automation.UITests.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TestSystemConfiguration
    {
        [JsonProperty]
        //public string Environment { get; set; }
        public Common.EnvironmentType Environment { get; set; }
        public Common.EnvironmentType Email { get; set; }
        [JsonProperty]
        public string SauceLabsUser { get; set; }
        [JsonProperty]
        public string SauceLabsKey { get; set; }
        [JsonProperty]
        public bool IsRemoteDriver { get; set; }
        [JsonProperty]
        public string BaseUrl { get; set; }
        [JsonProperty]
        public int WaitTimeInSeconds { get; set; }
        [JsonProperty]
        public int PageWaitTimeInSeconds { get; set; }
        [JsonProperty]
        public string LGUser { get; set; }
        [JsonProperty]
        public string LGPassword { get; set; }
        [JsonProperty]
        public int SlidingWaitFactor { get; set; }

        [JsonProperty]
        public int MaxDuration { get; set; }

        [JsonProperty]
        public string TestDataFolder { get; set; }
       
    }
}


