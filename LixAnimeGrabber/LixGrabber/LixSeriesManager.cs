using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Threading.Tasks;
using Lix.Downloader;
using Lix.Extensions;
using Newtonsoft.Json;

namespace Lix.SeriesManager
{
    public enum HosterSites : int
    {
        OpenloadHD = 24,
        Rapidvideo = 33,
        StreamangoHD = 34,
        Openload = 124,
        Streamango = 134,
        Vivo = 144
    }

    public enum HosterSitesSort : int
    {
        Rapidvideo = 0,
        OpenloadHD = 1,
        StreamangoHD = 2,
        Openload = 3,
        Streamango = 4,
        Vivo = 5
    }

    public enum Languages : int
    {
        German = 0,
        English = 1,
        English_GermanSub = 2,
        Japanese_EnglishSub = 3
    }

    public enum LanguagesShort : int
    {
        ger = 0,
        eng = 1,
        eng_gersub = 2,
        jap_engsub = 3
    }

    public enum VideoType : int
    {
        None = 0,
        URL = 1,
        Direct = 2,
    }

    public class HosterApi
    {
        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("result")]
        public Dictionary<string, HosterApiInfo> Result { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        public static HosterApi Get(string URL)
        {
            if (HosterConvert.HosterURLs.Any(t => URL.Contains(t.Value)))
            {
                var hi = HosterConvert.HosterURLs.FirstOrDefault(t => URL.Contains(t.Value)).Key;
                using (WebClient wc = new WebClient())
                {
                    var url = _getApiUrl(hi, URL);
                    var str = wc.DownloadString(url);
                    var arr = JsonConvert.DeserializeObject<HosterApi>(str);
                    return arr;
                }
            }
            return null;
        }

        public static string GetID(string url)
        {
            return new Uri(url.Replace("&", "?")).Segments.Last().Trim('/');
        }

        private static string _getApiUrl(HosterSites hoster, string url)
        {
            if (hoster == HosterSites.Openload || hoster == HosterSites.OpenloadHD)
            {
                return "https://api.openload.co/1/file/info?file=" + GetID(url) + "&login=b0bccd7e936e040a&key=UUeFvvab";
            }
            else if (hoster == HosterSites.Streamango || hoster == HosterSites.StreamangoHD)
            {
                return "https://api.fruithosted.net/file/info?file=" + GetID(url) + "&login=yWFkSejsbg&key=sG3O1ut9";
            }
            else if (hoster == HosterSites.Rapidvideo)
            {
                return "https://api.rapidvideo.com/v1/objects.php?ac=info&code=" + GetID(url) + "&apikey=d18e57f70991b9229611a28623bfb4162abf67c06594f3464188a61989da19a0";
            }
            return null;
        }
    }

    public class HosterApiInfo
    {
        [JsonProperty("content_type")]
        public string ContentType { get; set; }

        [JsonProperty("cstatus")]
        public string Cstatus { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("sha1")]
        public string Sha1 { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class HosterConvert
    {
        [JsonIgnore]
        public static Dictionary<string, HosterSites> HosterKeys { get; private set; }

        [JsonIgnore]
        public static Dictionary<HosterSites, string> HosterKeysReverse { get; private set; }

        [JsonIgnore]
        public static Dictionary<HosterSites, string> HosterURLs { get; private set; }

        public static Dictionary<Languages, string> LanguageKeys { get; private set; }

        public static Dictionary<Languages, string> LanguagesKeysFull { get; private set; }

        public static void Initialize()
        {
            HosterURLs = new Dictionary<HosterSites, string>();
            HosterKeys = new Dictionary<string, HosterSites>();
            HosterKeysReverse = new Dictionary<HosterSites, string>();
            LanguageKeys = new Dictionary<Languages, string>();
            LanguagesKeysFull = new Dictionary<Languages, string>();

            foreach (var hm in Enum.GetValues(typeof(HosterSites)))
            {
                var hs = (HosterSites)hm;
                var name = Enum.GetName(typeof(HosterSites), hs);
                HosterKeys.Add(name, hs);
                HosterKeysReverse.Add(hs, name);
                HosterURLs.Add(hs, name.Replace("HD", "").ToLower());
            }

            foreach (var hm in Enum.GetValues(typeof(Languages)))
            {
                var hs = (Languages)hm;
                var name = Enum.GetName(typeof(Languages), hs).Replace("_", "/");
                LanguagesKeysFull.Add(hs, name);
                var ls = (LanguagesShort)(int)hs;
                name = Enum.GetName(typeof(LanguagesShort), ls).Replace("_", "/");
                LanguageKeys.Add(hs, name);
            }
        }

        public static HosterSitesSort ToSort(HosterSites site)
        {
            var name = Enum.GetName(typeof(HosterSites), site);
            return (HosterSitesSort)Enum.Parse(typeof(HosterSitesSort), name);
        }
    }
}

namespace Lix.SeriesManager
{
    public enum SiteType
    {
        _9Anime,
        SerienStream,
    }

    public interface BaseInfo
    {
        VideoType HasVideo { get; }
        int? Index { get; set; }
        bool? IsViewed { get; }

        string ToString();
    }

    public class CancellationToken
    {
        public bool IsRunning { get; set; } = false;
        public bool IsStopped { get; private set; } = false;

        public void Reset(bool running = false)
        {
            IsStopped = false;
            IsRunning = running;
        }

        public void Stop()
        {
            IsStopped = true;
        }
    }

    public class EpisodeInfo : BaseInfo, IEqualityComparer<EpisodeInfo>, IEquatable<EpisodeInfo>
    {
        [JsonIgnore]
        private string _name = null;

        [JsonIgnore]
        public VideoType HasVideo
        {
            get
            {
                var lang = Hosters.FirstOrDefault(t => t.HasVideo > VideoType.None);
                if (lang != null)
                    return lang.HasVideo;
                return VideoType.None;
            }
        }

