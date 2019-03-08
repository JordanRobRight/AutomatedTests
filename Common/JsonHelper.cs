using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace QA.Automation.Common
{
    public class JsonHelper
    {
        public static JObject GetJsonJObjectFromString(string jsonString)
        {
            JObject jObectData = new JObject();

            try
            {
                JsonConvert.DeserializeObject(jsonString);
                jObectData = JsonConvert.DeserializeObject<JObject>(jsonString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return jObectData;
        }

        public static JToken GetTokenByPath(JToken token, string path)
        {
            JToken aToken = new JObject();
            try
            {
                aToken = token.SelectToken(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return aToken;
        }

        public static Match GetMatchFromRegEx(JToken jObject, string regExExpression)
        {
            Match match = null;

            try
            {
                if (jObject != null)
                {
                    match = Regex.Match(jObject.Value<string>(), regExExpression);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return match;
        }
    }
}
