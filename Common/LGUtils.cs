using System;
using System.Text;

namespace QA.Automation.Common
{
    public class LgUtils
    {
        public static string GetUrlBaseUrl(string env, string url, bool isProdUrl = false)
        {
            //string tempUrl = url;
            //return string.Equals(env, "Prod", StringComparison.OrdinalIgnoreCase) ? string.Format(tempUrl, "") : string.Format(tempUrl, "-" + env);
            if (isProdUrl)
            {
                return string.Format(url, "." + env);

            }
            return string.Format(url, "-" + env);
        }

        public static string GetStringFromBase64(string encodedData)
        {
            byte[] data = Convert.FromBase64String(encodedData);
            string decodedData = Encoding.UTF8.GetString(data);
            return string.IsNullOrWhiteSpace(decodedData) ? string.Empty : decodedData;
        }
    }
}
