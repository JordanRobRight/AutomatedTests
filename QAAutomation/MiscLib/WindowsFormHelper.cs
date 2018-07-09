using System.Windows.Forms;

namespace MiscLib
{
    public class WindowsFormHelper
    {
        public static void SendKeyToForm(string text, bool isWaiting = false)
        {
            try
            {
                if (isWaiting)
                {
                    SendKeys.SendWait(text);
                }
                else
                {
                    SendKeys.Send(text);
                }
            }
            catch
            {

            }
        }
    }
}
