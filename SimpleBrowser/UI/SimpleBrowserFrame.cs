using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using CefSharp;
using CefSharp.WinForms;

namespace SimpleBrowser.UI
{
    public partial class SimpleBrowserFrame : Form
    {
        /// <summary>
        /// CefSharpのWebViewのインスタンス
        /// </summary>
        public ChromiumWebBrowser WebBrowser { get; private set; }

        /// <summary>
        /// ホストしているWebViewのIBrowserインタフェース
        /// </summary>
        public IBrowser Browser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<String> Favicons { get; } = new List<string>();

        /// <summary>
        /// WebViewのコンテナコントロール
        /// </summary>
        public Control WebBrowserContainer
        {
            get {
                return webViewContainer;
            }
        }

        public SimpleBrowserFrame(bool bCreateWebView = true)
        {
            InitializeComponent();

            // WebView作成の指定がある場合は、WebViewを作成する
            if (bCreateWebView)
            {
                InitializeWebBrowser();
            }
        }

        private void InitializeWebBrowser()
        {
            // CefSharpのWebViewを作成する。
            WebBrowser = new ChromiumWebBrowser("https://www.google.co.jp");
            //WebBrowser = new ChromiumWebBrowser("chrome://version");

            // コントロールを追加する。
            this.webViewContainer.Controls.Add(WebBrowser);
            WebBrowser.Dock = DockStyle.Fill;

            WebBrowser.BrowserSettings.Javascript = CefState.Enabled;               // javascriptは無効にできる。
            WebBrowser.BrowserSettings.JavascriptAccessClipboard = CefState.Enabled;// 【注意】有効にならない。
            WebBrowser.BrowserSettings.JavascriptDomPaste = CefState.Enabled;       // document.execCommandでのcopy&pasteは無効にできる。
            WebBrowser.BrowserSettings.ApplicationCache = CefState.Enabled;         // Application Cacheも無効にできる。
            WebBrowser.BrowserSettings.LocalStorage = CefState.Enabled;             // localStorageは無効にできる。
            WebBrowser.BrowserSettings.Databases = CefState.Enabled;                // 【注意】無効できない。
            //WebBrowser.BrowserSettings.DefaultFontSize = 50;                      // デフォルトフォントサイズも指定できる。

            WebBrowser.KeyboardHandler = new Handlers.KeyboardHandler();
            WebBrowser.RequestHandler = new Handlers.RequestHandler();
            WebBrowser.DisplayHandler = new Handlers.DisplayHandler();
            WebBrowser.DownloadHandler = new Handlers.DownloadHandler();
            WebBrowser.LoadHandler = new Handlers.LoadHandler();
            WebBrowser.LifeSpanHandler = new Handlers.LifeSpanHandler();

            WebBrowser.JavascriptObjectRepository.Register("Test", new SimpleBrowser.Util.Test(), true, null);
        }

        private void SimpleBrowserFrame_Load(object sender, EventArgs e)
        {
        }

        private void setProxy(String server, String bypassList)
        {
            Cef.UIThreadTaskFactory.StartNew(new Action(() =>
            {
                var v = new Dictionary<string, object>();
                v.Add("mode", "fixed_servers");
                v.Add("server", server);
                v.Add("bypass_list", bypassList);

                String error;
                WebBrowser.GetBrowserHost().RequestContext.SetPreference("proxy", v, out error);
            }));
        }

        public void updateFavicon(String path, String mimeType)
        {
            try
            {
                // MIME-Typeに応じて、Iconオブジェクトを作成する。
                Icon icon = null;
                switch (mimeType.ToLower())
                {
                    case "image/png":
                    case "	image/gif":
                        Bitmap bitmap = new Bitmap(Image.FromFile(path));
                        icon = Icon.FromHandle(bitmap.GetHicon());
                        break;
                    case "image/vnd.microsoft.icon":
                    case "image/x-icon":
                        icon = Icon.ExtractAssociatedIcon(path);
                        break;
                    default:
                        break;
                }

                // ファビコンを表示する
                if (icon != null)
                {
                    this.Icon = icon;
                }

                // FVICONファイルを削除する。
                System.IO.File.Delete(path);
            }
            catch
            {
            }
        }

        public void updateAddressBar(String url)
        {
            this.addressBar.Text = url;
        }

        private void SimpleBrowserFrame_Resize(object sender, EventArgs e)
        {
            if (Browser != null)
            {
                IntPtr hWnd = Browser.GetHost().GetWindowHandle();
                SetWindowPos(hWnd, HWND_TOP, 0, 0, webViewContainer.Width, webViewContainer.Height, SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOZORDER);
            }
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            if (Browser.CanGoBack)
            {
                Browser.GoBack();
            }
        }

        private void forwardBtn_Click(object sender, EventArgs e)
        {
            if (Browser.CanGoForward)
            {

                Browser.GoForward();
            }
        }

        private void reloadBtn_Click(object sender, EventArgs e)
        {
            Browser.Reload();
        }

        private void addressBar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!String.IsNullOrEmpty(addressBar.Text))
                {
                    Browser.MainFrame.LoadUrl(addressBar.Text);
                }
            }
        }

        /// <summary>
        /// IBrowserからメインフレームを取得する
        /// </summary>
        /// <param name="browser"></param>
        /// <returns></returns>
        public static SimpleBrowserFrame getMainFrame(IBrowser browser)
        {
            // browserのウィンドウハンドル
            IntPtr hWnd = browser.GetHost().GetWindowHandle();

            // browserの親コントロール(webViewContainer)
            Control container = Control.FromHandle(GetParent(hWnd));

            if (container != null && container.TopLevelControl is SimpleBrowserFrame)
            {
                // コントロールのトップレベルのコントロールを取得して返却する
                return (SimpleBrowserFrame)container.TopLevelControl;
            }
            else
            {
                return null;
            }
        }

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        private static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);

        [Flags]
        public enum SetWindowPosFlags : uint
        {
            SWP_ASYNCWINDOWPOS = 0x4000,
            SWP_DEFERERASE = 0x2000,
            SWP_DRAWFRAME = 0x0020,
            SWP_FRAMECHANGED = 0x0020,
            SWP_HIDEWINDOW = 0x0080,
            SWP_NOACTIVATE = 0x0010,
            SWP_NOCOPYBITS = 0x0100,
            SWP_NOMOVE = 0x0002,
            SWP_NOOWNERZORDER = 0x0200,
            SWP_NOREDRAW = 0x0008,
            SWP_NOREPOSITION = 0x0200,
            SWP_NOSENDCHANGING = 0x0400,
            SWP_NOSIZE = 0x0001,
            SWP_NOZORDER = 0x0004,
            SWP_SHOWWINDOW = 0x0040,
        }

        private static readonly IntPtr HWND_TOP = new IntPtr(0);
        private static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
    }
}
