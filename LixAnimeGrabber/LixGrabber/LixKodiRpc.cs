using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Lix.HttpServer;
using Newtonsoft.Json;

namespace Lix.Kodi
{
    public enum KodiInput
    {
        Up,
        Down,
        Left,
        Right,
        Select,
        ShowCodec,
        ShowOSD,
        ShowPlayerProcessInfo,
        Info,
        Home,
        ContextMenu,
        Back,
        Fullscreen
    }

    public enum KodiPlayerStatus : int
    {
        Playing = 1,
        Pause = 2,
        Stopped = 0,
        Error = -1
    }

    public class Kodi : IDisposable
    {
        private HTTP_Stream_Server server;

        public Kodi()
        {
            StreamPort = new Random().Next(8200, 9999);
            server = new HTTP_Stream_Server(StreamPort);
            server.Start();
        }

        public string Address { get; set; }
        public int BufferSize { get { return server.BufferSize; } set { server.BufferSize = value; } }
        public string Password { get; set; }
        public KodiPlayerInfo PlayerInfo { get; private set; } = new KodiPlayerInfo() { Status = KodiPlayerStatus.Error };
        public int StreamPort { get; private set; } = 9123;
        public string UserName { get; set; }

        private int? PlayerID
        {
            get
            {
                if (PlayerInfo.PlayerID == null)
                {
                    var pl = _playerID();
                    if (pl != null)
                    {
                        PlayerInfo.PlayerID = pl;
                    }
                }
                return PlayerInfo.PlayerID;
            }
        }

        public void Dispose()
        {
            server.Dispose();
        }

        public Task<KodiPlayerInfo> GetProperties()
        {
            return Task.Run(() =>
            {
                _properties();
                if (PlayerInfo.Status != KodiPlayerStatus.Error)
                {
                    _playerID();
                    _position();
                }
                return PlayerInfo;
            });
        }

        public Task<bool> Input(KodiInput input)
        {
            return Task.Run(() =>
            {
                try
                {
                    var method = ((input == KodiInput.Fullscreen) ? "GUI.Set" : "Input.") +
                        Enum.GetName(typeof(KodiInput), input);

                    var jsonrpc = new KodiJSON()
                    {
                        Id = 1,
                        Jsonrpc = "2.0",
                        Method = method,
                        Params = (input == KodiInput.Fullscreen) ? new KodiJSON.Kodi_Params { Fullscreen = true } : null
                    };
                    var raw = sendJson(jsonrpc);
                    var json = Newtonsoft.Json.Linq.JObject.Parse(raw);
                    return (json["result"].Equals("OK"));
                }
                catch { }
                return false;
            });
            //var obj = Newtonsoft.Json.Linq.JObject.Parse(raw);
        }

        public Task<KodiPlayerInfo> Mute()
        {
            return Task.Run(() =>
            {
                var jsonrpc = new KodiJSON()
                {
                    Id = 1,
                    Jsonrpc = "2.0",
                    Method = "Application.SetMute",
                    Params = new KodiJSON.Kodi_Params { Mute = !PlayerInfo.Muted }
                };
                var raw = sendJson(jsonrpc);
                try
                {
                    var obj = Newtonsoft.Json.Linq.JObject.Parse(raw);
                    var muted = obj.Value<bool>("result");
                    PlayerInfo.Muted = muted;
                }
                catch { }
                return PlayerInfo;
            });
        }

        public Task<KodiPlayerInfo> Pause()
        {
            return Task.Run(() =>
            {
                if (this.PlayerID != null)
                {
                    var jsonrpc = new KodiJSON()
                    {
                        Id = 1,
                        Jsonrpc = "2.0",
                        Method = "Player.PlayPause",
                        Params = new KodiJSON.Kodi_Params()
                        {
                            PlayerID = this.PlayerID,
                        }
                    };
                    var raw = sendJson(jsonrpc);
                    try
                    {
                        var obj = Newtonsoft.Json.Linq.JObject.Parse(raw);
                        var speed = obj.Last.Last.Value<int>("speed");
                        PlayerInfo.Status = (speed == 0 ? KodiPlayerStatus.Pause : KodiPlayerStatus.Playing);
                        return PlayerInfo;
                    }
                    catch { }
                }
                PlayerInfo.Status = KodiPlayerStatus.Stopped;
                return PlayerInfo;
            });
        }

