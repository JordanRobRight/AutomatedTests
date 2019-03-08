using System;
using System.Windows.Forms;
using AutoIt;

namespace QA.Automation.MiscLib
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
        public static void GetAutoIt(string windowName, string textToType, int waitTime = 2)
        {
            AutoItX.WinActivate(windowName);

            AutoItX.Send(textToType);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(waitTime));
            AutoItX.Send("{ENTER}");

        }
    }
}