        public List<HosterInfo> Hosters { get; set; } = new List<HosterInfo>();

        public int? Index { get; set; }

        [JsonIgnore]
        public bool? IsViewed { get { return (ViewDate > DateTime.MinValue); } }

        [JsonProperty("Name")]
        public string Name { get { return _name; } set { _name = value; } }

        [JsonIgnore]
        public SeasonInfo Season { get; set; }

        public string URL { get; set; }

        public DateTime ViewDate { get; set; } = DateTime.MinValue;

        public bool Equals(EpisodeInfo x, EpisodeInfo y)
        {
            return x.URL.Equals(y.URL);
        }

        public bool Equals(EpisodeInfo other)
        {
            return Equals(this, other);
        }

        public HosterInfo GetBest()
        {
            int min = 100;
            HosterInfo item = null;
            foreach (var kvp in Hosters)
            {
                var typ = HosterConvert.ToSort(kvp.Type);
                if ((int)typ < min)
                {
                    item = kvp;
                    min = (int)typ;
                }
            }
            return item;
        }

        public int GetHashCode(EpisodeInfo obj)
        {
            return URL.GetHashCode();
        }

        public Task<string> GetName()
        {
            return Task.Run(() =>
            {
                return GetNameSync();
            });
        }

        public string GetNameSync()
        {
            if (string.IsNullOrWhiteSpace(_name))
            {
                try
                {
                    var lang = Hosters.SelectMany(t => t.Languages).FirstOrDefault(t => t.HasVideo > VideoType.None);
                    if (lang != null)
                    {
                        var arr = HosterApi.Get(lang.Video_URL);
                        _name = arr?.Result?.First().Value?.Name;
                    }
                }
                catch (Exception x)
                {
                }
            }
            return _name;
        }

        public string SetName(string value)
        {
            return _name = value;
        }

        //public string Video_URL { get; set; }

        public override string ToString()
        {
            return string.IsNullOrWhiteSpace(_name) ? "Press 'Fetch Video' to get Name" : _name;
        }
    }

    public class HosterInfo : BaseInfo
    {
        [JsonIgnore]
        public string Domain
        {
            get
            {
                return HosterConvert.HosterURLs[Type];
            }
        }

        [JsonIgnore]
        public EpisodeInfo Episode { get; set; }

        [JsonIgnore]
        public VideoType HasVideo
        {
            get
            {
                var lang = Languages.FirstOrDefault(t => t.HasVideo > VideoType.None);
                if (lang != null)
                    return lang.HasVideo;
                return VideoType.None;
            }
        }

        public int? Index { get => null; set { } }
        public bool? IsViewed { get; } = null;
        public List<LanguageInfo> Languages { get; set; } = new List<LanguageInfo>();
        public HosterSites Type { get; set; }

        public LanguageInfo GetBest()
        {
            int min = 100;
            LanguageInfo item = null;
            foreach (var kvp in Languages)
            {
                if ((int)kvp.Type < min)
                {
                    item = kvp;
                    min = (int)kvp.Type;
                }
            }
            return item;
        }

        public override string ToString()
        {
            if (HosterConvert.HosterKeysReverse.ContainsKey(Type))
            {
                return HosterConvert.HosterKeysReverse[Type];
            }
            return "ERROR " + Type;
        }
    }

    public class LanguageInfo : BaseInfo, IEqualityComparer<LanguageInfo>, IEquatable<LanguageInfo>
    {
        public const int DirectUrlValidityTime = 120;

        [JsonProperty("ldvu")]
        private string direct_video_url = null;

        [JsonProperty("lduu")]
        [System.ComponentModel.DefaultValue(null)]
        private DateTime last_direct_url_update = DateTime.MinValue;

        [JsonIgnore]
        public string Direct_Video_Url
        {
            get
            {
                return (string.IsNullOrEmpty(direct_video_url) ||
               DateTime.Now.Subtract(last_direct_url_update).TotalMinutes > DirectUrlValidityTime)
               ? "" : direct_video_url;
            }
        }

        public string FilePath { get; set; }

        [JsonIgnore]
        public VideoType HasVideo
        {
            get
            {
                if (!string.IsNullOrEmpty(direct_video_url))
                {
                    return VideoType.Direct;
                }
                else if (!string.IsNullOrEmpty(Video_URL))
                {
                    return VideoType.URL;
                }
                return VideoType.None;
            }
        }

        [JsonIgnore]
        public HosterInfo Hoster { get; set; }

        public int? Index { get => null; set { } }

        public bool? IsViewed { get; } = null;

        public Languages Type { get; set; }

        public string URL { get; set; }

        public string Video_URL { get; set; }

        public LanguageInfo CopyFrom(LanguageInfo languageInfo)
        {
            FilePath = languageInfo.FilePath;
            Video_URL = languageInfo.Video_URL;
            direct_video_url = languageInfo.direct_video_url;
            last_direct_url_update = languageInfo.last_direct_url_update;
            Hoster.Episode.ViewDate = languageInfo.Hoster.Episode.ViewDate;

            return this;
        }

        public async Task<string> CreateFileName()
        {
            var name = await Hoster.Episode.GetName();
            if (Path.HasExtension(name))
            {
                return name;
            }

            return (Hoster.Episode.Season.Series.Title.Replace(" ", "_") +
            ".S" + Hoster.Episode.Season.Index.ToString("00") +
            "E" + Hoster.Episode.Index?.ToString("00") +
            "." + name.Replace(" ", "_") +
            ".[" + HosterConvert.LanguageKeys[Type] + "].mp4").
            RemoveInvalidFileChars();
        }

