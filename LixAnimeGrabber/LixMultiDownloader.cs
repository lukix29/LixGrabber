using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using SerienStreamTo;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using LixGrabber;
using System.Runtime.InteropServices;

namespace LixDownlaoder
{
    [Flags]
    public enum ThreadAccess : int
    {
        TERMINATE = (0x0001),
        SUSPEND_RESUME = (0x0002),
        GET_CONTEXT = (0x0008),
        SET_CONTEXT = (0x0010),
        SET_INFORMATION = (0x0020),
        QUERY_INFORMATION = (0x0040),
        SET_THREAD_TOKEN = (0x0080),
        IMPERSONATE = (0x0100),
        DIRECT_IMPERSONATION = (0x0200)
    }

    public class DownloadItem : IEqualityComparer<DownloadItem>, IEquatable<DownloadItem>
    {
        private LixProcess p;

        public DownloadItem(LanguageInfo languageInfo)
        {
            Language = languageInfo;
        }

        public long CurrentSize { get; private set; } = 0;
        public string DownloadInfo { get; set; } = "";
        public long FullSize { get; private set; } = 0;
        public bool IsFinished { get; private set; } = false;
        public bool IsPaused { get; private set; }
        public bool IsRunning { get; set; } = false;
        public bool IsSuccess { get; private set; } = false;
        public LanguageInfo Language { get; set; }
        public long Percentage { get; set; } = 0;
        public long Speed { get; private set; } = 0;

        public double convert(string input)
        {
            double size = 0.0;
            if (input.EndsWith("KiB"))
            {
                double.TryParse(input.Replace("KiB", ""), System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo, out size);
                size *= 1024;
            }
            else if (input.EndsWith("MiB"))
            {
                double.TryParse(input.Replace("MiB", ""), System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo, out size);
                size *= HTTP_Stream_Server.MEGABYTE;
            }
            else if (input.EndsWith("B"))
            {
                double.TryParse(input.Replace("B", ""), out size);
            }
            return size;
        }

        public void Download(MultiDownloader multiDownloader)
        {
            try
            {
                IsRunning = true;
                IsFinished = false;
                IsSuccess = false;
                string url = Language.Direct_Video_Url;
                if (!string.IsNullOrEmpty(url))
                {
                    var item = Language;//.GetBest().GetBest();
                    var path = Language.Hoster.Episode.Season.Series.Directory;

                    var filename = item.CreateFileNameSync();

                    if (multiDownloader.UseAria2c)
                    {
                        downloadInternal(url, path, filename, multiDownloader);
                    }
                    else
                    {
                        downloadInternal1(url, path, filename, multiDownloader);
                    }
                    var fpath = Path.GetFullPath(Path.Combine(path, filename));
                    var apath = Path.GetFullPath(Path.Combine(path, filename + ".aria2"));
                    if (File.Exists(fpath) && !File.Exists(apath))
                    {
                        FileInfo fas = new FileInfo(fpath);
                        if (fas.Length > HTTP_Stream_Server.MEGABYTE)
                        {
                            item.FilePath = fpath;
                            IsSuccess = true;
                        }
                        else
                        {
                            File.Delete(fpath);
                        }
                    }
                    else
                    {
                        item.FilePath = string.Empty;
                    }
                    IsFinished = true;
                    DownloadInfo = "";
                }
            }
            catch (Exception x)
            {
                DownloadInfo = x.Message;
                IsSuccess = false;
            }
            IsRunning = false;
        }

        public bool Equals(DownloadItem x, DownloadItem y)
        {
            return x.Language.Equals(y.Language);
        }

        public bool Equals(DownloadItem other)
        {
            return Equals(this, other);
        }

        public int GetHashCode(DownloadItem obj)
        {
            return obj.Language.GetHashCode();
        }

        public void Pause()
        {
            if (p != null)
            {
                IsPaused = p.Pause(!p.IsPaused);
            }
            else
            {
                IsPaused = !IsPaused;
            }
        }

        public void Reset(bool isFinished = false)
        {
            Stop();
            IsFinished = isFinished;
            IsRunning = false;
            IsSuccess = false;
            Percentage = 0;
            DownloadInfo = "";
        }

        public void Start(MultiDownloader multiDownloader)
        {
            Task.Run(() => Download(multiDownloader));
        }

        public void Stop()
        {
            if (IsSuccess)
            {
                IsFinished = true;
            }
            IsRunning = false;
            if (p != null)
            {
                SeriesLoader.KillProcessAndChildrens(p.Id);
            }
        }

        public override string ToString()
        {
            var title = Language.Hoster.Episode.Season.Series.Title;
            return title + " - " +
                "S" + Language.Hoster.Episode.Season.Index.ToString("00") +
                "E" + Language.Hoster.Episode.Index?.ToString("000") +
                " (" + HosterConvert.LanguageKeys[Language.Type] + ")" +
                (IsSuccess ? "  -  [ completed ]" : "  -  " + DownloadInfo);
            //" (" + Episode.Name.Replace(title, "").Trim() + ")";
        }

