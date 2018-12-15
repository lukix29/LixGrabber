// MIT License - Copyright (c) 2016 Can Güney Aksakalli

using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Lix.Extensions;

namespace Lix.HttpServer
{
    public class HTTP_Server : IDisposable
    {
        //private const string tokenKey = "token=";

        private readonly int _port;
        private HttpListener _listener;
        private Task _serverThread;
        private bool _stopServer = false;

        //private string received = "";

        /// <summary>
        /// Construct server with given port.
        /// </summary>
        /// <param name="path">Directory path to serve.</param>
        /// <param name="port">Port of the server.</param>
        public HTTP_Server(int port)
        {
            this._port = port;
        }

        //public delegate void OnURLReceived(HTTP_Server server, string URL);

        //public event OnURLReceived ReceivedURL;
        public Action<string> Action { get; set; }

        public bool IsStarted
        {
            get;
            private set;
        }

        public void Dispose()
        {
            try
            {
                _listener?.Close();
                _serverThread?.Dispose();
            }
            catch { }
        }

        public void Start()
        {
            if (!IsStarted)
            {
                _stopServer = false;
                _serverThread = Task.Run(() => Listen());
                IsStarted = true;
            }
        }

        /// <summary>
        /// Stop server and dispose all functions.
        /// </summary>
        public void Stop()
        {
            _stopServer = true;
            IsStarted = false;
            Dispose();
        }

        //public string Wait(string content, CancellationToken token)
        //{
        //    while (!received.Contains(content))//
        //    {
        //        if (token.Stop) return null;
        //        Task.Delay(100).Wait();
        //    }
        //    return received;
        //}

        private void Listen()
        {
            try
            {
                _listener?.Close();
                _listener = new HttpListener();

                _listener.Prefixes.Add("http://localhost:" + _port + "/");
                _listener.Start();
                while (true)
                {
                    if (_stopServer)
                    {
                        _listener.Close();
                        return;
                    }
                    HttpListenerContext context = _listener.GetContext();
                    Process(context);
                }
            }
            catch (Exception ex)
            {
                if (IsStarted)
                {
                    _listener?.Close();
                    Listen();
                }
            }
        }

        private void Process(HttpListenerContext context)
        {
            string filename = context.Request.Url.ToString();
            //string sessionid = "";

            if (!string.IsNullOrEmpty(filename))
            {
                var request = context.Request;
                if (request.ContentLength64 > 0)
                {
                    string text = "";
                    using (var reader = new System.IO.StreamReader(request.InputStream, request.ContentEncoding))
                    {
                        text = reader.ReadToEnd();
                    }
                    if (Action != null)
                    {
                        Action.Invoke(text);
                        WriteFile(text, context);
                    }
                    else
                    {
                        WriteFile("", context);
                    }
                    //received = text;
                }
            }
        }

        private void WriteFile(string input, HttpListenerContext context)
        {
            if (!string.IsNullOrEmpty(input))
            {
                try
                {
                    byte[] data = System.Text.Encoding.UTF8.GetBytes(input);

                    context.Response.ContentType = "mime";
                    context.Response.ContentLength64 = data.LongLength;
                    context.Response.AddHeader("Access-Control-Allow-Origin", "*");
                    context.Response.AddHeader("Date", DateTime.Now.ToString("r"));
                    context.Response.AddHeader("Last-Modified", DateTime.Now.ToString("r"));

                    context.Response.OutputStream.Write(data, 0, data.Length);

                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    context.Response.OutputStream.Flush();
                }
                catch
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
        }
    }

    public class HTTP_Stream_Server : IDisposable
    {
        //private const string tokenKey = "token=";

        private readonly int _port;
        private HttpListener _listener;
        private Task _serverThread;
        private bool _stopServer = false;

        /// <summary>
        /// Construct server with given port.
        /// </summary>
        /// <param name="path">Directory path to serve.</param>
        /// <param name="port">Port of the server.</param>
        public HTTP_Stream_Server(int port)
        {
            this._port = port;
        }

