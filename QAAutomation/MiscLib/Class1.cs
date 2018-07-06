using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiscLib
{
    public class Helper
    {
        public static void SendTheKey(string text)
        {
            System.Windows.Forms.SendKeys.SendWait(text);
        }
    }
}
