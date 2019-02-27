using System.Collections.Generic;
using Newtonsoft.Json;

namespace QA.Automation.Common.Models
{
    public class EnvironmentTestData
    {
        [JsonProperty] public Common.EnvironmentType EnvironmentName { get; set; }

        public Dictionary<string, dynamic> TestAnswers { get; set; }
    }
}
