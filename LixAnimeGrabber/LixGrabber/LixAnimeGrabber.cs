using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Lix.Extensions;
using Lix.SeriesManager;
using Newtonsoft.Json;

namespace Lix.Anime
{
    public class AnimeEpisodes
    {
        public string Id { get; set; } = null;
        public int Index { get; set; } = -1;
    }

    public class AnimeIDS
    {
        public string Cover { get; set; }

        public Dictionary<HosterSites, List<AnimeEpisodes>> IDs { get; set; }

        public string Title { get; set; }

        public string URL { get; set; }
    }

    public class AnimeInfo
    {
        [JsonIgnore]
        public string Anime9_ID { get; set; }

        [JsonIgnore]
        public string Cover { get; set; }

        [JsonIgnore]
        public HosterSites Hoster { get; set; }

        [JsonProperty("name")]
        public string Index { get; set; }

        [JsonIgnore]
        public string Name { get; set; }

        [JsonIgnore]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string URL { get; set; }

        [JsonProperty("target"), JsonRequired]
        public string Video_URL { get; set; }

        public override string ToString()
        {
            return Index + ":  " + Name + "  (" + Video_URL + ")";
        }
    }

    public class DownloadInfo
    {
        public string Url { get; set; }
        public VideoInfo VideoInfo { get; set; }
    }

    //public class OpenloadResult
    //{
    //    [JsonProperty("msg")]
    //    public string Msg { get; set; }

    //    [JsonProperty("result")]
    //    public Dictionary<string, OpenloadApiInfo> Result { get; set; }

    //    [JsonProperty("status")]
    //    public string Status { get; set; }

    //    public static string GetID(string url)
    //    {
    //        int idx = url.Trim('/').LastIndexOf('/');
    //        return url.Substring(idx + 1).Trim('/');
    //    }

    //    public class OpenloadApiInfo
    //    {
    //        [JsonProperty("content_type")]
    //        public string ContentType { get; set; }

    //        [JsonProperty("cstatus")]
    //        public string Cstatus { get; set; }

    //        [JsonProperty("id")]
    //        public string Id { get; set; }

    //        [JsonProperty("name")]
    //        public string Name { get; set; }

    //        [JsonProperty("sha1")]
    //        public string Sha1 { get; set; }

    //        [JsonProperty("size")]
    //        public long Size { get; set; }

    //        [JsonProperty("status")]
    //        public string Status { get; set; }
    //    }
    //}

    public class VideoInfo
    {
        public string File { get; set; }
        public int Index { get; set; }
        public string Path { get; set; }
        public string url { get; set; }
    }

    public class VideoLoader
    {
        private string _path = "";
        private bool silent = true;

        public VideoLoader()
        {
        }

        public static long Unix_Timestamp
        {
            get { return (long)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds; }
        }

        public string DirPath
        {
            get { return _path; }
            set { _path = (value.EndsWith("\\")) ? value.Trim('\\') : value; }
        }

        public int Download_Connections { get; set; } = 10;

        public bool killit { get; set; } = false;

        public int Parallel_Downloads { get; set; } = 2;

        public static string CreateFilename(string url)
        {
            string filename = "";
            foreach (var l in url.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (!l.StartsWith("http") && !l.StartsWith("www") && !l.Equals("watch", StringComparison.OrdinalIgnoreCase))
                {
                    filename = l.Remove(l.LastIndexOf('.')).Replace("-", "_");
                    foreach (var c in Path.GetInvalidFileNameChars())
                    {
                        filename = filename.Replace(c, '~');
                    }
                    break;
                }
            }
            return filename;
        }

        //public static string GetEntry(HosterSites key)
        //{
        //    if (VideoSITES.ContainsKey(key))
        //    {
        //        return VideoSITES[key].Trim() + key;
        //    }
        //    return "Unknown" + key;
        //}

        //public static Task<VideoInfo> GetUrl(AnimeInfo info)
        //{
        //    return Task.Run(() =>
        //    {
        //        var t = YtDl_GetLink(info.URL);
        //        VideoInfo vi = new VideoInfo
        //        {
        //            Index = int.Parse(info.Index),
        //            File = getName(info.URL),
        //            Path = "",
        //            url = t
        //        };
        //        return vi;
        //    });
        //}

