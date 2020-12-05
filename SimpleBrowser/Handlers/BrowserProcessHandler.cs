using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp;

namespace SimpleBrowser.Handlers
{
    class BrowserProcessHandler : IBrowserProcessHandler
    {
        void IDisposable.Dispose()
        {
        }

        void IBrowserProcessHandler.OnContextInitialized()
        {
        }

        void IBrowserProcessHandler.OnScheduleMessagePumpWork(long delay)
        {
        }
    }
}
