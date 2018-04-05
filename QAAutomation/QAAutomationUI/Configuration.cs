using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace QA.Automation.UITests
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TestConfiguration
    {
        [JsonProperty]
        //public string Environment { get; set; }
        public Common.EnvironmentType Environment { get; set; }
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

        public static TestConfiguration GetTestConfiguration()
        {
            JsonSerializer serializer = new JsonSerializer();
            FileInfo fi = new FileInfo(Path.Combine(Assembly.GetExecutingAssembly().Location));

            string data = System.IO.File.ReadAllText(Path.Combine(fi.DirectoryName, "Settings.json"));
            UITests.TestConfiguration deserializedProduct = JsonConvert.DeserializeObject<UITests.TestConfiguration>(data);
            return deserializedProduct;
        }
    }
}

    
