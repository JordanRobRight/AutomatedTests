using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace QA.Automation.Common
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ConfigurationSettings
    {
        private static T GetDataFromFile<T>(string fileName)
        {
            FileInfo fi = new FileInfo(Path.Combine(Assembly.GetExecutingAssembly().Location));

            string data = File.ReadAllText(Path.Combine(fi.DirectoryName, fileName));
            T deserializedProduct = JsonConvert.DeserializeObject<T>(data);
            return deserializedProduct;
        }

        public static T GetSettingsConfiguration<T>(string filename = "Settings.json")
        {
            return GetDataFromFile<T>(filename);
        }

        public static T GetSettingsConfiguration<T>(string folder, string filename = "Settings.json")
        {
            return GetDataFromFile<T>(Path.Combine(folder, filename));
        }

        public static string SetSettingsConfiguration<T>(T testData)
        {
            JsonSerializer serializer = new JsonSerializer();
            string deserializedProduct = JsonConvert.SerializeObject(testData);
            return deserializedProduct;
        }
    }
}