        //public event OnURLReceived ReceivedURL;
        public Action<string> Action { get; set; }

        public int BufferSize { get; set; } = LiXMath.MIBIBYTE * 5;

        //private string received = "";
        //public delegate void OnURLReceived(HTTP_Server server, string URL);
        public bool IsStarted
        {
            get;
            private set;
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public void Dispose()
        {
            try
            {
                _listener?.Close();
                _serverThread?.Dispose();
            }
            catch { }
        }

        public void Listen()
        {
            try
            {
                _listener?.Close();
                _listener = new HttpListener();

                _listener.Prefixes.Add("http://*:" + _port + "/");
                _listener.Start();
                while (true)
                {
                    if (_stopServer)
                    {
                        _listener.Close();
                        return;
                    }
                    HttpListenerContext context = _listener.GetContext();
                    Process(context);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                if (IsStarted)
                {
                    _listener?.Close();
                    Listen();
                }
            }
        }

        public void Start()
        {
            if (!IsStarted)
            {
                _stopServer = false;
                _serverThread = Task.Run(() => Listen());
                IsStarted = true;
            }
        }

        public void StartSync()
        {
            if (!IsStarted)
            {
                _stopServer = false;
                IsStarted = true;
                Listen();
            }
        }

        /// <summary>
        /// Stop server and dispose all functions.
        /// </summary>
        public void Stop()
        {
            _stopServer = true;
            IsStarted = false;
            Dispose();
        }

        private void Process(HttpListenerContext context)
        {
            string filename = context.Request.Url.ToString();

            if (!string.IsNullOrEmpty(filename))
            {
                filename = filename.Substring(filename.IndexOf("/", filename.IndexOf("http") + 8) + 1);
                filename = filename.Replace("/", "\\").Replace("+", " ");// System.Web.HttpUtility.UrlDecode(filename);
                filename = Path.GetFullPath(filename);
                Console.WriteLine(DateTime.Now.ToLongTimeString() + " " + Path.GetFileName(filename) + " " + context.Request.RemoteEndPoint.Address.ToString());
                WriteFile(filename, context);
            }
        }

        private void WriteFile(string filename, HttpListenerContext context)
        {
            if (File.Exists(filename))
            {
                FileInfo fi = new FileInfo(filename);
                using (var str = fi.OpenRead())
                {
                    try
                    {
                        context.Response.ContentType = "video/mp4";
                        //context.Response.ContentLength64 = fi.Length;
                        context.Response.AddHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filename) + ";");
                        context.Response.AddHeader("Access-Control-Allow-Origin", "*");
                        context.Response.AddHeader("Date", DateTime.Now.ToString("r"));
                        context.Response.AddHeader("Last-Modified", DateTime.Now.ToString("r"));
                        context.Response.KeepAlive = true;
                        context.Response.SendChunked = true;

                        string ip = context.Request.RemoteEndPoint.Address.ToString();
                        int size = BufferSize;
                        byte[] buff = new byte[size];
                        while (str.Length > str.Position)
                        {
                            int r = str.Read(buff, 0, size);
                            if (r <= 0) break;
                            context.Response.OutputStream.Write(buff, 0, r);
                            float perc = ((str.Position / (float)str.Length) * 100f);
                            Console.WriteLine(DateTime.Now.ToLongTimeString() + " " +
                                Path.GetFileName(filename) + " " + perc.ToString("F1") + "% " +
                                "(" + (str.Position / 1000000f).ToString("F1") + "MB/" + (str.Length / 1000000f).ToString("F1") + "MB) " +
                                 ip);
                        }

                        context.Response.OutputStream.Flush();
                        context.Response.StatusCode = (int)HttpStatusCode.OK;
                    }
                    catch (Exception x)
                    {
                        if (x.HResult == -2147467259)
                        {
                            context.Response.OutputStream.Flush();
                        }
                        else
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.Continue;
                        }
                        return;
                    }
                }
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
        }
    }
}