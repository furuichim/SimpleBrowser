using System;
using System.Drawing;
using CefSharp;
using SimpleBrowser.UI;

namespace SimpleBrowser.Handlers
{
    class LifeSpanHandler : ILifeSpanHandler
    {
        bool ILifeSpanHandler.DoClose(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {
            return false;
        }

        void ILifeSpanHandler.OnAfterCreated(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {
            SimpleBrowserFrame parent = SimpleBrowserFrame.getMainFrame(browser);
            if (parent != null)
            {
                parent.BeginInvoke(new Action(() =>
                {
                    parent.Browser = browser;
                }));
            }
        }

        void ILifeSpanHandler.OnBeforeClose(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {
        }

        bool ILifeSpanHandler.OnBeforePopup(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, string targetUrl, string targetFrameName, WindowOpenDisposition targetDisposition, bool userGesture, IPopupFeatures popupFeatures, IWindowInfo windowInfo, IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser)
        {
            SimpleBrowserFrame parent = SimpleBrowserFrame.getMainFrame(browser);
            if (parent != null)
            {
                parent.Invoke(new Action(() =>
                {
                    SimpleBrowserFrame newWindows = new SimpleBrowserFrame(false);
                    Rectangle rect = newWindows.ClientRectangle;
                    windowInfo.SetAsChild(newWindows.WebBrowserContainer.Handle, rect.Left, rect.Top, rect.Right, rect.Bottom);
                    newWindows.Show();
                }));
            }

            newBrowser = null;

            return false;
        }
    }
}