        public Task<KodiPlayerInfo> Play(string url)
        {
            return Task.Run(async () =>
            {
                try
                {
                    if (!url.StartsWith("http") && File.Exists(url))
                    {
                        url = System.Web.HttpUtility.UrlEncode(url);
                        url = "http://" + HTTP_Stream_Server.GetLocalIPAddress() + ":" + StreamPort + "/" + url;
                    }
                    var jsonrpc = new KodiJSON()
                    {
                        Id = 1,
                        Jsonrpc = "2.0",
                        Method = "Player.Open",
                        Params = new KodiJSON.Kodi_Params() { Properties = null, PlayerID = null, Item = new KodiJSON.Kodi_Item { File = url } }
                    };

                    var raw = sendJson(jsonrpc);
                    var obj = Newtonsoft.Json.Linq.JObject.Parse(raw);
                    if (obj.Value<string>("result").Equals("OK"))
                    {
                        return await GetProperties();
                    }
                    else
                    {
                        PlayerInfo.Status = KodiPlayerStatus.Stopped;
                        return PlayerInfo;
                    }
                }
                catch (Exception x)
                {
                    System.Windows.Forms.MessageBox.Show(x.ToString());
                }
                PlayerInfo.Status = KodiPlayerStatus.Error;
                return PlayerInfo;
            });
        }

        public Task<bool> SetCredentials(string address, string username = null, string password = null)
        {
            return Task.Run(() =>
            {
                try
                {
                    using (var ping = new System.Net.NetworkInformation.Ping())
                    {
                        string addr = address;
                        var port = "8080";
                        if (addr.Contains(":"))
                        {
                            var arr = addr.Split(':');
                            port = arr[1];
                            addr = arr[0];
                        }
                        if (IPAddress.TryParse(addr, out IPAddress ip))
                        {
                            if (ping.Send(addr).Status == System.Net.NetworkInformation.IPStatus.Success)
                            {
                                Address = address;
                                UserName = username;
                                Password = password;
                                GetProperties().Wait();
                                if (PlayerInfo.Status != KodiPlayerStatus.Error)
                                {
                                    return true;
                                }
                                else
                                {
                                    Address = null;
                                    UserName = null;
                                    Password = null;
                                    return false;
                                }
                            }
                        }
                    }
                }
                catch (Exception x)
                {
                    System.Windows.Forms.MessageBox.Show(x.ToString());
                }
                return false;
            });
        }

        public Task<KodiPlayerInfo> Stop(KodiJSON pl = null)
        {
            return Task.Run(() =>
            {
                try
                {
                    if (PlayerID != null)
                    {
                        var jsonrpc = new KodiJSON()
                        {
                            Id = 1,
                            Jsonrpc = "2.0",
                            Method = "Player.Stop",
                            Params = new KodiJSON.Kodi_Params()
                            {
                                PlayerID = PlayerID,
                            }
                        };
                        var raw = sendJson(jsonrpc);
                        PlayerInfo.Status = KodiPlayerStatus.Stopped;
                    }
                }
                catch { }
                return PlayerInfo;
            });
        }

        public Task<KodiPlayerInfo> Volume(int volume = -1)
        {
            return Task.Run(() =>
            {
                try
                {
                    if (volume >= 0)
                    {
                        volume = Math.Max(0, Math.Min(100, volume));
                        var jsonrpc = new KodiJSON()
                        {
                            Id = 1,
                            Jsonrpc = "2.0",
                            Method = "Application.SetVolume",
                            Params = new KodiJSON.Kodi_Params { Volume = volume }
                        };
                        var raw = sendJson(jsonrpc);
                        if (raw != null)
                        {
                            var obj = Newtonsoft.Json.Linq.JObject.Parse(raw);
                            PlayerInfo.Volume = obj.Value<int>("result");
                        }
                    }
                    else
                    {
                        _properties();
                    }
                }
                catch { }
                return PlayerInfo;
            });
        }

