using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace QA.Automation.APITests
{
   // [JsonObject(MemberSerialization.OptIn)]
    //public class TestConfiguration
    //{
    //    [JsonProperty]
    //    //public string Environment { get; set; }
    //    public QA.Automation.Common.EnvironmentType Environment { get; set; }
    //    [JsonProperty]
    //    public string UserName { get; set; }
    //    [JsonProperty]
    //    public string Password { get; set; }
    //    [JsonProperty]
    //    public string BaseUrl { get; set; }

    //    public static TestConfiguration GetTestConfiguration()
    //    {
    //        JsonSerializer serializer = new JsonSerializer();
    //        FileInfo fi = new FileInfo(Path.Combine(Assembly.GetExecutingAssembly().Location));

    //        string data = System.IO.File.ReadAllText(Path.Combine(fi.DirectoryName, "Settings.json"));
    //        TestConfiguration deserializedProduct = JsonConvert.DeserializeObject<TestConfiguration>(data);
    //        return deserializedProduct;
    //    }
    //}
}

    