        public string CreateFileNameSync()
        {
            var name = Hoster.Episode.GetNameSync();
            if (Path.HasExtension(name))
            {
                return name;
            }

            return (Hoster.Episode.Season.Series.Title.Replace(" ", "_") +
            ".S" + Hoster.Episode.Season.Index.ToString("00") +
            "E" + Hoster.Episode.Index?.ToString("00") +
            "." + name.Replace(" ", "_") +
            ".[" + HosterConvert.LanguageKeys[Type] + "].mp4").
            RemoveInvalidFileChars();
        }

        public bool Equals(LanguageInfo x, LanguageInfo y)
        {
            return x.URL.Equals(y.URL);
        }

        public bool Equals(LanguageInfo other)
        {
            return Equals(this, other);
        }

        public async Task<string> FetchVideoUrl(Action<int, string, string> action)
        {
            if (File.Exists(FilePath))
            {
                return FilePath;
            }
            else if (!string.IsNullOrEmpty(Hoster.Episode.Name) &&
                File.Exists(Path.Combine(Hoster.Episode.Season.Series.Directory, Hoster.Episode.Name)))
            {
                return FilePath = Path.Combine(Hoster.Episode.Season.Series.Directory, Hoster.Episode.Name);
            }
            else
            {
                FilePath = "";
            }
            if (string.IsNullOrEmpty(direct_video_url) ||
            DateTime.Now.Subtract(last_direct_url_update).TotalMinutes > DirectUrlValidityTime)
            {
                if (HasVideo == VideoType.None)
                {
                    var url = await SeriesLoader.FetchUrl(this);
                    Video_URL = url;
                    Hoster.Episode.Season.Series.Save();
                }
                var t = await SeriesLoader.GetVideoUrl(Video_URL, action);
                if (!string.IsNullOrEmpty(t))
                {
                    direct_video_url = t;
                    last_direct_url_update = DateTime.Now;
                }
                return t;
            }
            return direct_video_url;
        }

        public int GetHashCode(LanguageInfo obj)
        {
            return obj.URL.GetHashCode();
        }

        public async Task<bool> Play(CancellationToken cancellationToken, Action<int, string, string> downloadInfo)
        {
            if (await ResourceDownLoader.Download(ResourceDownloadType.MPV, downloadInfo))
            {
                var proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = "mpv.exe";
                var url = await FetchVideoUrl(downloadInfo);
                //System.Windows.Forms.MessageBox.Show(url);
                if (!string.IsNullOrEmpty(url))
                {
                    proc.StartInfo.Arguments = "\"" + url + "\"";
                    await Task.Run(() =>
                    {
                        proc.Start();
                        proc.WaitForExit();
                    });
                    Hoster.Episode.ViewDate = DateTime.Now;// IsViewed
                    Hoster.Episode.Season.Series.Save();
                    return true;
                }
            }
            return false;
        }

        //public Task<string> FetchUrl(CancellationToken ct)
        //{
        //    var t = SeriesLoader.FetchUrl(this, ct);
        //    t.ContinueWith(ac =>
        //    {
        //        Video_URL = ac.Result;
        //        Hoster.Episode.Season.Series.Save();
        //    });
        //    return t;
        //}
        public override string ToString()
        {
            return HosterConvert.LanguagesKeysFull[Type];
        }
    }

    public class SearchResult : IEqualityComparer<SearchResult>, IEquatable<SearchResult>
    {
        [JsonProperty("description")]
        public string _descr = "";

        [JsonIgnore]
        public string Cover { get; set; }

        public Task<string> Description
        {
            get
            {
                return Task.Run(() =>
                {
                    if (string.IsNullOrEmpty(_descr))
                    {
                        using (WebClient wc = new WebClient())
                        {
                            _descr = wc.DownloadString("https://www1.9anime.to/ajax/film/tooltip/" +
                                new Uri(Link).Segments.Last().Split('.').Last());
                            //_descr = _descr.GetBetween("class=\"desc\"", "</p>");
                        }
                    }
                    return _descr;
                });
            }
        }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonIgnore]
        public SiteType Site
        {
            get
            {
                return Link.Contains("9anime") ? SiteType._9Anime : SiteType.SerienStream;
            }
        }

