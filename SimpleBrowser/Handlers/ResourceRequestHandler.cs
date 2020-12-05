using System;
using System.Linq;
using System.Text.RegularExpressions;
using CefSharp;

namespace SimpleBrowser.Handlers
{
    class ResourceRequestHandler : IResourceRequestHandler
    {
        void IDisposable.Dispose()
        {
        }

        ICookieAccessFilter IResourceRequestHandler.GetCookieAccessFilter(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request)
        {
            return new CookieAccessFilter();
        }

        IResourceHandler IResourceRequestHandler.GetResourceHandler(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request)
        {
            return null;
        }

        IResponseFilter IResourceRequestHandler.GetResourceResponseFilter(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IResponse response)
        {
            // メインフレームとサブフレームのリソースの場合だけリソースを改変する。
            // 注意：pdfや開発者ツールの場合には、フィルターが適用されないように、httpとhttpsプロトコルだけに限定する。
            if ((request.ResourceType == ResourceType.MainFrame || request.ResourceType == ResourceType.SubFrame) &&
                Regex.IsMatch(request.Url, "^(http|https)://.*$", RegexOptions.IgnoreCase))
            {
                return new ResponseFilter(response.Charset);
            }

            return null;
        }

        CefReturnValue IResourceRequestHandler.OnBeforeResourceLoad(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IRequestCallback callback)
        {
            return CefReturnValue.Continue;
        }

        bool IResourceRequestHandler.OnProtocolExecution(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request)
        {
            return false;
        }

        void IResourceRequestHandler.OnResourceLoadComplete(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IResponse response, UrlRequestStatus status, long receivedContentLength)
        {
        }

        void IResourceRequestHandler.OnResourceRedirect(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IResponse response, ref string newUrl)
        {
        }

        bool IResourceRequestHandler.OnResourceResponse(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IResponse response)
        {
            return false;
        }
    }
}