        private void downloadInternal(string url, string path, string filename, MultiDownloader multiDownloader)
        {
            p = new LixProcess();

            p.StartInfo.Arguments =
                    " -d \"" + path + "\"" +
                    (string.IsNullOrEmpty(filename) ? "" : " -o \"" + filename + "\"") +
                    " -c" +
                    //" --enable-rpc=true" +
                    " --file-allocation=none" +
                    //" -j" + Download_Connections +
                    " -x" + multiDownloader.Download_Connections +
                    " -s" + multiDownloader.Download_Connections +
                    " -k10M" +
                    " \"" + url + "\"";
            p.StartInfo.FileName = "aria2c.exe";
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            //p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            p.Start();
            while (!p.StandardOutput.EndOfStream)
            {
                if (multiDownloader.IsStopped)
                {
                    SeriesLoader.KillProcessAndChildrens(p.Id);
                    break;
                }
                try
                {
                    var ps = p.StandardOutput.ReadLine();
                    if (!string.IsNullOrWhiteSpace(ps))
                    {
                        ps = ps.Trim();
                        int.TryParse(ps.GetBetweenN("(", "%)"), out int perc);

                        CurrentSize = (long)convert(ps.GetBetweenN(" ", "/"));
                        FullSize = (long)convert(ps.GetBetweenN("/", "("));
                        Speed = (long)convert(ps.GetBetween("DL", " "));

                        if (perc > 0) Percentage = perc;
                        if (!ps.StartsWith("-"))
                        {
                            DownloadInfo = (ps.Contains("#") ? ("[" + ps.Substring(ps.IndexOf(" ", ps.IndexOf("#")) + 1)) : ps);
                        }
                    }
                }
                catch (Exception x)
                {
                }
            }
        }

