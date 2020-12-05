using System;
using System.Linq;
using System.IO;
using System.Text;
using CefSharp;

namespace SimpleBrowser.Handlers
{
    class ResponseFilter : IResponseFilter
    {
        /// <summary>
        /// 埋め込み位置の文字列
        /// ここでは、最初に現れる"<script"の直前に埋め込む。
        /// </summary>
        private static readonly String SEARCH_TARGET = "<script";

        /// <summary>
        /// 埋め込むコード
        /// </summary>
        private static readonly String INJECTED_CODE = "<script>console.log('injected');</script>";

        /// <summary>
        /// 出力ストリームに未書き込みのデータ
        /// </summary>
        private byte[] remainData = new byte[0];

        /// <summary>
        /// コード埋め込み済みのフラグ
        /// </summary>
        private bool bInjected = false;

        /// <summary>
        /// レスポンスで指定されているエンコーディング
        /// </summary>
        private Encoding encoding = Encoding.UTF8;


        public ResponseFilter(String charset = null)
        {
            if (!String.IsNullOrEmpty(charset))
            {
                try
                {
                    this.encoding = Encoding.GetEncoding(charset);
                }
                catch
                {
                    this.encoding = Encoding.UTF8;
                }
            }
        }

        void IDisposable.Dispose()
        {
        }

        FilterStatus IResponseFilter.Filter(Stream dataIn, out long dataInRead, Stream dataOut, out long dataOutWritten)
        {
            if (dataIn != null)
            {
                // 書き出すデータ
                byte[] modifiedContent;

                // 入力ストリームのサイズ
                long lDataSize = dataIn.Length;

                // 出力トリームのサイズ
                long lCapacity = dataOut.Length;

                // 入力ストリームから全て読み込む
                byte[] buffer = new byte[lDataSize];
                dataIn.Read(buffer, 0, (int)lDataSize);

                // 読み取ったサイズを返却
                dataInRead = lDataSize;

                if (!bInjected) 
                {
                    // 検索する文字列をバイト配列に変換する
                    byte[] pattern = encoding.GetBytes(SEARCH_TARGET);

                    // コードを埋め込む位置を探す
                    int injectionPoint = 0;
                    for (int i = 0; i < lDataSize - pattern.Length; ++i)
                    {
                        bool bMatch = true;
                        for (int j = 0; j < pattern.Length; ++ j)
                        {
                            if (pattern[j] != buffer[i + j])
                            {
                                bMatch = false;
                                break;
                            }
                        }

                        if (bMatch)
                        {
                            injectionPoint = i;
                            break;
                        }
                    }

                    // コードを埋め込む位置が見つかった
                    if (injectionPoint != 0)
                    {
                        // 埋め込んだことをマークする。
                        bInjected = true;

                        // 入力データをコード埋め込み位置で2つに分割する。
                        byte[] firstPart = buffer.Take(injectionPoint).ToArray();
                        byte[] secondPart = buffer.Skip(injectionPoint).ToArray();

                        // 出力ストリームに書き込むデータを作成する。
                        // 前回の書き込めなかったデータ＋入力データの前半＋埋め込むデータ＋入力データの後半
                        byte[] code = encoding.GetBytes(INJECTED_CODE);
                        modifiedContent = remainData.Concat(firstPart).Concat(code).Concat(secondPart).ToArray();
                    }
                    // コードを埋め込む位置が見つからなかった
                    else
                    {
                        // 入力データを書き込むデータとする。
                        modifiedContent = buffer;
                    }
                }
                else
                {
                    // 入力データを書き込むデータとする。
                    modifiedContent = buffer;
                }

                // 出力ストリームに編集したコンテンツを書き出す。
                dataOutWritten = Math.Min(lCapacity, modifiedContent.Length);
                dataOut.Write(modifiedContent, 0, (int)dataOutWritten);

                // 書き込むデータが出力ストリームのサイズよりも場合
                if (modifiedContent.Length > lCapacity)
                {
                    // 書き込むない部分を、次回書き込むデータとして保存しておく
                    remainData = modifiedContent.Skip((int)lCapacity).ToArray();

                    // バッファ不足であることを返却する
                    return FilterStatus.NeedMoreData;
                }
                // 全てのデータを出力ストリームに書き込めた場合
                else
                {
                    // 全て書き込めているので、未書き込みデータは空にする。
                    remainData = new byte[0];
                    // 処理が完了したことを返却する。
                    return FilterStatus.Done;
                }
            }
            else
            {
                dataInRead = 0;
                dataOutWritten = 0;
                return FilterStatus.Done;
            }
        }

        bool IResponseFilter.InitFilter()
        {
            return true;
        }
    }
}