        //public static Dictionary<HosterSites, List<AnimeInfo>> Fetch(string url, Action<int, string, string, int> action)
        //{
        //    var dict = Fetch9AnimeIDs(url, action);
        //    return FetchVideoHosterLinks(dict, action);
        //}

        //        DownloadInfo dli = new DownloadInfo
        //        {
        //            VideoInfo = new VideoInfo
        //            {
        //                Index = i,
        //                File = getName(url),
        //                Path = DirPath,
        //                url = url
        //            }
        //        };
        //        var t = new Task(() => _download(dli, action));
        //        tasks.Add(t);
        //        t.Start();
        //        if (tasks.Count >= Math.Min(list.Length, Math.Max(0, Parallel_Downloads)))
        //        {
        //            int idx = Task.WaitAny(tasks.ToArray());
        //            tasks.RemoveAt(idx);
        //        }
        //    }
        //    if (tasks.Count > 0)
        //    {
        //        foreach (var tsk in tasks)
        //        {
        //            if (tsk.Status == TaskStatus.Created)
        //                tsk.Start();
        //        }
        //        Task.WaitAll(tasks.ToArray());
        //    }
        //}
        public static AnimeIDS Fetch9AnimeIDs(string url, Action<int, string, string, int> action, params int[] indices)
        {
            //https://www1.9anime.to/watch/that-time-i-got-reincarnated-as-a-slime.yoqz/jk7p83
            //https://www1.9anime.to/ajax/film/servers/yoqz?ts=1541923200

            action?.Invoke(0, "Loading Website", url.GetLast("/"), 1);
            var uri = new Uri(url.Trim('/'));
            var uriid = uri.Segments.Last().GetBetweenN(".");
            var t1 = Task.Run(async () =>
           {
               using (WebClient wc = new WebClient())
               {
                   wc.Proxy = null;
                   wc.DownloadProgressChanged += (o, e) =>
                   {
                       action?.Invoke(e.ProgressPercentage, "Loading Website " + (e.BytesReceived / 1024) + "kiB", url.GetLast("/"), 100);
                   };
                   var raw = await wc.DownloadStringTaskAsync(new Uri("https://www1.9anime.to/ajax/film/servers/" + uriid));

                   return Newtonsoft.Json.Linq.JToken.Parse(raw).Value<string>("html");
               }
           });
            var t2 = Task.Run(() =>
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Proxy = null;
                    return wc.DownloadString(url);
                }
            });
            Task.WaitAll(t1, t2);
            var apiHtml = t1.Result;
            var siteHtml = t2.Result;

            var cover = siteHtml.GetBetween("class=\"thumb col-md-5 hidden-sm hidden-xs", "alt=")
                .GetBetween("src=", "\"");

            var title = siteHtml.GetBetween("og:title", ">").Replace("content=", "").Trim('"');
            title = title.GetBetween("Watch", "in HD").Trim();