        private void downloadInternal1(string url, string path, string filename, MultiDownloader multiDownloader)
        {
            try
            {
                var httpWebRequest = HttpWebRequest.CreateHttp(url);
                //httpWebRequest.Method = "GET";
                var filePath = Path.Combine(path, filename);
                var fi = new FileInfo(filePath);
                FullSize = GetLength(url);
                if (fi.Exists)
                {
                    if (fi.Length >= FullSize) return;
                    httpWebRequest.AddRange(fi.Length);
                }
                using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))//, FileShare.ReadWrite))
                {
                    using (var httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse)
                    {
                        using (var stream = httpWebResponse.GetResponseStream())
                        {
                            var dt = DateTime.UtcNow;
                            var buffsize = HTTP_Stream_Server.MEGABYTE;
                            var buffer = new byte[buffsize];
                            var speed = 0;
                            while (!multiDownloader.IsStopped)
                            {
                                if (IsPaused)
                                {
                                    buffsize = 1000;
                                    Task.Delay(1000).Wait();
                                }
                                else
                                {
                                    buffsize = buffer.Length;
                                }
                                var l = stream.Read(buffer, 0, buffsize);
                                if (l <= 0 || multiDownloader.IsStopped)
                                    break;
                                fileStream.Write(buffer, 0, l);
                                //if (fileStream.Position >= FullSize)
                                //    break;
                                speed += l;
                                if (DateTime.UtcNow.Subtract(dt).TotalMilliseconds >= 1000)
                                {
                                    CurrentSize = fileStream.Length;
                                    Speed = speed;

                                    Percentage = (int)(fileStream.Length / (float)FullSize * 100f);
                                    DownloadInfo = "[" +
                                       fileStream.Length.SizeSuffix(0) + "/" + FullSize.SizeSuffix(0) +
                                        "(" + Percentage + "%) " +
                                        "CN:" + multiDownloader.Download_Connections +
                                        " DL:" + speed.SizeSuffix(1) +
                                        " ETA: " + TimeSpan.FromSeconds((FullSize - fileStream.Length) / speed).ToString(@"hh\:mm\:ss") +
                                        "]";
                                    speed = 0;
                                    dt = DateTime.UtcNow;
                                }
                            }
                        }//.CopyTo(fileStream);
                    }
                }
            }
            catch (Exception x)
            {
            }
        }

        private long GetLength(string url)
        {
            var httpWebRequest = HttpWebRequest.CreateHttp(url);
            using (var httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse)
            {
                return httpWebResponse.ContentLength;
            }
        }
    }

    public class LixProcess : Process
    {
        public bool IsPaused { get; private set; } = false;

        public bool Pause(bool pause = true)
        {
            if (pause)
            {
                IsPaused = SuspendProcess();
            }
            else
            {
                IsPaused = !ResumeProcess();
            }
            return IsPaused;
        }

        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool CloseHandle(IntPtr handle);

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);

        [DllImport("kernel32.dll")]
        private static extern int ResumeThread(IntPtr hThread);

        [DllImport("kernel32.dll")]
        private static extern uint SuspendThread(IntPtr hThread);

        private bool ResumeProcess()
        {
            if (ProcessName == string.Empty)
                return false;

            foreach (ProcessThread pT in Threads)
            {
                IntPtr pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);

                if (pOpenThread == IntPtr.Zero)
                {
                    continue;
                }

                var suspendCount = 0;
                do
                {
                    suspendCount = ResumeThread(pOpenThread);
                } while (suspendCount > 0);

                CloseHandle(pOpenThread);
            }
            return true;
        }

        private bool SuspendProcess()
        {
            if (ProcessName == string.Empty)
                return false;

            foreach (ProcessThread pT in Threads)
            {
                IntPtr pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);

                if (pOpenThread == IntPtr.Zero)
                {
                    continue;
                }

                SuspendThread(pOpenThread);

                CloseHandle(pOpenThread);
            }
            return true;
        }
    }

    public class MultiDownloader
    {
        private int maxparDL = 1;

        public long CurrrentSize
        {
            get { return Items.Sum(t => t.CurrentSize); }
        }

        public int Download_Connections { get; set; } = 1;

        public bool IsStopped { get; private set; } = false;

        public List<DownloadItem> Items { get; set; } = new List<DownloadItem>();

        public int Max_Parallel_Downloads { get { return maxparDL; } set { maxparDL = Math.Max(1, Math.Min(10, value)); } }

        public long Size
        {
            get { return Items.Sum(t => (t.FullSize < long.MaxValue ? t.FullSize : 0)); }
        }

        public long Speed
        {
            get { return Items.Sum(t => t.Speed); }
        }

        public bool UseAria2c { get; set; } = true;

        public bool CheckDownloads(Form formCallback)
        {
            while (!IsStopped)
            {
                try
                {
                    if (IsStopped) return false;
                    var runCnt = Items.Count(t => t.IsRunning);
                    if (runCnt < maxparDL)
                    {
                        DownloadItem di = null;
                        for (int i = 0; i < (maxparDL - runCnt); i++)
                        {
                            if (IsStopped) return false;
                            var it = Items.FirstOrDefault(t => !t.IsFinished && !t.IsRunning && !t.IsSuccess);
                            if (it == null)
                            {
                                continue;
                            }
                            else if (di != null)
                            {
                                if (di.Equals(it))
                                {
                                    continue;
                                }
                            }
                            var task = it.Language.FetchVideoUrl(null);
                            task.Wait();
                            if (string.IsNullOrEmpty(task.Result))
                            {
                                it.Reset(true);
                            }
                            else
                            {
                                it.IsRunning = true;
                                it.Start(this);
                            }
                            di = it;
                        }
                        if (IsStopped) return false;
                    }
                    else if (maxparDL > runCnt)
                    {
                        Items.LastOrDefault(t => t.IsRunning)?.Pause();
                    }
                    Task.Delay(1000).Wait();
                }
                catch (Exception x)
                {
                }
            }
            return true;
        }

        //    HttpWebRequest httpWebRequest = HttpWebRequest.Create(fileUrl) as HttpWebRequest;
        //    httpWebRequest.Method = "GET";
        //                httpWebRequest.AddRange(readRange.Start, readRange.End);
        //                using (HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse)
        //                {
        //                    String tempFilePath = Path.GetTempFileName();
        //                    using (var fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write, FileShare.Write))
        //                    {
        //                        httpWebResponse.GetResponseStream().CopyTo(fileStream);
        //tempFilesDictionary.TryAdd((int) index, tempFilePath);
        //                    }
        //                }
        public Task<bool> Start(Form formCallback)
        {
            if (LixDownloader.DownLoader.DownloadSync(LixDownloader.DownloadType.Aria2c, null))
            {
                return Task.Run(() => CheckDownloads(formCallback));
            }
            return null;
            //    List<Task> list = new List<Task>();
            //for (int i = 0; i < Items.Count; i++)
            //{
            //    if (cancellationToken.IsStopped) return;
            //    if (!Items[i].IsFinished)
            //    {
            //        Items[i].DownloadInfo = "Starting Download...";
            //        string url = await Items[i].Language.FetchVideoUrl(null);
            //        var t = Items[i].Download(url, cancellationToken);
            //        list.Add(t);
            //        if (list.Count >= maxparDL)
            //        {
            //            int idx = await Task.Run(() => Task.WaitAny(list.ToArray()));
            //            list.RemoveAt(idx);
            //        }
            //    }
            //}
            //if (list.Count > 0)
            //{
            //    await Task.Run(() => Task.WaitAll(list.ToArray()));
            //}
        }

        public Task Stop()
        {
            IsStopped = true;
            return Task.Run(() =>
            {
                foreach (var item in Items)
                {
                    item.Stop();
                }
            });
        }
    }
}