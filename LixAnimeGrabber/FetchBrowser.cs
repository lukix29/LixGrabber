using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SerienStreamTo;
using System.Net;
using System.Runtime.InteropServices;

namespace SerienStreamTo
{
    public partial class FetchBrowser : Form
    {
        private LanguageInfo languageInfo = null;

        private string URL = "";

        public FetchBrowser()
        {
            InitializeComponent();
        }

        [STAThread]
        public Task<string> Show(LanguageInfo language)
        {
            languageInfo = language;
            if (!string.IsNullOrEmpty(LixGrabber.Properties.Settings.Default.STO_SESSION_COOKIE))
            {
                Cookie ck = new Cookie("SSTOSESSION", LixGrabber.Properties.Settings.Default.STO_SESSION_COOKIE, "/", "/");
                InternetSetCookie(language.URL, null, ck.ToString());
            }

            webBrowser1.Navigate(language.URL);
            base.Show();
            return Task.Run(() =>
            {
                while (string.IsNullOrEmpty(URL)) ;
                URL = URL.Trim(' ', '\n', '\r', '\t'); ;
                language.Video_URL = URL;
                return URL;
            });
        }

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool InternetSetCookie(string lpszUrlName, string lpszCookieName, string lpszCookieData);

        private void FetchBrowser_FormClosing(object sender, FormClosingEventArgs e)
        {
            webBrowser1.Navigated -= WebBrowser1_Navigated;
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Optimized, false);
        }

        private void FetchBrowser_Load(object sender, EventArgs e)
        {
            //var appName = System.IO.Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            //Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION",
            //      appName, 11001, Microsoft.Win32.RegistryValueKind.DWord);

            WebBrowserHelper.FixBrowserVersion(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

            webBrowser1.Navigated += WebBrowser1_Navigated;
        }

        private void WebBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (languageInfo != null)
            {
                if (e.Url.Host.Contains(languageInfo.Hoster.Domain + "."))
                {
                    var url = e.Url.ToString();
                    if (!string.IsNullOrEmpty(url))
                    {
                        URL = url;
                        this.Close();
                    }
                }
            }
            else
            {
                var str = webBrowser1.DocumentText;
                System.IO.File.WriteAllText(e.Url.ToString().RemoveInvalidFileChars() + ".html", str);
            }
        }
    }

    public class WebBrowserHelper
    {
        public static void FixBrowserVersion()
        {
            string appName = System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetExecutingAssembly().Location);
            FixBrowserVersion(appName);
        }

        public static void FixBrowserVersion(string appName)
        {
            FixBrowserVersion(appName, GetEmbVersion());
        }

        // FixBrowserVersion("<YourAppName>", 9000);
        public static void FixBrowserVersion(string appName, int ieVer)
        {
            FixBrowserVersion_Internal("HKEY_LOCAL_MACHINE", appName + ".exe", ieVer);
            FixBrowserVersion_Internal("HKEY_CURRENT_USER", appName + ".exe", ieVer);
            FixBrowserVersion_Internal("HKEY_LOCAL_MACHINE", appName + ".vshost.exe", ieVer);
            FixBrowserVersion_Internal("HKEY_CURRENT_USER", appName + ".vshost.exe", ieVer);
        }

        public static int GetBrowserVersion()
        {
            // string strKeyPath = @"HKLM\SOFTWARE\Microsoft\Internet Explorer";
            string strKeyPath = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer";
            string[] ls = new string[] { "svcVersion", "svcUpdateVersion", "Version", "W2kVersion" };

            int maxVer = 0;
            for (int i = 0; i < ls.Length; ++i)
            {
                object objVal = Microsoft.Win32.Registry.GetValue(strKeyPath, ls[i], "0");
                string strVal = System.Convert.ToString(objVal);
                if (strVal != null)
                {
                    int iPos = strVal.IndexOf('.');
                    if (iPos > 0)
                        strVal = strVal.Substring(0, iPos);

                    int res = 0;
                    if (int.TryParse(strVal, out res))
                        maxVer = Math.Max(maxVer, res);
                } // End if (strVal != null)
            } // Next i

            return maxVer;
        }

        public static int GetEmbVersion()
        {
            int ieVer = GetBrowserVersion();

            if (ieVer > 9)
                return ieVer * 1000 + 1;

            if (ieVer > 7)
                return ieVer * 1111;

            return 7000;
        } // End Function GetEmbVersion

        // End Sub FixBrowserVersion

        // End Sub FixBrowserVersion

        private static void FixBrowserVersion_Internal(string root, string appName, int ieVer)
        {
            try
            {
                //For 64 bit Machine
                if (Environment.Is64BitOperatingSystem)
                    Microsoft.Win32.Registry.SetValue(root + @"\Software\Wow6432Node\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", appName, ieVer);
                else  //For 32 bit Machine
                    Microsoft.Win32.Registry.SetValue(root + @"\Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", appName, ieVer);
            }
            catch (Exception)
            {
                // some config will hit access rights exceptions
                // this is why we try with both LOCAL_MACHINE and CURRENT_USER
            }
        } // End Sub FixBrowserVersion_Internal

        // End Function GetBrowserVersion
    }
}