        [JsonIgnore]
        public string Status { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        public static Task<IEnumerable<SearchResult>> Search(string name, Action<IEnumerable<SearchResult>> action)
        {
            return Task.Run(() =>
            {
                var t1 = Task.Run(() =>
                {
                    return Search9Anime("https://www1.9anime.to/search?keyword=" + name, action);
                });
                var t2 = Task.Run(() =>
                {
                    return SearchSto(name, action);
                });
                Task.WaitAll(t1, t2);
                if (t1.Result != null && t2.Result != null)
                {
                    return t1.Result.Concat(t2.Result);
                }
                else if (t1.Result != null)
                {
                    return t1.Result;
                }
                else if (t2.Result != null)
                {
                    return t2.Result;
                }
                return new List<SearchResult>();
            });
        }

        public bool Equals(SearchResult x, SearchResult y)
        {
            return x.Title.Equals(y.Title);
        }

        public bool Equals(SearchResult other)
        {
            return Equals(this, other);
        }

        public int GetHashCode(SearchResult obj)
        {
            return obj.Title.GetHashCode();
        }

        public override string ToString()
        {
            return Title;
        }

        private static List<SearchResult> Search9Anime(string searchUrl, Action<IEnumerable<SearchResult>> action = null)
        {
            var list = new List<SearchResult>();
            try
            {
                using (var wc = new System.Net.WebClient())
                {
                    var str = wc.DownloadString(searchUrl);
                    var aInfo = str.GetBetween("class=\"film-list", "paging-wrapper");
                    var arr = aInfo.Split(new string[] { "class=\"item" }, StringSplitOptions.RemoveEmptyEntries)
                        .Where(t => t.Length > 8 && t.Contains("class=\"inner"));
                    foreach (var item in arr)
                    {
                        var sr = new SearchResult()
                        {
                            Link = item.GetBetween("href=", "\""),
                            Cover = item.GetBetween("src=", "\""),
                            Title = item.GetBetween("class=\"name\"", "</a>"),
                            //Description = item.GetBetween("status\"", "</div></div>")
                        };
                        list.Add(sr);
                    }
                    action?.Invoke(list);
                    var nextpage = str.GetBetween("Previous</span>", "Next</span>");
                    nextpage = nextpage.GetBetween("href=\"search", "\"");
                    if (!string.IsNullOrWhiteSpace(nextpage) && nextpage.Length > 3)
                    {
                        nextpage = "https://9anime.to/search?" + nextpage;
                        var li = Search9Anime(nextpage, action);
                        return list.Except(li).ToList();
                    }
                    return list;
                }
            }
            catch { }
            return list;
        }

        private static List<SearchResult> SearchSto(string name, Action<IEnumerable<SearchResult>> action)
        {
            try
            {
                using (var wc = new WebClient())
                {
                    wc.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
                    var raw = wc.UploadString("https://s.to/ajax/search", "keyword=" + name);
                    if (!string.IsNullOrWhiteSpace(raw))
                    {
                        var list = JsonConvert.DeserializeObject<List<SearchResult>>(raw);
                        list.RemoveAll(t =>
                        !t.Link.Contains("serie/stream") ||
                        t.Link.Contains("season-") ||
                        t.Link.Contains("episode-"));
                        list.ForEach(t =>
                        {
                            t.Title = t.Title.Replace("<em>", "").Replace("</em>", "");
                            t._descr = t._descr.Replace("<em>", "").Replace("</em>", "");
                        });
                        if (list.Count > 0)
                        {
                            action?.Invoke(list);
                            return list;
                        }
                    }
                }
            }
            catch { }
            return new List<SearchResult>();
        }
    }

    public class SeasonInfo : IEqualityComparer<SeasonInfo>, IEquatable<SeasonInfo>
    {
        public List<EpisodeInfo> Episodes { get; set; } = new List<EpisodeInfo>();
        public int Index { get; set; }

        [JsonIgnore]
        public SeriesInfo Series { get; set; }

        public string URL { get; set; }

        public bool Equals(SeasonInfo x, SeasonInfo y)
        {
            return x.Series.Equals(y.Series) && x.Index == y.Index;
        }

        public bool Equals(SeasonInfo other)
        {
            return Equals(this, other);
        }

        public int GetHashCode(SeasonInfo obj)
        {
            return int.Parse(obj.Series.GetHashCode() + obj.Index.ToString());
        }

        //public Task FetchAll(CancellationToken ct, Action<int> action)
        //{
        //    return Task.Run(() =>
        //    {
        //        try
        //        {
        //            ct.Running = true;
        //            foreach (var epi in Epsiodes)
        //            {
        //                if (ct.Stop) return;
        //                var lang = epi.GetBest().GetBest();
        //                if (!lang.HasVideo)
        //                {
        //                    lang.FetchUrl(ct).Wait();
        //                }
        //                action?.Invoke(epi.Index);
        //            }

        //            ct.Reset();
        //        }
        //        catch (Exception x)
        //        {
        //        }
        //    });
        //}

        public override string ToString()
        {
            return Index.ToString("000");
        }
    }

    public class SeriesInfo : IEqualityComparer<SeriesInfo>, IEquatable<SeriesInfo>
    {
        public string Cover { get; set; }

        public string Directory { get; set; }

        public Dictionary<int, SeasonInfo> Seasons { get; set; } = new Dictionary<int, SeasonInfo>();

        [JsonIgnore]
        public string SelfFileName { get; set; }

        [JsonIgnore]
        public string SiteName
        {
            get { return new Uri(URL).Host; }
        }

        public SiteType SiteType
        {
            get
            {
                return URL.Contains("9anime") ? SiteType._9Anime : SiteType.SerienStream;
            }
        }

        public string Title { get; set; }

        public string URL { get; set; }

        public static Task<SeriesInfo> LoadFull(string path)
        {
            return Task.Run(() =>
            {
                Dictionary<int, HosterSites> dict = new Dictionary<int, HosterSites>
            {
                { 0, HosterSites.OpenloadHD },
                {1, HosterSites.StreamangoHD },
                {2, HosterSites.Openload },
                {3, HosterSites.Streamango },
                {4, HosterSites.Vivo },
                {5, HosterSites.Rapidvideo }
            };
                var json = File.ReadAllText(Path.GetFullPath(path));
                var info = JsonConvert.DeserializeObject<SeriesInfo>(json);
                info.SelfFileName = path;
                info.Title = info.Title.Trim();
                foreach (var season in info.Seasons)
                {
                    season.Value.Series = info;
                    season.Value.Index = season.Key;
                    foreach (var episode in season.Value.Episodes)
                    {
                        episode.Season = season.Value;
                        foreach (var hoster in episode.Hosters)
                        {
                            if (dict.ContainsKey((int)hoster.Type))
                            {
                                hoster.Type = dict[(int)hoster.Type];
                            }
                            hoster.Episode = episode;
                            foreach (var lang in hoster.Languages)
                            {
                                lang.Hoster = hoster;
                            }
                        }
                    }
                }
                return info;
            });
        }

