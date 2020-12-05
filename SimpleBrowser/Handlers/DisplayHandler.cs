using System;
using System.Collections.Generic;
using CefSharp;
using CefSharp.Structs;
using SimpleBrowser.UI;
using log4net;

namespace SimpleBrowser.Handlers
{
    class DisplayHandler : IDisplayHandler
    {
        /// <summary>
        /// Logger
        /// </summary>
        private static ILog logger = LogManager.GetLogger("ConsoleLog");

        public void OnAddressChanged(IWebBrowser chromiumWebBrowser, AddressChangedEventArgs addressChangedArgs)
        {
            SimpleBrowserFrame mainFrame = SimpleBrowserFrame.getMainFrame(addressChangedArgs.Browser);
            if (mainFrame != null)
            {
                mainFrame.BeginInvoke(new Action(() =>
                {
                    mainFrame.updateAddressBar(addressChangedArgs.Address);
                }));
            }
        }

        public bool OnAutoResize(IWebBrowser chromiumWebBrowser, IBrowser browser, Size newSize)
        {
            // ブラウザのデフォルト処理を行う
            return false;
        }

        public bool OnConsoleMessage(IWebBrowser chromiumWebBrowser, ConsoleMessageEventArgs consoleMessageArgs)
        {
            switch (consoleMessageArgs.Level)
            {
                case LogSeverity.Error:
                    logger.Error(consoleMessageArgs.Message);
                    break;
                case LogSeverity.Fatal:
                    logger.Fatal(consoleMessageArgs.Message);
                    break;
                case LogSeverity.Info:
                    logger.Info(consoleMessageArgs.Message);
                    break;
                case LogSeverity.Warning:
                    logger.Warn(consoleMessageArgs.Message);
                    break;
                case LogSeverity.Verbose:
                    logger.Debug(consoleMessageArgs.Message);
                    break;
            }

            // コンソールにメッセージを出力する
            return false;
        }

        public void OnFaviconUrlChange(IWebBrowser chromiumWebBrowser, IBrowser browser, IList<string> urls)
        {
            // コントロールのトップレベルのコントロールを取得（SimpleBrowserFrame）
            SimpleBrowserFrame mainFrame = SimpleBrowserFrame.getMainFrame(browser);
            if (mainFrame != null)
            {
                // ロード中のFaviconを登録する
                mainFrame.Favicons.Add(urls[0]);
            }

            // Faviconをロードする。
            browser.GetHost().StartDownload(urls[0]);
        }

        public void OnFullscreenModeChange(IWebBrowser chromiumWebBrowser, IBrowser browser, bool fullscreen)
        {
        }

        public void OnLoadingProgressChange(IWebBrowser chromiumWebBrowser, IBrowser browser, double progress)
        {
        }

        public void OnStatusMessage(IWebBrowser chromiumWebBrowser, StatusMessageEventArgs statusMessageArgs)
        {
        }

        public void OnTitleChanged(IWebBrowser chromiumWebBrowser, TitleChangedEventArgs titleChangedArgs)
        {
            // コントロールのトップレベルのコントロールを取得（SimpleBrowserFrame）
            SimpleBrowserFrame mainFrame = SimpleBrowserFrame.getMainFrame(titleChangedArgs.Browser);

            if (mainFrame != null)
            {
                // 親コントロールのコンテキストでタイトル文字列を変更する。
                mainFrame.BeginInvoke(new Action(() =>
                {
                    // タイトル文字列を変更する
                    mainFrame.Text = titleChangedArgs.Title;
                }));
            }
        }

        public bool OnTooltipChanged(IWebBrowser chromiumWebBrowser, ref string text)
        {
            // ブラウザにツールチップ表示の処理をさせる
            return false;
        }
    }
}
