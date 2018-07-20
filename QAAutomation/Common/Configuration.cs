using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace QA.Automation.Common
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ConfigurationSettings
    {
        public static T GetSettingsConfiguration<T>(string filename = "Settings.json")
        {
            JsonSerializer serializer = new JsonSerializer();
            FileInfo fi = new FileInfo(Path.Combine(Assembly.GetExecutingAssembly().Location));

            string data = File.ReadAllText(Path.Combine(fi.DirectoryName, filename));
            T deserializedProduct = JsonConvert.DeserializeObject<T>(data);
            return deserializedProduct;
        }
    }
}

    