        public void CopyFrom(SeriesInfo seriesInfo)
        {
            try
            {
                if (seriesInfo != null)
                {
                    var arru = seriesInfo.Seasons.Select(t => t.Value)
                    .SelectMany(t => t.Episodes)
                    .SelectMany(t => t.Hosters)
                    .SelectMany(t => t.Languages)
                    .Where(t => t.HasVideo > VideoType.None);

                    foreach (var kvp in seriesInfo.Seasons)
                    {
                        foreach (var epi in kvp.Value.Episodes)
                        {
                            var nm = Seasons[kvp.Key].Episodes.FirstOrDefault(t => t.Index == epi.Index);
                            if (string.IsNullOrEmpty(nm?.Name))
                            {
                                nm.Name = epi.Name;
                            }
                            else if (string.IsNullOrEmpty(epi.Name))
                            {
                                epi.Name = nm.Name;
                            }
                        }
                    }

                    this.Directory = seriesInfo.Directory;

                    foreach (var kvp in arru)
                    {
                        try
                        {
                            var lang = this.Seasons[kvp.Hoster.Episode.Season.Index]
                        .Episodes.FirstOrDefault(t => t.Index == kvp.Hoster.Episode.Index)
                        .Hosters.FirstOrDefault(t => t.Type.Equals(kvp.Hoster.Type))
                        .Languages.FirstOrDefault(t => t.URL.Equals(kvp.URL));

                            lang?.CopyFrom(kvp);
                        }
                        catch { }
                    }
                }
                this.Seasons = this.Seasons.OrderBy(t => t.Key).ToDictionary(k => k.Key, v => v.Value);
            }
            catch
            {
            }
        }

        public bool Equals(SeriesInfo x, SeriesInfo y)
        {
            return x.URL.Equals(y.URL);
        }

        public bool Equals(SeriesInfo other)
        {
            return Equals(this, other);
        }

        public int GetHashCode(SeriesInfo obj)
        {
            return obj.URL.GetHashCode();
        }

        public string GetStandardDirectory(string fold = null)
        {
            fold = (string.IsNullOrEmpty(fold) ?
                Environment.GetFolderPath(Environment.SpecialFolder.MyVideos) :
                Path.GetFullPath(fold));

            return Directory = Path.GetFullPath(Path.Combine(fold, Title.RemoveInvalidPathChars().RemoveInvalidFileChars()));
        }

        public void Save()
        {
            var json = JsonConvert.SerializeObject(this, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });
            var uri = new Uri(URL);
            var file = uri.Segments.Last()
                .RemoveInvalidFileChars("_")
                .Replace(" ", "_").Replace(".", "-")
                .Trim('_', ' ') + ".lixg";
            SelfFileName = file;
            File.WriteAllText(Path.Combine(SeriesLoader.SaveDir, file), json);
        }

        public override string ToString()
        {
            return Title;
        }
    }

    public class SeriesLoader
    {
        public readonly static string SaveDir = Path.GetFullPath(".\\saves");

        private static int dlPid = -1;

        public static int Download_Connections { get; set; } = 5;

        public static string CreateCrawljob(string url, string packageName, string folder = null, string filename = null,
                                      bool autoStart = true, bool forcedStart = true, bool autoConfirm = true, bool enabled = true)
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("#Package");
            sb.AppendLine("packageName=" + packageName);

            sb.AppendLine("#Download Settings");
            sb.AppendLine("#URL");
            sb.AppendLine("text=" + url);
            if (!string.IsNullOrEmpty(folder)) sb.AppendLine("downloadFolder=" + folder);
            if (!string.IsNullOrEmpty(filename)) sb.AppendLine("filename=" + filename);

            sb.AppendLine("#jDownloader Settings");
            sb.AppendLine("enabled=" + enabled.ToString().ToUpper());
            sb.AppendLine("autoStart=" + autoStart.ToString().ToUpper());
            sb.AppendLine("forcedStart=" + forcedStart.ToString().ToUpper());
            sb.AppendLine("autoConfirm=" + autoConfirm.ToString().ToUpper());
            return sb.ToString();
            //File.WriteAllText(Path.GetFullPath(Path.Combine(".\\crawljobs", kvp.Key + ".crawljob")), sb.ToString());
        }

        public static void Dispose()
        {
            KillProcessAndChildrens(dlPid);
        }

        public static bool Download(string path, string file, string url, CancellationToken token, Action<int, string, string> onStdoutReadLine)
        {
            if (token.IsStopped) return false;
            if (ResourceDownLoader.DownloadSync(ResourceDownloadType.Aria2c, onStdoutReadLine))
            {
                using (Process p = new Process())
                {
                    p.StartInfo.Arguments =
                        "-d \"" + path + "\"" +
                        (string.IsNullOrEmpty(file) ? "" : " -o \"" + file + "\"") +
                        " -c" +
                        //" -j" + Download_Connections +
                        " -x" + Download_Connections +
                        " -s" + Download_Connections +
                        " -k10M" +
                        " \"" + url + "\"";
                    p.StartInfo.FileName = "aria2c.exe";
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.RedirectStandardOutput = true;
                    //p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                    p.Start();
                    dlPid = p.Id;
                    token.Reset(true);

                    while (!p.StandardOutput.EndOfStream)
                    {
                        if (token.IsStopped)
                        {
                            KillProcessAndChildrens(p.Id);

                            onStdoutReadLine?.Invoke(-1, "", "");
                            token.IsRunning = false;
                            return false;
                        }
                        var ps = p.StandardOutput.ReadLine();
                        if (!string.IsNullOrWhiteSpace(ps))
                        {
                            ps = ps.Trim();
                            int.TryParse(ps.GetBetweenN("(", "%)"), out int perc);
                            onStdoutReadLine?.Invoke(perc, ps, file);
                        }
                    }
                    onStdoutReadLine?.Invoke(-1, "", "");
                    token.IsRunning = false;
                    return true;
                }
            }
            return false;
        }

        public static Task<bool> DownloadThreaded(string path, string file, string url, CancellationToken token, Action<int, string, string> onStdoutReadLine = null)// string path,string url)
        {
            return Task.Run(() =>
            {
                return Download(path, file, url, token, onStdoutReadLine);
            });
        }

