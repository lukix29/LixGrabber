using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using LixGrabber.Properties;
using SerienStreamTo;
using System.Diagnostics;

namespace LixDownloader
{
    public enum DownloadType
    {
        MPV,
        YT_DL,
        Phantom,
        Aria2c,
    }

    public class DownLoader
    {
        private static Dictionary<DownloadType, string[]> fileNames = new Dictionary<DownloadType, string[]>
        {
                { DownloadType.MPV,     new string[]{ "mpv.exe", "libaacs.dll", "libbdplus.dll" } },
                { DownloadType.Phantom, new string[]{ "phantomjs.exe" } },
                { DownloadType.YT_DL,   new string[]{ "youtube-dl.exe" } },
                { DownloadType.Aria2c,  new string[]{ "aria2c.exe" } },
        };

        private static Dictionary<DownloadType, string> urls = new Dictionary<DownloadType, string>()
        {
            { DownloadType.MPV,     "https://filedrop.space/dl?u=643884bce498ba0dbbeee66ecd883c46" },
            { DownloadType.Phantom, "https://bitbucket.org/ariya/phantomjs/downloads/phantomjs-2.1.1-windows.zip" },
            { DownloadType.YT_DL,   "https://yt-dl.org/latest/youtube-dl.exe" },
            { DownloadType.Aria2c,  "https://github.com/aria2/aria2/releases/download/release-1.34.0/aria2-1.34.0-win-64bit-build1.zip" },
        };

        private static Dictionary<DownloadType, string> zipNames = new Dictionary<DownloadType, string>
        {
                { DownloadType.MPV,     "mpv.7z" },
                { DownloadType.Phantom, "phantomjs.zip" },
                { DownloadType.YT_DL,   "youtube-dl.exe" },
                { DownloadType.Aria2c,  "aria2c.zip" },
        };

        public static Task<bool> Download(DownloadType type, Action<int, string, string> downloadInfo)
        {
            return Task.Run(() =>
            {
                return DownloadSync(type, downloadInfo);
            });
        }

        public static bool DownloadSync(DownloadType type, Action<int, string, string> downloadInfo)
        {
            try
            {
                if (!Exists(type))
                {
                    switch (type)
                    {
                        case DownloadType.MPV:
                            bool b = DownloadWebClient(type, downloadInfo);
                            if (b)
                            {
                                return Unzip(type, downloadInfo);
                            }
                            break;

                        case DownloadType.Phantom:
                            b = DownloadWebClient(type, downloadInfo);
                            if (b)
                            {
                                return Unzip(type, downloadInfo);
                            }
                            break;

                        case DownloadType.YT_DL:
                            b = DownloadWebClient(type, downloadInfo);
                            if (b)
                            {
                                return DownloadSync(DownloadType.Phantom, downloadInfo);
                            }
                            break;

                        case DownloadType.Aria2c:
                            b = DownloadWebClient(type, downloadInfo);
                            if (b)
                            {
                                return Unzip(type, downloadInfo);
                            }
                            break;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch (Exception x)
            {
            }
            return false;
        }

        public static bool DownloadWebClient(DownloadType type, Action<int, string, string> onStdoutReadLine)
        {
            var request = (HttpWebRequest)WebRequest.Create(urls[type]);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (var filestream = new FileStream(zipNames[type], FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                if (filestream.Length > 0)
                {
                    request.AddRange(filestream.Length);
                }
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    float length = response.ContentLength;
                    using (var stream = response.GetResponseStream())
                    {
                        float speed = 0f;
                        int aspd = 0;
                        DateTime dt = DateTime.UtcNow;
                        byte[] buffer = new byte[1024];// HTTP_Stream_Server.MEGABYTE];
                        while (true)
                        {
                            int r = stream.Read(buffer, 0, buffer.Length);
                            if (r <= 0) break;
                            aspd += r;
                            filestream.Write(buffer, 0, r);
                            if (DateTime.UtcNow.Subtract(dt).TotalSeconds >= 1.0)
                            {
                                speed = aspd / 1024f;
                                aspd = 0;

                                onStdoutReadLine?.Invoke((int)((filestream.Position / length) * 100),
                                        speed.ToString("F1") + "kiB/s", zipNames[type]);

                                dt = DateTime.UtcNow;
                            }
                        }
                        onStdoutReadLine?.Invoke(-1, "", "");
                        return (filestream.Length > HTTP_Stream_Server.MEGABYTE && filestream.Length >= length);
                    }
                }
            }
        }

        public static bool Unzip(DownloadType type, Action<int, string, string> onStdoutReadLine, params string[] names)
        {
            try
            {
                if (zipNames[type].EndsWith("exe")) return true;
                using (Process p = new Process())
                {
                    p.StartInfo.Arguments = "e -y -r " + zipNames[type] + " " + string.Join(" ", fileNames[type]);
                    p.StartInfo.FileName = "7za.exe";
                    p.StartInfo.WorkingDirectory = Path.GetFullPath(".\\");
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.Start();

                    while (!p.StandardOutput.EndOfStream)
                    {
                        var ps = p.StandardOutput.ReadLine();
                        if (!string.IsNullOrWhiteSpace(ps))
                        {
                            ps = ps.Trim();
                            //int.TryParse(ps.GetBetweenN("(", "%)"), out int perc);
                            onStdoutReadLine?.Invoke(100, ps, string.Join(",", fileNames[type]));
                        }
                    }
                    onStdoutReadLine?.Invoke(-1, "", "");
                    if (names.Count(t => File.Exists(t)) == names.Length)
                    {
                        File.Delete(zipNames[type]);
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
            }
            return false;
        }

        private static bool Exists(DownloadType type)
        {
            return File.Exists(Path.Combine(Path.GetFullPath(".\\"), fileNames[type][0]));
        }
    }
}