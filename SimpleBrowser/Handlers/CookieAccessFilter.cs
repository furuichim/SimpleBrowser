using System;
using CefSharp;

namespace SimpleBrowser.Handlers
{
    class CookieAccessFilter : ICookieAccessFilter
    {
        bool ICookieAccessFilter.CanSaveCookie(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IResponse response, Cookie cookie)
        {
            //return false;
            return true;
        }

        bool ICookieAccessFilter.CanSendCookie(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, Cookie cookie)
        {
            //return false;
            return true;
        }
    }
}