        public static Task<string> FetchUrl(LanguageInfo lang, System.Windows.Forms.Control control = null)
        {
            if (lang.URL.Contains("9anime"))
            {
                return Task.Run(() =>
                {
                    var ainfo = Lix.Anime.VideoLoader.GetAnimeInfo(lang.URL, lang.Hoster.Type);
                    lang.Video_URL = ainfo.Video_URL;
                    return lang.Video_URL;
                });
            }
            else
            {
                return new FetchBrowser().Show(lang);
            }
        }

        public static List<HosterInfo> GetHoster(EpisodeInfo episodeInfo)
        {
            try
            {
                WebClient wc = new WebClient();
                var str = wc.DownloadString(episodeInfo.URL);
                wc.Dispose();

                str = str.GetBetween("inSiteWebStream", "ContentContainerBox");
                var arr = str.Split(new string[] { "<li" }, StringSplitOptions.RemoveEmptyEntries);
                Dictionary<string, Dictionary<string, string>> dict = new Dictionary<string, Dictionary<string, string>>();
                // Parallel.ForEach(arr, (l) =>
                foreach (var l in arr)
                {
                    var line = l.Trim().Replace("\r", "").Replace("\n", "").Replace("  ", "");
                    if (line.StartsWith("class"))
                    {
                        var hoster = line.GetBetween("title=", ">").Replace("Hoster", "").Trim(' ', '"');
                        if (HosterConvert.HosterKeys.ContainsKey(hoster))
                        {
                            var langKey = line.GetBetween("data-lang-key=", "\"");
                            var link = "https://s.to" + line.GetBetween("data-link-target=", "\"");
                            if (dict.ContainsKey(hoster))
                            {
                                if (dict[hoster].ContainsKey(langKey))
                                {
                                    dict[hoster][langKey] = link;
                                }
                                else
                                {
                                    dict[hoster].Add(langKey, link);
                                }
                            }
                            else
                            {
                                dict.Add(hoster, new Dictionary<string, string>() { { langKey, link } });
                            }
                        }
                    }
                }

                List<HosterInfo> hinfo = new List<HosterInfo>();
                foreach (var kvp in dict)
                {
                    var item = new HosterInfo
                    {
                        Type = HosterConvert.HosterKeys[kvp.Key],
                        Episode = episodeInfo
                    };
                    foreach (var lang in kvp.Value)
                    {
                        var li = (Languages)int.Parse(lang.Key);
                        var stolang = new LanguageInfo
                        {
                            Type = li,
                            URL = lang.Value,
                            Hoster = item
                        };
                        item.Languages.Add(stolang);
                    }
                    hinfo.Add(item);
                }
                return hinfo;
            }
            catch (Exception x)
            {
            }
            return null;
        }

        public static Task<List<EpisodeInfo>> GetNewestEpisodeInfo(SeasonInfo info, Action<int, string, string, int> action)
        {
            //fdsfs;
            //Make Anime fetch newest
            if (info.Series.URL.Contains("9anime.to"))
            {
                return GetAnimeNewestEpisodeInfo(info, action);
            }
            else
            {
                return GetStoNewestEpisodeInfo(info);
            }
        }

        public static Task<string> GetSeriesCover(string URL)
        {
            return Task.Run(() =>
            {
                WebClient wcm = new WebClient();
                var html1 = wcm.DownloadString(URL);// VideoLoader.GetWebsite(url);
                wcm.Dispose();
                var str = html1.GetBetween("hosterSiteDirectNav", "itemscope itemtype");
                return html1.GetBetween("seriesCoverBox", "alt").GetBetween("src=", "\"");
            });
        }

        public static Task<SeriesInfo> GetSeriesInfo(string URL, Action<int, string, string, int> action, SeriesInfo stoSeriesInfo = null)
        {
            if (URL.Contains("9anime.to"))
            {
                return GetAnimeSeriesInfo(URL, action, stoSeriesInfo);
            }
            else
            {
                return GetStoSeriesInfo(URL, action, stoSeriesInfo);
            }
        }

        public static string GetVideoTypeIcon(VideoType type)
        {
            switch (type)
            {
                case VideoType.Direct:
                    return "🖭💾";

                case VideoType.URL:
                    return "🖭";

                default:
                    return "";
            }
        }

        public static async Task<string> GetVideoUrl(string url, Action<int, string, string> action)// string path,string url)
        {
            return await Task.Run(() => GetVideoUrlSync(url, action));
        }

        public static string GetVideoUrlSync(string url, Action<int, string, string> action = null)// string path,string url)
        {
            if (url.Contains(HosterConvert.HosterURLs[HosterSites.Rapidvideo]))
            {
                using (WebClient wc = new WebClient())
                {
                    string id = HosterApi.GetID(url);
                    var raw = wc.DownloadString("https://lixmod.tk/api/video/extract/index.php?id=" + id);
                    var json = JsonConvert.DeserializeObject<Dictionary<string, string>>(raw);
                    if (json["url"].StartsWith("http"))
                    {
                        return json["url"];
                    }
                    return null;
                }
            }
            else
            {
                if (ResourceDownLoader.DownloadSync(ResourceDownloadType.YT_DL, action))
                {
                    using (Process p = new Process())
                    {
                        p.StartInfo.Arguments = " -g " + url;
                        p.StartInfo.FileName = "youtube-dl.exe";
                        p.StartInfo.CreateNoWindow = true;
                        p.StartInfo.UseShellExecute = false;
                        p.StartInfo.RedirectStandardOutput = true;
                        p.Start();
                        p.WaitForExit();
                        return p.StandardOutput.ReadToEnd().Trim(' ', '\n', '\r', '\t');
                    }
                }
            }
            return null;
        }

