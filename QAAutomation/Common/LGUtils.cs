using System;

namespace QA.Automation.Common
{
    public class LGUtils
    {
        public static string GetUrlBaseUrl(string env, string url, bool isProdUrl = false)
        {
            string tempUrl = url;
            return string.Equals(env, "Prod", StringComparison.OrdinalIgnoreCase) ? string.Format(tempUrl, "") : string.Format(tempUrl, (isProdUrl ? "." : "-") + env);
        }
    }
}
