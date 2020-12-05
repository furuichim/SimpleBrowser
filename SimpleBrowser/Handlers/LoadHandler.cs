using System;
using System.Windows.Forms;
using CefSharp;

namespace SimpleBrowser.Handlers
{
    class LoadHandler : ILoadHandler
    {
        void ILoadHandler.OnFrameLoadEnd(IWebBrowser chromiumWebBrowser, FrameLoadEndEventArgs frameLoadEndArgs)
        {
        }

        void ILoadHandler.OnFrameLoadStart(IWebBrowser chromiumWebBrowser, FrameLoadStartEventArgs frameLoadStartArgs)
        {
        }

        void ILoadHandler.OnLoadError(IWebBrowser chromiumWebBrowser, LoadErrorEventArgs loadErrorArgs)
        {
        }

        void ILoadHandler.OnLoadingStateChange(IWebBrowser chromiumWebBrowser, LoadingStateChangedEventArgs loadingStateChangedArgs)
        {
            // ロードが完了した場合
            if (!loadingStateChangedArgs.IsLoading)
            {
                // Javascriptを実行する。
                //loadingStateChangedArgs.Browser.MainFrame.EvaluateScriptAsync(@"document.body.setAttribute('style', 'background-color:red;')");

                //loadingStateChangedArgs.Browser.MainFrame.EvaluateScriptAsync(@"(()=>{return document.title;})();").ContinueWith((reponse) =>
                //{
                //    String title = (String)reponse.Result.Result;
                //    MessageBox.Show($"document.title = {title}");
                //});
            }
        }
    }
}