        public static void KillProcessAndChildrens(int pid)
        {
            try
            {
                ManagementObjectSearcher processSearcher = new ManagementObjectSearcher
                  ("Select * From Win32_Process Where ParentProcessID=" + pid);
                ManagementObjectCollection processCollection = processSearcher.Get();

                try
                {
                    Process proc = Process.GetProcessById(pid);
                    if (!proc.HasExited) proc.Kill();
                }
                catch (ArgumentException)
                {
                }

                if (processCollection != null)
                {
                    foreach (ManagementObject mo in processCollection)
                    {
                        KillProcessAndChildrens(Convert.ToInt32(mo["ProcessID"])); //kill child processes(also kills childrens of childrens etc.)
                    }
                }
            }
            catch
            {
            }
        }

        private static SeriesInfo ConvertAnimeInfo(string URL, Lix.Anime.AnimeIDS dict, Action<int, string, string, int> action, SeriesInfo oldSeriesInfo = null)
        {
            SeriesInfo serie = new SeriesInfo()
            {
                URL = URL,
                Title = dict.Title,
                Cover = dict.Cover
            };
            var seas = new SeasonInfo
            {
                Episodes = new List<EpisodeInfo>(),
                Series = serie,
                URL = URL,
                Index = 1,
            };
            serie.Seasons.Add(1, seas);
            foreach (var episode in dict.IDs.SelectMany(t => t.Value))//.First().Value)
            {
                int index = episode.Index;
                if (!seas.Episodes.Any(t => t.Index == index))
                {
                    var epi = new EpisodeInfo
                    {
                        Season = seas,
                        URL = episode.Id,
                        Index = index,
                        Hosters = new List<HosterInfo>()
                    };
                    //epi.SetName(episode.Name);
                    seas.Episodes.Add(epi);
                }
            }
            seas.Episodes = seas.Episodes.OrderBy(t => t.Index).ToList();

            foreach (var key in dict.IDs.Keys)
            {
                foreach (var episode in dict.IDs[key])
                {
                    var Index = episode.Index;
                    var epi = seas.Episodes.FirstOrDefault(t => t.Index == Index);
                    var host = new HosterInfo
                    {
                        Episode = epi,
                        Type = key,
                    };
                    var lang = new LanguageInfo
                    {
                        Hoster = host,
                        Type = Languages.Japanese_EnglishSub,// LanguageKeys[4],
                        URL = URL + (URL.EndsWith("/") ? "" : "/") + episode.Id// string.IsNullOrEmpty(episode.URL) ? episode.Video_URL : episode.URL,
                        //Video_URL = episode.Video_URL,
                    };
                    host.Languages = new List<LanguageInfo> { lang };
                    epi.Hosters.Add(host);
                }
            }
            serie.CopyFrom(oldSeriesInfo);
            return serie;
        }

        private static Task<List<EpisodeInfo>> GetAnimeNewestEpisodeInfo(SeasonInfo info, Action<int, string, string, int> action)
        {
            return Task.Run(() =>
            {
                try
                {
                    var animids = Lix.Anime.VideoLoader.Fetch9AnimeIDs(info.URL, action);

                    var epis = info.Episodes;
                    Dictionary<HosterSites, List<Lix.Anime.AnimeEpisodes>> IDs = new Dictionary<HosterSites, List<Lix.Anime.AnimeEpisodes>>();
                    foreach (var key in animids.IDs.Keys)
                    {
                        var arr = animids.IDs[key].Where(t => !epis.Any(t1 => t1.Index == t.Index)).ToList();
                        if (arr.Count > 0)
                        {
                            IDs.Add(key, arr);
                        }
                        //animids.IDs[key] = arr;
                    }
                    if (IDs.Count > 0)
                    {
                        animids.IDs = IDs;
                        //var animeinfos = LixAnimeGrabber.VideoLoader.FetchVideoHosterLinks(animids, action);
                        var tempInfo = ConvertAnimeInfo(info.URL, animids, action);

                        return tempInfo.Seasons.First().Value.Episodes;
                    }
                }
                catch (Exception x)
                {
                }
                return new List<EpisodeInfo>();
            });
        }

        private static Task<SeriesInfo> GetAnimeSeriesInfo(string URL, Action<int, string, string, int> action, SeriesInfo oldSeriesInfo = null)
        {
            return Task.Run(() =>
            {
                try
                {
                    var fn = Path.GetFileName(URL);
                    if (!string.IsNullOrEmpty(fn) && !fn.Contains("-") && !fn.Contains("."))
                    {
                        URL = URL.Replace(fn, "");
                    }
                    //LixAnimeGrabber.VideoLoader.FetchVideoHosterLinks()
                    var arr = Lix.Anime.VideoLoader.Fetch9AnimeIDs(URL, action);
                    return ConvertAnimeInfo(URL, arr, action, oldSeriesInfo);
                }
                catch (Exception x)
                {
                }
                return null;
            });
        }

        private static Task<List<EpisodeInfo>> GetStoNewestEpisodeInfo(SeasonInfo info)
        {
            return Task.Run(() =>
            {
                try
                {
                    var episode = new EpisodeInfo();

                    using (WebClient wc = new WebClient())
                    {
                        var html = wc.DownloadString(info.URL);
                        var str = html.GetBetween("hosterSiteDirectNav", "itemscope itemtype");
                        var arr = str.Split(new string[] { "<li" }, StringSplitOptions.RemoveEmptyEntries);

                        List<EpisodeInfo> episodes = new List<EpisodeInfo>();
                        foreach (var l in arr)
                        {
                            var line = l.Trim();
                            if (line.Contains("/serie/stream/") && line.Contains("episode"))
                            {
                                var uri = line.GetBetween("href=", "\"");
                                var index = uri.GetLast("-");

                                int i0 = html.IndexOf("seasonEpisodeTitle");
                                int i1 = html.IndexOf(uri, i0);
                                i0 = html.IndexOf("</span>", i1);
                                var subs = html.Substring(i1, i0 - i1);
                                var name = subs.GetBetween("<span");

                                var eps = new EpisodeInfo
                                {
                                    Index = int.Parse(index),
                                    URL = "https://s.to" + uri,
                                    //Season = info
                                };
                                eps.SetName(name);
                                episodes.Add(eps);
                            }
                        }
                        var output = new List<EpisodeInfo>();
                        foreach (var epi in episodes)
                        {
                            if (!info.Episodes.Any(t => t.Equals(epi)))
                            {
                                epi.Hosters = GetHoster(epi);
                                output.Add(epi);
                            }
                        }
                        return output;
                    }
                }
                catch (Exception x)
                {
                    System.Windows.Forms.MessageBox.Show(x.ToString());
                }
                return null;
                //Task End
            });
        }