            var data = new Dictionary<HosterSites, List<AnimeEpisodes>>();
            using (StringReader sr = new StringReader(apiHtml))
            {
                HosterSites key = 0;
                while (true)
                {
                    var line = sr.ReadLine()?.Trim();
                    if (line == null)
                    {
                        break;
                    }
                    if (line.Contains("class=\"server "))
                    {
                        var tk = sr.ReadLine()?.Trim()?.GetBetween("data-name=", "\"");
                        if (int.TryParse(tk, out int k))
                        {
                            key = (HosterSites)k;
                            if (!Enum.IsDefined(typeof(HosterSites), key))
                            {
                                key = 0;
                            }
                            else
                            {
                                while (!sr.ReadLine().Contains("<ul class=")) ;
                                data.Add(key, new List<AnimeEpisodes>());
                                while (true)
                                {
                                    var li = sr.ReadLine()?.Trim();
                                    if (li == null)
                                    {
                                        break;
                                    }
                                    else if (li.Contains("</div>"))
                                    {
                                        break;
                                    }
                                    else if (li.Contains("<li>"))
                                    {
                                        var epi = new AnimeEpisodes();
                                        while (true)
                                        {
                                            var l0 = sr.ReadLine();
                                            if (l0.Contains("data-id="))
                                            {
                                                epi.Id = l0.GetBetween("data-id=", "\"");
                                            }
                                            else if (l0.Contains("data-base="))
                                            {
                                                epi.Index = int.Parse(l0.GetBetween("data-base=", "\""));
                                            }
                                            if (epi.Index >= 0 && !string.IsNullOrEmpty(epi.Id)) break;
                                            if (li.Equals("</li>")) break;
                                        }
                                        data[key].Add(epi);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            action?.Invoke(-1, "", "", 0);
            return new AnimeIDS
            {
                URL = url,
                IDs = data,
                Title = title,
                Cover = cover
            };
        }

        //public void Download(AnimeInfo[] list, Action<string, DownloadInfo> action)
        //{
        //    List<Task> tasks = new List<Task>();
        //    for (int i = 0; i < list.Length; i++)
        //    {
        //        if (killit) return;
        //        string url = list[i].URL;
        //        //string index = i.ToString();
        public static AnimeIDS Fetch9AnimeIDs(int start, int end, string url, Action<int, string, string, int> action)
        {
            int[] arr = new int[(end - start) + 1];
            for (int i = start; i <= end; i++)
            {
                arr[i - start] = i;
            }
            return Fetch9AnimeIDs(url, action, arr);
        }

        //public static Dictionary<HosterSites, List<AnimeInfo>> FetchVideoHosterLinks(AnimeIDS ids, Action<int, string, string, int> action)
        //{
        //    try
        //    {
        //        Dictionary<HosterSites, List<AnimeInfo>> urls = new Dictionary<HosterSites, List<AnimeInfo>>();

        //        var max = ids.IDs.Aggregate((l, r) => l.Value.Count > r.Value.Count ? l : r).Key;
        //        var all = ids.IDs.Sum(t => t.Value.Count);
        //        var cnt = 0;
        //        var index = 0;
        //        //Parallel.ForEach(ids.IDs.Keys, (key) =>
        //        foreach (var key in ids.IDs.Keys)
        //        {
        //            Parallel.For(0, ids.IDs[key].Count, (i) =>
        //            {
        //                try
        //                {
        //                    var info = GetAnimeInfo(ids.IDs[key][i].Id, key);
        //                    info.Title = ids.Title;
        //                    info.Cover = ids.Cover;
        //                    if (urls.ContainsKey(key))
        //                    {
        //                        urls[key].Add(info);
        //                    }
        //                    else
        //                    {
        //                        urls.Add(key, new List<AnimeInfo> { info });
        //                    }
        //                    if (max == key)
        //                    {
        //                        action?.Invoke(cnt, "Loading Episode " +
        //                            cnt + "/" + ids.IDs[key].Count, ids.Title, all);
        //                        index++;
        //                    }
        //                    cnt++;
        //                }
        //                catch (Exception x)
        //                {
        //                }
        //            });
        //        }
        //        return urls;
        //    }
        //    catch (Exception x)
        //    {
        //    }
        //    return null;
        //}

        public static AnimeInfo GetAnimeInfo(string url, HosterSites key)
        {
            using (var wc = new WebClient())
            {
                var id = new Uri(url).Segments.Last();
                wc.Proxy = null;
                var str = "https://www1.9anime.to/ajax/episode/info?ts=" + Unix_Timestamp + "&id=" + id + "&server=" + (int)key;
                str = wc.DownloadString(str);

                var info = JsonConvert.DeserializeObject<AnimeInfo>(str);
                info.Hoster = key;
                info.Anime9_ID = id;
                if (key == HosterSites.StreamangoHD)
                {
                }
                return info;
            }
        }

        public static string GetWebsite(string url)
        {
            if (Lix.Downloader.ResourceDownLoader.DownloadSync(Lix.Downloader.ResourceDownloadType.Phantom, null))
            {
                var proc = new Process();
                proc.StartInfo.FileName = "phantomjs.exe";
                proc.StartInfo.Arguments = "stdout.js " + url;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;

                proc.StartInfo.RedirectStandardOutput = true;
                proc.Start();
                while (!proc.StandardOutput.EndOfStream)
                {
                    return proc.StandardOutput.ReadToEnd();
                }
            }
            return null;
        }

        public static Task<string> YtDl_Async(string url)
        {
            return Task.Run(() => YtDl_GetLink(url));
        }

        public static string YtDl_GetLink(string url)
        {
            using (Process p = new Process())
            {
                p.StartInfo.FileName = "youtube-dl.exe";
                p.StartInfo.Arguments = "-g " + url;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.Start();
                p.WaitForExit();
                return p.StandardOutput.ReadToEnd();
            }
        }

        private static string getA9EpsiodeList(string url)
        {
            foreach (var l in url.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (!l.StartsWith("http") &&
                    !l.StartsWith("www") &&
                    !l.Equals("watch", StringComparison.OrdinalIgnoreCase))
                {
                    using (WebClient wc = new WebClient())
                    {
                        wc.Proxy = null;
                        var id = l.GetLast(".", "/");
                        return wc.DownloadString("https://www1.9anime.to/ajax/film/servers/" + id + "?ts=" + Unix_Timestamp);
                    }
                }
            }
            return null;
        }

        private static string getName(string item)
        {
            int idx = item.Trim('/').LastIndexOf('/');
            var hostid = item.Substring(idx + 1).Trim('/');

            using (WebClient wc = new WebClient())
            {
                //var vi = new VideoInfo();
                var str = wc.DownloadString("https://api.openload.co/1/file/info?file=" + hostid + "&login=b0bccd7e936e040a&key=UUeFvvab");

                var name = Newtonsoft.Json.Linq.JObject.Parse(str)
                        ["result"].First.First.Value<string>("name").Replace(" ", "_");
                foreach (char c in Path.GetInvalidFileNameChars())
                {
                    name = name.Replace(c, '~');
                }
                return name;
            }
        }

        private void _download(DownloadInfo dli, Action<string, DownloadInfo> action)
        {
            try
            {
                //Console.WriteLine(index + "/" + vi.Index + ": " + url + " - " + vi.File);
                var filepath = Path.Combine(DirPath, dli.VideoInfo.File);

                if (!File.Exists(filepath) ||
                     File.Exists(filepath + ".aria2"))
                {
                    action.Invoke("[Fetching URL of " + dli.VideoInfo.File + "]", dli);
                    dli.Url = YtDl_GetLink(dli.VideoInfo.url).Trim();
                    //Console.WriteLine(vi.Url);
                    action.Invoke("[Starting download of " + dli.VideoInfo.File + "]", dli);
                    bool b = downloadThreaded(dli, action);
                    if (b)
                    {
                        killit = true;
                        return;
                    }
                }
                action.Invoke("[Finished download of " + dli.VideoInfo.File + "]", dli);
            }
            catch (Exception x)
            {
            }
        }

        private bool downloadThreaded(DownloadInfo vi, Action<string, DownloadInfo> onStdoutReadLine)// string path,string url)
        {
            using (Process p = new Process())
            {
                p.StartInfo.Arguments =
                    " -d \"" + vi.VideoInfo.Path + "\"" +
                    " -o \"" + vi.VideoInfo.File + "\"" +
                    " -c" +
                    " -j" + Download_Connections +
                    " -x" + Download_Connections +
                    " -s" + Download_Connections +
                    " -k10M" +
                    " \"" + vi.Url + "\"";
                p.StartInfo.FileName = "aria2c.exe";
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
                        if (ps.StartsWith("["))
                        {
                            onStdoutReadLine.Invoke(ps, vi);
                        }
                    }
                    if (Console.KeyAvailable || killit)
                    {
                        p.Kill();
                        return true;
                    }
                }
            }
            return false;
        }
    }
}