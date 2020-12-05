using System;
using System.IO;
using CefSharp;
using SimpleBrowser.UI;

namespace SimpleBrowser.Handlers
{
    class DownloadHandler : IDownloadHandler
    {
        void IDownloadHandler.OnBeforeDownload(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IBeforeDownloadCallback callback)
        {
            // コントロールのトップレベルのコントロールを取得（SimpleBrowserFrame）
            SimpleBrowserFrame mainFrame = SimpleBrowserFrame.getMainFrame(browser);

            // faviconの場合は、ランダムなファイル名で保存
            if (mainFrame != null && mainFrame.Favicons.Contains(downloadItem.Url))
            {
                callback.Continue($"c:\\temp\\{Path.GetRandomFileName()}", false);
            }
            // それ以外の場合は、ファイル保存ダイアログを表示して保存する。
            else
            {
                callback.Continue("", true);
            }
        }

        void IDownloadHandler.OnDownloadUpdated(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IDownloadItemCallback callback)
        {
            // ダウンロードが完了した場合
            if (downloadItem.IsComplete)
            {
                // コントロールのトップレベルのコントロールを取得（SimpleBrowserFrame）
                SimpleBrowserFrame mainFrame = SimpleBrowserFrame.getMainFrame(browser);

                // faviconの場合
                if (mainFrame != null && mainFrame.Favicons.Contains(downloadItem.Url))
                {
                    // ロード中のfaviconファイルのリストから取り除く
                    mainFrame.Favicons.Remove(downloadItem.Url);

                    // faviconを更新する。
                    mainFrame.BeginInvoke(new Action(() =>
                    {
                        mainFrame.updateFavicon(downloadItem.FullPath, downloadItem.MimeType);

                    }));
                }

            }
        }
    }
}