        private KodiPlayerInfo _currentItem()
        {
            var jsonrpc = new KodiJSON()
            {
                Id = 1,
                Jsonrpc = "2.0",
                Method = "Player.GetItem",
                Params = new KodiJSON.Kodi_Params()
                {
                    PlayerID = PlayerInfo.PlayerID,
                    Properties = new List<string>()
                     {
                            "title",
                            "album",
                            "artist",
                            "season",
                            "episode",
                            "duration",
                            "showtitle",
                            "tvshowid",
                            "thumbnail",
                            "file",
                            "fanart",
                            "streamdetails"
                     }
                }
            };

            var raw = sendJson(jsonrpc);
            try
            {
                var json = Newtonsoft.Json.Linq.JToken.Parse(raw);
                json = json["result"]["item"];
                var file = json.Value<string>("file");
                var title = json.Value<string>("label");
                if (string.IsNullOrEmpty(title))
                {
                    title = json.Value<string>("title");
                }
                PlayerInfo.FileName = file;
                PlayerInfo.Title = title;
                var thumb = json.Value<string>("thumbnail");
                if (!string.IsNullOrEmpty(thumb))
                {
                    thumb = System.Web.HttpUtility.UrlDecode(thumb);
                    thumb = thumb.Replace("image://", "").Trim('/');
                }
                PlayerInfo.Thumbnail = thumb;
            }
            catch { }
            return PlayerInfo;
        }

        private int? _playerID()
        {
            try
            {
                var jsonrpc = new KodiJSON()
                {
                    Id = 1,
                    Jsonrpc = "2.0",
                    Method = "Player.GetActivePlayers",
                    Params = null
                };
                var raw = sendJson(jsonrpc);
                var res = JsonConvert.DeserializeObject<KodiJSON>(raw);
                if (res.Status != null && res.Status.Length > 0)
                {
                    PlayerInfo.PlayerID = res.Status[0].PlayerID;
                    return res.Status[0].PlayerID;
                }
            }
            catch { }
            return PlayerInfo.PlayerID = null;
        }

        private KodiPlayerInfo _position()
        {
            try
            {
                if (PlayerID != null)
                {
                    var jsonrpc = new KodiJSON()
                    {
                        Id = 1,
                        Jsonrpc = "2.0",
                        Method = "Player.GetProperties",
                        Params = new KodiJSON.Kodi_Params()
                        {
                            PlayerID = PlayerID,
                            Properties = new List<string>()
                     {
                        "percentage",
                        "totaltime",
                        "time"
                     }
                        }
                    };

                    var raw = sendJson(jsonrpc);
                    var pi = JsonConvert.DeserializeObject<KodiPlayerInfo>(raw);
                    PlayerInfo.PositionInfo = pi.PositionInfo;
                    PlayerInfo.Status = (PlayerInfo.Duration.Ticks > 0) ? KodiPlayerStatus.Playing : KodiPlayerStatus.Stopped;
                    if (PlayerInfo.Status > KodiPlayerStatus.Stopped)
                    {
                        _currentItem();
                    }
                }
                else
                {
                    PlayerInfo.Status = KodiPlayerStatus.Stopped;
                }
            }
            catch
            {
            }
            return PlayerInfo;
        }

        private KodiPlayerInfo _properties()
        {
            var jsonrpc = new KodiJSON()
            {
                Id = 1,
                Jsonrpc = "2.0",
                Method = "Application.GetProperties",
                Params = new KodiJSON.Kodi_Params()
                {
                    Properties = new List<string>()
                    {   "muted" ,
                        "volume"
                    }
                }
            };
            var raw = sendJson(jsonrpc);
            if (raw != null)
            {
                try
                {
                    var json = Newtonsoft.Json.Linq.JToken.Parse(raw);
                    json = json["result"];
                    var muted = json.Value<bool>("muted");
                    var volume = json.Value<int>("volume");
                    PlayerInfo.Muted = muted;
                    PlayerInfo.Volume = volume;
                    if (PlayerInfo.Status == KodiPlayerStatus.Error) PlayerInfo.Status = KodiPlayerStatus.Stopped;
                }
                catch { PlayerInfo.Status = KodiPlayerStatus.Error; }
            }
            else PlayerInfo.Status = KodiPlayerStatus.Error;
            return PlayerInfo;
        }

