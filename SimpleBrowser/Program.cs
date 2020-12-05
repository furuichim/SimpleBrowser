using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using SimpleBrowser.Handlers;
using SimpleBrowser.UI;

namespace SimpleBrowser
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            CefSettings settings = new CefSettings()
            {
                MultiThreadedMessageLoop = true,
                AcceptLanguageList = "ja,en-US;q=0.9,en;q=0.8", // 受け入れる言語のリスト(カンマで区切る)
                CachePath = @"c:\temp\cache",                   // キャッシュフォルダ
                //UserAgent = @"Mozilla/5.0 (Windows NT 6.2; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36 SimpleBroser/1.0.0.0",
                LogSeverity = LogSeverity.Verbose
            };

            // media-streamを有効にする
            settings.CefCommandLineArgs.Add("enable-media-stream", "1");

            // オプションでプロキシを指定する場合(方式1)
            //CefSharpSettings.Proxy = new ProxyOptions("127.0.0.1", "8080", "", "", "192.168.0.*");

            // コマンドラインでプロキシを指定する場合(方式2)
            //settings.CefCommandLineArgs.Add("proxy-server", "127.0.0.1:8080");
            //settings.CefCommandLineArgs.Add("proxy-bypass-list", "192.168.0.*;*.google.co.jp");

            settings.RegisterScheme(new CefCustomScheme()
            {
                SchemeName = "localfile",   // スキーマ名はlocalfile
                IsCSPBypassing = true,      // CSPはバイパス
                IsSecure = true,            // HTTPSとして扱う
                SchemeHandlerFactory = new Handlers.LocalFileSchemeHandlerFactory()
            });
            //
            Cef.Initialize(settings, false, new BrowserProcessHandler());

            Application.Run(new SimpleBrowserFrame());
        }
    }
}
