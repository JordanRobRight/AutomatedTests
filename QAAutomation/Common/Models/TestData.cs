using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace QA.Automation.Common.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TestData
    {
        [JsonProperty]
        public string version { get; set; }
        [JsonProperty]
        public List<EnvironmentTestData> Data { get; set; }
    }
}