        private string sendJson(object jsonrpc)
        {
            try
            {
                if (string.IsNullOrEmpty(Address)) return null;
                using (var ping = new System.Net.NetworkInformation.Ping())
                {
                    if (ping.Send(Address).Status == System.Net.NetworkInformation.IPStatus.Success)
                    {
                        using (WebClient wc = new WebClient())
                        {
                            wc.Proxy = null;
                            wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                            wc.UseDefaultCredentials = true;
                            wc.Credentials = new NetworkCredential(UserName, Password);
                            string json = JsonConvert.SerializeObject(jsonrpc, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                            json = WebUtility.UrlEncode(json);
                            return wc.DownloadString("http://" + Address + "/jsonrpc?request=" + json);
                        }
                    }
                }
            }
            catch
            { }
            return null;
        }

        //private class MyWebClient : WebClient
        //{
        //    protected override WebRequest GetWebRequest(Uri uri)
        //    {
        //        WebRequest wc = base.GetWebRequest(uri);
        //        wc.Timeout = 10 * 1000;

        //        return wc;
        //    }
        //}
    }

    public class KodiJSON
    {
        public class Kodi_Item
        {
            [JsonProperty("file")]
            public string File { get; set; } = null;
        }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("jsonrpc")]
        public string Jsonrpc { get; set; }

        [JsonProperty("result")]
        public Kodi_Params[] Status { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("params")]
        public Kodi_Params Params { get; set; }

        public class Kodi_Params
        {
            [JsonProperty("fullscreen")]
            public bool? Fullscreen { get; set; } = null;

            [JsonProperty("item")]
            public Kodi_Item Item { get; set; } = null;

            [JsonProperty("items")]
            public List<string> Items { get; set; } = null;

            [JsonProperty("mute")]
            public bool? Mute { get; set; } = null;

            [JsonProperty("playerid")]
            public int? PlayerID { get; set; } = null;

            [JsonProperty("properties")]
            public List<string> Properties { get; set; } = null;

            [JsonProperty("type")]
            public string Type { get; set; } = null;

            [JsonProperty("volume")]
            public int? Volume { get; set; } = null;
        }
    }

    public class KodiPlayerInfo
    {
        //    [JsonIgnore]
        //    public static PlayerResult Error { get { return new PlayerResult() { Status = PlayerStatus.Error }; } }

        //    [JsonIgnore]
        //    public static PlayerResult Stopped { get { return new PlayerResult() { Status = PlayerStatus.Stopped }; } }

        [JsonProperty("id")]
        public long CommandID { get; set; }

        [JsonIgnore]
        public TimeSpan CurrentTime { get { return PositionInfo.Time.Span; } }

        [JsonIgnore]
        public TimeSpan Duration { get { return PositionInfo.Totaltime.Span; } }

        [JsonIgnore]
        public string FileName { get; set; }

        [JsonIgnore]
        public bool Muted { get; set; }

        [JsonIgnore]
        public double Percentage { get { return PositionInfo.Percentage; } }

        [JsonIgnore]
        public int? PlayerID { get; set; }

        [JsonProperty("result")]
        public Position PositionInfo { get; set; } = new Position();

        [JsonIgnore]
        public KodiPlayerStatus Status
        {
            get;
            set;
        }

        [JsonIgnore]
        public string Thumbnail { get; set; }

        [JsonIgnore]
        public string Title { get; set; }

        [JsonIgnore]
        public int Volume { get; set; }

        public class Position
        {
            [JsonProperty("percentage")]
            public double Percentage { get; set; } = 0;

            [JsonProperty("time")]
            public Time Time { get; set; } = new Time();

            [JsonProperty("totaltime")]
            public Time Totaltime { get; set; } = new Time();
        }

        public class Time
        {
            [JsonProperty("hours")]
            public int Hours { get; set; } = 0;

            [JsonProperty("milliseconds")]
            public int Milliseconds { get; set; } = 0;

            [JsonProperty("minutes")]
            public int Minutes { get; set; } = 0;

            [JsonProperty("seconds")]
            public int Seconds { get; set; } = 0;

            [JsonIgnore]
            public TimeSpan Span
            {
                get
                {
                    return new TimeSpan(0, Hours, Minutes, Seconds, Milliseconds);
                }
            }
        }
    }
}