        private static Task<SeriesInfo> GetStoSeriesInfo(string URL, Action<int, string, string, int> action, SeriesInfo oldSeriesInfo = null)
        {
            var urlarr = URL.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            urlarr = urlarr.Where(t => !t.Contains("staffel-") && !t.Contains("episode-")).ToArray();
            URL = string.Join("/", urlarr);
            URL = URL.Replace(":/", "://");
            return Task.Run(() =>
            {
                try
                {
                    WebClient wcm = new WebClient();
                    var html1 = wcm.DownloadString(URL);// VideoLoader.GetWebsite(url);
                    wcm.Dispose();

                    var str = html1.GetBetween("hosterSiteDirectNav", "itemscope itemtype");
                    var arro = str.Split(new string[] { "<li" }, StringSplitOptions.RemoveEmptyEntries);
                    SeriesInfo serie = new SeriesInfo()
                    {
                        Title = html1.GetBetween("<meta name=\"keywords\" content=", ","),
                        URL = URL,
                        Cover = html1.GetBetween("seriesCoverBox", "alt").GetBetween("src=", "\"")
                    };
                    if (string.IsNullOrWhiteSpace(serie.Title))
                    {
                        serie.Title = URL.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault(t =>
                        {
                            return !t.Contains("https:") && !t.Contains("s.to") && !t.Equals("serie") && !t.Equals("stream");
                        });
                    }
                    Dictionary<int, tempinfo> temp = new Dictionary<int, tempinfo>();
                    int length = 0;// arro.Count(line => line.Contains("/serie/stream/") && line.Contains("title=\"Staffel") && !line.Contains("data-episode-id"));

                    Parallel.ForEach(arro, (l) =>
                    // foreach (var l in arr)
                    {
                        try
                        {
                            var line = l.Trim();
                            if (line.Contains("/serie/stream/") &&
                                line.Contains("title=\"Staffel") &&
                                !line.Contains("data-episode-id"))
                            {
                                var uri = line.GetBetween("href=", "\"");
                                var idxs = uri.GetLast("-");
                                var index = int.Parse(idxs);
                                serie.Seasons.Add(index, new SeasonInfo()
                                {
                                    Index = index,
                                    URL = "https://s.to" + uri,
                                    Series = serie
                                });
                                using (WebClient wc = new WebClient())
                                {
                                    var html = wc.DownloadString("https://s.to" + uri);
                                    str = html.GetBetween("hosterSiteDirectNav", "itemscope itemtype");
                                    var arri = str.Split(new string[] { "<li" }, StringSplitOptions.RemoveEmptyEntries);
                                    temp.Add(index, new tempinfo { Arr = arri, Html = html });// VideoLoader.GetWebsite(url);
                                    length += arri.Count(t => (t.Contains("/serie/stream/") && t.Contains("episode")));
                                    action.Invoke(length, "Loading Episodes " + length.ToString(), serie.Title, length);
                                }
                            }
                        }
                        catch (Exception x)
                        {
                        }
                    });
                    int cnt = 0;
                    //foreach (var kvp in temp)
                    Parallel.ForEach(temp, kvp =>
                     {
                         var html = kvp.Value.Html;
                         str = html.GetBetween("hosterSiteDirectNav", "itemscope itemtype");
                         Parallel.ForEach(kvp.Value.Arr, l =>
                        {
                            try
                            {
                                var line = l.Trim();
                                if (line.Contains("/serie/stream/") && line.Contains("episode"))
                                {
                                    var uri = line.GetBetween("href=", "\"");
                                    var index = uri.GetLast("-");
                                    //var id = line.GetBetween("data-episode-id=", "\"");

                                    int i0 = html.IndexOf("seasonEpisodeTitle");
                                    int i1 = html.IndexOf(uri, i0);
                                    i0 = html.IndexOf("</span>", i1);
                                    var subs = html.Substring(i1, i0 - i1);
                                    var name = subs.GetBetween("<span");
                                    //var seas = line.GetBetween("title=\"Staffel", "Episode").Trim();

                                    var eps = new EpisodeInfo
                                    {
                                        Index = int.Parse(index),
                                        URL = "https://s.to" + uri,
                                        Season = serie.Seasons[kvp.Key]
                                    };
                                    eps.SetName(name);
                                    var hosters = GetHoster(eps);// "https://s.to" + uri);
                                    eps.Hosters = hosters;

                                    serie.Seasons[kvp.Key].Episodes.Add(eps);

                                    action.Invoke(cnt, "Loading Hosters: " + (cnt + 1) + "/" + length, serie.Title, length);
                                    cnt++;
                                }
                            }
                            catch (Exception x)
                            {
                            }
                        });
                         serie.Seasons[kvp.Key].Episodes = serie.Seasons[kvp.Key].Episodes.OrderBy(t => t.Index).ToList();
                     });

                    serie.CopyFrom(oldSeriesInfo);
                    return serie;
                }
                catch (Exception x)
                {
                    System.Windows.Forms.MessageBox.Show(x.ToString());
                }
                return null;
                //Task End
            });
        }

        private class tempinfo
        {
            public string[] Arr { get; set; }
            public string Html { get; set; }
        }
    }
}