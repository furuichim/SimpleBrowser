using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using CefSharp;

namespace SimpleBrowser.Util
{
    class Test
    {
        public String exec(String exePath, String arguments = null)
        {
            Process ps = Process.Start(exePath, arguments);
            return "Hello";
        }

        public void wait(int millisecond, IJavascriptCallback callback)
        {
            Task.Delay(millisecond).ContinueWith((o) =>
            {
                if (callback.CanExecute)
                {
                    callback.ExecuteAsync(new int[5] { 1, 2, 3, 4, 5 });
                }
            });

        }
    }
}
