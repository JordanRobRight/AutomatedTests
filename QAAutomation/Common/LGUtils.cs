using System;

namespace QA.Automation.Common
{
    public class LGUtils
    {
        public static string GetUrlBaseUrl(string env, string url)
        {
            //string tempUrl = url;
            //return string.Equals(env, "Prod", StringComparison.OrdinalIgnoreCase) ? string.Format(tempUrl, "") : string.Format(tempUrl, "-" + env);
            return string.Format(url, "-" + env);
        }
    }
}
