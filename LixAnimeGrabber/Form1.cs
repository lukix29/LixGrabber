using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lix.Extensions;
using Lix.SeriesManager;
using LixGrabber.Properties;

//using PuppeteerSharp;

namespace LixGrabber
{
    public partial class Form1 : Form
    {
        public CancellationToken cancelationToken = new CancellationToken();

        public KodiPlay kodiPlay = null;

        private int dlPercentage = -1;

        private FormDownload formDownload = null;
        private Dictionary<string, SeriesInfo> SavedSeries = new Dictionary<string, SeriesInfo>();

        public Form1()
        {
            InitializeComponent();
            cBL_Episodes.MeasureItem += CBL_MeasureItem;
            cBL_Hoster.MeasureItem += CBL_MeasureItem;
            cBL_Languages.MeasureItem += CBL_MeasureItem;

            cBL_Episodes.DrawItem += CBL_DrawItem;
            cBL_Hoster.DrawItem += CBL_DrawItem;
            cBL_Languages.DrawItem += CBL_DrawItem;

            HosterConvert.Initialize();
        }

        private SeriesInfo SelectedSeries
        {
            get
            {
                if (cBL_Series.SelectedIndex >= 0)
                {
                    return SavedSeries[((SeriesInfo)cBL_Series.SelectedItem).URL];
                }
                else
                {
                    return null;
                }
            }
        }

        public void downloadInfo(int perc = -1)
        {
            downloadInfo(perc, "", "", 100);
        }

        public void downloadInfo(int perc, string sout, string filename)
        {
            downloadInfo(perc, sout, filename, 100);
        }

        public void downloadInfo(int perc, string sout, string filename, int lenght)
        {
            try
            {
                var act = new Action(() =>
                {
                    lbl_Progress.Text = sout + " " + Path.GetFileName(filename);
                    progressBar1.Maximum = lenght;
                    dlPercentage = perc;
                    if (perc >= 0)
                    {
                        if (!this.TopMost)
                        {
                            this.BringToFront();
                        }
                        if (perc == 0) progressBar1.Style = ProgressBarStyle.Marquee;
                        else progressBar1.Style = ProgressBarStyle.Blocks;
                        progressBar1.Value = perc;
                    }
                    else
                    {
                        progressBar1.Value = 0;
                        lbl_Progress.Text = "";
                    }
                });
                if (this.InvokeRequired)
                {
                    this.Invoke(act);
                }
                else
                {
                    act();
                }
            }
            catch { }
        }

        private void AddToDownloader(params LanguageInfo[] infos)
        {
            if (infos.Length > 0)
            {
                if (formDownload?.IsDisposed == null || formDownload.IsDisposed)
                {
                    formDownload = new FormDownload();
                    formDownload.Show();
                }
                formDownload.AddDownload(infos);
            }
        }

        private void addToDownloaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var arr = new List<LanguageInfo>();

            foreach (var li in cBL_Episodes.SelectedItems.Cast<EpisodeInfo>())
            {
                arr.Add(li.GetBest().GetBest());
            }
            AddToDownloader(arr.ToArray());
        }

        private async void bt_Fetch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtB_Url.Text.Contains("://s.to") || txtB_Url.Text.Contains("9anime.to"))
                {
                    var url = new Uri(txtB_Url.Text);
                    var selser = SavedSeries.FirstOrDefault(t => t.Value.URL.Contains(url.AbsolutePath)).Value;
                    var si = await SeriesLoader.GetSeriesInfo(txtB_Url.Text, downloadInfo, selser);
                    dlPercentage = -1;
                    if (SavedSeries.ContainsKey(si.URL))
                    {
                        SavedSeries[si.URL] = si;
                    }
                    else
                    {
                        SavedSeries.Add(si.URL, si);
                        cBL_Series.Items.Add(si);
                    }
                    si.Save();
                    cBL_Series.SelectedIndex = -1;
                    cBL_Series.SelectedIndex = cBL_Series.Items.Cast<SeriesInfo>().ToList().FindIndex(t => t.URL.Equals(si.URL));
                }
            }
            catch (Exception x)
            {
            }
        }

        private void btn_DirectDownload_Click(object sender, EventArgs e)
        {
            if (cancelationToken.IsRunning)
            {
                cancelationToken.Stop();
            }
            if (cBL_Languages.SelectedItem is LanguageInfo item)
            {
                CheckDirectory(SelectedSeries, txtb_output.Text);

                AddToDownloader(item);
            }
        }

        private void btn_Settings_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (fBD_Output.ShowDialog() == DialogResult.OK)
            {
                CheckDirectory(SelectedSeries, fBD_Output.SelectedPath);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (cancelationToken.IsRunning)
                {
                    cancelationToken.Stop();
                }
                else if (cBL_Languages.SelectedItem is LanguageInfo lang)
                {
                    if (lang.HasVideo == VideoType.None)
                    {
                        var url = await SeriesLoader.FetchUrl(lang);
                        if (!string.IsNullOrEmpty(url))
                        {
                            await lang.Hoster.Episode.GetName();
                            txtB_HosterUrl.Text = url;
                            txtB_DirectUrl.Text = url;

                            lang.Hoster.Episode.Season.Series.Save();

                            btn_FetchVideo.ForeColor = Color.LimeGreen;
                            btn_FetchVideo.Text = "Play Video";

                            cBL_Episodes.Refresh();
                            cBL_Hoster.Refresh();
                            cBL_Languages.Refresh();
                        }
                        else
                        {
                            MessageBox.Show("Couldn't fetch Video-URL, please try another Hoster.");
                        }
                    }
                    else
                    {
                        if (await lang.Play(cancelationToken, downloadInfo))
                        {
                            cBL_Episodes.Refresh();
                        }
                        else
                        {
                            MessageBox.Show("Couldn't fetch Video-URL, please try another Hoster.");
                        }
                    }
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            //var url = txtB_Url.Text;
            var selser = SelectedSeries;// SavedSeries.FirstOrDefault(t => t.Value.URL.Equals(url)).Value;
            FetchNewEpisode(selser);
        }

        private void cBL_Episodes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (cBL_Episodes.SelectedIndex >= 0)
            {
                var ep = (EpisodeInfo)cBL_Episodes.SelectedItem;
                ep.ViewDate = (bool)ep.IsViewed ? DateTime.MinValue : DateTime.Now;
                ep.Season.Series.Save();
                cBL_Episodes.Refresh();
            }
        }

        private async void cBL_Episodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cBL_Episodes.SelectedIndex >= 0)
                {
                    var ep = (EpisodeInfo)cBL_Episodes.SelectedItem;
                    await ep.GetName();
                    cBL_Hoster.Items.Clear();
                    cBL_Languages.Items.Clear();
                    foreach (var host in ep.Hosters)
                    {
                        cBL_Hoster.Items.Add(host);
                    }
                    var best = ep.GetBest();
                    cBL_Hoster.SelectedIndex = ep.Hosters.FindIndex(t => t.Type.Equals(best.Type));
                }
                else
                {
                    btn_FetchVideo.Enabled = false;
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }
        }

        private void cBL_Hoster_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var ep = (HosterInfo)cBL_Hoster.SelectedItem;
                cBL_Languages.Items.Clear();
                foreach (var host in ep.Languages)
                {
                    cBL_Languages.Items.Add(host);
                }
                cBL_Languages.SelectedIndex = ep.Languages.FindIndex(t => t.HasVideo > VideoType.None);
                if (cBL_Languages.SelectedIndex < 0) cBL_Languages.SelectedIndex = 0;
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }
        }

        private void cBL_Languages_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var item = (LanguageInfo)cBL_Languages.SelectedItem;

                btn_FetchVideo.Enabled = true;
                btn_FetchVideo.ForeColor = Color.White;
                btn_FetchVideo.Text = item.HasVideo > VideoType.None ? "Play Video" : "Fetch Video";
                txtB_HosterUrl.Text = item.HasVideo > VideoType.None ? item.Video_URL : item.URL;
                if (item.HasVideo > VideoType.None)
                {
                    txtB_DirectUrl.Text = item.Direct_Video_Url;
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }
        }

        private void cBL_saved_series_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var seli = SelectedSeries;
                if (seli != null)
                {
                    cBL_Seasons.Items.Clear();
                    cBL_Episodes.Items.Clear();
                    cBL_Hoster.Items.Clear();
                    cBL_Languages.Items.Clear();

                    pictureBox1.LoadAsync(seli.Cover);
                    txtB_Url.Text = seli.URL;
                    if (!string.IsNullOrEmpty(seli.Directory))
                    {
                        txtb_output.Text = seli.Directory;
                    }
                    else
                    {
                        txtb_output.Text = seli.GetStandardDirectory();
                        seli.Save();
                    }
                    foreach (var seas in seli.Seasons)
                    {
                        cBL_Seasons.Items.Add(seas.Value);
                    }
                    cBL_Seasons.SelectedIndex = cBL_Seasons.Items.Count - 1;
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }
        }

        private void cBL_Seasons_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var seas = (SeasonInfo)cBL_Seasons.SelectedItem;
            seas.Episodes.ForEach(t => { t.ViewDate = (bool)t.IsViewed ? DateTime.MinValue : DateTime.Now; });
            cBL_Episodes.Refresh();
        }

        private void cBL_Seasons_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cBL_Seasons.SelectedIndex >= 0)
                {
                    var seas = (SeasonInfo)cBL_Seasons.SelectedItem;
                    cBL_Episodes.Items.Clear();
                    cBL_Hoster.Items.Clear();
                    cBL_Languages.Items.Clear();
                    for (int i = 0; i < seas.Episodes.Count; i++)
                    {
                        cBL_Episodes.Items.Add(seas.Episodes[i]);
                    }
                    cBL_Episodes.SelectedIndex = cBL_Episodes.Items.Count - 1;
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }
        }

        private void cBL_Series_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var sel = SelectedSeries;
            sel?.Seasons.Select(t => t.Value)
                .SelectMany(seas => seas.Episodes).ToList()
                .ForEach(t => { t.ViewDate = (bool)t.IsViewed ? DateTime.MinValue : DateTime.Now; });
            cBL_Episodes.Refresh();
        }

        private bool check_Text(string data)
        {
            return !data.Equals(txtB_Url.Text) &&
                    (data.Contains("https://s.to/serie/stream/")
                    || (data.Contains("9anime") && data.Contains("watch")));
        }

        private void checkAllForNewEpisodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Parallel.ForEach(SavedSeries.Keys, (key) =>
            {
                FetchNewEpisode(SavedSeries[key]);
            });
        }

        private void CheckDirectory(SeriesInfo series, string path)
        {
            if (series != null)
            {
                var seli = series;
                if (!string.IsNullOrEmpty(path))
                {
                    if (Directory.Exists(path))
                    {
                        txtb_output.Text = path;
                    }
                    else
                    {
                        if (MessageBox.Show("Directory doesn't exist.\r\nCreate Directory?", "Create Directory?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                        {
                            txtb_output.Text = Directory.CreateDirectory(path).FullName;
                        }
                        else
                        {
                            txtb_output.Text = seli.GetStandardDirectory();
                        }
                    }
                }
                else
                {
                    txtb_output.Text = seli.GetStandardDirectory();
                }
                Settings.Default.LAST_PATH = txtb_output.Text;
                Settings.Default.Save();
                seli.Directory = txtb_output.Text;
                seli.Save();
            }
        }

        private void deleteEpisodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ep = (EpisodeInfo)cBL_Episodes.SelectedItem;
            ep.Season.Episodes.RemoveAll(t => t.Index == ep.Index);
            cBL_Episodes.Refresh();
        }

        private void downloadAllEpisodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cBL_Seasons.SelectedItem is SeasonInfo season)
            {
                var arr = new List<LanguageInfo>();
                foreach (var li in season.Episodes)
                {
                    arr.Add(li.GetBest().GetBest());
                }
                AddToDownloader(arr.ToArray());
                //btn_DirectDownload.Text = "Stop";
                //btn_DirectDownload.ForeColor = Color.Red;
                //cancelationToken.Reset();
                //foreach (var epi in season.Episodes)
                //{
                //    if (cancelationToken.IsStopped) break;
                //    var item = epi.GetBest().GetBest();

                //    //this.Text = "Downloading (" + epi.Season.Series.Title + " - " + epi.Name + ")";

                //    string url = await item.FetchVideoUrl(downloadInfo);
                //    if (string.IsNullOrEmpty(url)) continue;
                //    cBL_Episodes.Refresh();

                //    var filename = await item.CreateFileName();
                //    var path = txtb_output.Text;
                //    var fpath = Path.GetFullPath(Path.Combine(path, filename));
                //    var apath = Path.GetFullPath(Path.Combine(path, filename + ".aria2"));
                //    if (!File.Exists(fpath) || File.Exists(apath))
                //    {
                //        await SeriesLoader.DownloadThreaded(path, filename, url, cancelationToken, downloadInfo);
                //    }
                //    if (File.Exists(fpath) && !File.Exists(apath))
                //    {
                //        item.FilePath = fpath;
                //    }
                //    else
                //    {
                //        item.FilePath = string.Empty;
                //    }
                //    item.Hoster.Episode.Season.Series.Save();
                //    cBL_Episodes.Refresh();
                //    //this.Text = "Lix Grabber";
                //}
                //btn_DirectDownload.Text = "Download";
                //btn_DirectDownload.ForeColor = Color.White;
                //progressBar1.Value = 0;
                //cancelationToken.Reset();
            }
        }

        //private async void downloadItem(LanguageInfo item, string path)
        //{
        //    this.Invoke(new Action(() =>
        //    {
        //        btn_DirectDownload.Text = "Stop";
        //        btn_DirectDownload.ForeColor = Color.Red;
        //    }));
        //    cancelationToken.Reset();

        //    string url = await item.FetchVideoUrl(downloadInfo);
        //    if (!string.IsNullOrEmpty(url))
        //    {
        //        this.Invoke(new Action(() =>
        //        {
        //            txtB_DirectUrl.Text = url;
        //            txtB_HosterUrl.Text = item.Video_URL;
        //        }));
        //        var filename = await item.CreateFileName();
        //        await SeriesLoader.DownloadThreaded(path, filename, url, cancelationToken, downloadInfo);
        //        var fpath = Path.GetFullPath(Path.Combine(path, filename));
        //        var apath = Path.GetFullPath(Path.Combine(path, filename + ".aria2"));
        //        if (File.Exists(fpath) && !File.Exists(apath))
        //        {
        //            item.FilePath = fpath;
        //        }
        //        else
        //        {
        //            item.FilePath = string.Empty;
        //        }
        //        item.Hoster.Episode.Season.Series.Save();
        //    }
        //    this.Invoke(new Action(() =>
        //    {
        //        btn_DirectDownload.Text = "Download";
        //        btn_DirectDownload.ForeColor = Color.White;
        //    }));
        //    downloadInfo();
        //}

        private async void FetchNewEpisode(SeriesInfo seriesInfo)
        {
            try
            {
                var seas = seriesInfo.Seasons[seriesInfo.Seasons.Max(t => t.Key)];
                var eps = await SeriesLoader.GetNewestEpisodeInfo(seas, downloadInfo);
                if (eps.Count > 0)
                {
                    foreach (var ep in eps)
                    {
                        ep.Season = seas;
                        seas.Episodes.Add(ep);
                    }
                    seas.Series.Save();
                    this.Invoke(new Action(() =>
                    {
                        cBL_Series.SelectedIndex = -1;
                        cBL_Series.SelectedIndex = cBL_Series.Items.Cast<SeriesInfo>().ToList().FindIndex(t => t.URL.Equals(seas.URL));
                        MessageBox.Show("Added " + eps.Count + "Episode" + (eps.Count == 1 ? "" : "s to ") + seas.Series.Title + ".");
                    }));
                }
            }
            catch (Exception x)
            {
            }
        }

        #region ListBoxes

        private void CBL_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                if (e.Index < 0) return;
                var lstbox = ((ListBox)sender);
                var item = (BaseInfo)lstbox.Items[e.Index];

                var rect = new Rectangle(0, e.Bounds.Y, lstbox.Width, e.Bounds.Height);
                using (var sb = new SolidBrush(e.BackColor))
                {
                    e.Graphics.FillRectangle(sb, rect);
                }
                if (item.Index != null)
                {
                    var inds = ((int)item.Index).ToString("00") + ": ";
                    var size = TextRenderer.MeasureText(inds, e.Font);
                    TextRenderer.DrawText(e.Graphics, inds, e.Font, new Point(rect.X, rect.Y), e.ForeColor, TextFormatFlags.Left | TextFormatFlags.NoPadding);
                    rect.X += size.Width - 8;
                }

                var str = SeriesLoader.GetVideoTypeIcon(item.HasVideo);
                if (!string.IsNullOrEmpty(str))
                {
                    using (var font = new Font(e.Font.FontFamily, 18))
                    {
                        var size = TextRenderer.MeasureText(str, font);

                        TextRenderer.DrawText(e.Graphics, str,
                        font, new Point(rect.X, rect.Y - 5), e.ForeColor, TextFormatFlags.Left | TextFormatFlags.NoPadding);

                        rect.X += size.Width - 10;
                    }
                }
                if (item.IsViewed != null && (bool)item.IsViewed)
                {
                    using (var font1 = new Font(e.Font.FontFamily, 23))
                    {
                        var size = TextRenderer.MeasureText("👁", font1);

                        TextRenderer.DrawText(e.Graphics, "👁", font1,
                        new Point(rect.X - 5, rect.Y - 11), e.ForeColor, TextFormatFlags.Left | TextFormatFlags.NoPadding);

                        rect.X += size.Width - 20;
                    }
                }
                TextRenderer.DrawText(e.Graphics, item.ToString(), e.Font, new Point(rect.X, rect.Y), e.ForeColor, TextFormatFlags.Left | TextFormatFlags.NoPadding);
            }
            catch { }
        }

        private void CBL_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = TextRenderer.MeasureText(((ListBox)sender).Items[e.Index].ToString(), ((ListBox)sender).Font).Height + 1;
        }

        #endregion ListBoxes

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                foreach (var kvp in SavedSeries)
                {
                    kvp.Value.Save();
                }
            }
            catch { }

            SeriesLoader.Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(SeriesLoader.SaveDir))
            {
                Directory.CreateDirectory(SeriesLoader.SaveDir);
            }
            if (!File.Exists(Path.GetFullPath(".\\stdout.js")))
            {
                File.WriteAllText(Path.GetFullPath(".\\stdout.js"), Resources.stdoutjs);
            }
            if (!File.Exists(Path.GetFullPath(".\\Newtonsoft.Json.dll")))
            {
                File.WriteAllBytes(Path.GetFullPath(".\\Newtonsoft.Json.dll"), Resources.Newtonsoft_Json);
            }
            if (!File.Exists(Path.GetFullPath(".\\7za.exe")))
            {
                File.WriteAllBytes(Path.GetFullPath(".\\7za.exe"), Resources._7za);
            }

            LoadSaves();

            txtB_Url.AllowDrop = true;
            txtb_output.AllowDrop = true;

            if (string.IsNullOrEmpty(Settings.Default.LAST_PATH))
            {
                txtb_output.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
            }
            else
            {
                txtb_output.Text = Settings.Default.LAST_PATH;
            }
        }

        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            try
            {
                if (kodiPlay != null)
                {
                    //kodiPlay.Location = new Point(this.Top, this.Right);
                    kodiPlay.BringToFront();
                }
            }
            catch (Exception x) { }
        }

        private void Form1_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                if (kodiPlay != null)
                {
                    kodiPlay.BringToFront();
                }
            }
            catch (Exception x) { }
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            try
            {
                if (kodiPlay != null)
                {
                    //kodiPlay.Location = new Point(this.Top, this.Right);
                    kodiPlay.BringToFront();
                }
            }
            catch (Exception x) { }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://s.to");
        }

        private void lbl_Url_MouseClick(object sender, MouseEventArgs e)
        {
            txtB_HosterUrl.SelectAll();
            txtB_HosterUrl.Copy();
        }

        private void lbl_Url_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (new Uri(txtB_HosterUrl.Text).IsAbsoluteUri)
            {
                System.Diagnostics.Process.Start(txtB_HosterUrl.Text);
            }
        }

        private async void LoadSaves()
        {
            try
            {
                var files = Directory.GetFiles(".\\", "*.lixg");
                if (files.Length > 0)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        var newFile = Path.Combine(SeriesLoader.SaveDir, Path.GetFileName(files[i]));
                        File.Move(files[i], newFile);
                        files[i] = newFile;
                    }
                }
                else
                {
                    files = Directory.GetFiles(SeriesLoader.SaveDir, "*.lixg");
                }
                if (files.Length > 0)
                {
                    foreach (var file in files)
                    {
                        var sinfo = await SeriesInfo.LoadFull(file);
                        if (!SavedSeries.ContainsKey(sinfo.URL))
                        {
                            SavedSeries.Add(sinfo.URL, sinfo);
                        }
                        else
                        {
                            SavedSeries[sinfo.URL].CopyFrom(sinfo);
                            File.Delete(file);
                        }
                        cBL_Series.Items.Add(sinfo);
                    }
                    cBL_Series.SelectedIndex = 0;
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SelectedSeries?.URL))
            {
                System.Diagnostics.Process.Start(SelectedSeries.URL);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (cancelationToken.IsRunning)
                {
                    btn_DirectDownload.Text = "Stop";
                    btn_DirectDownload.ForeColor = Color.Red;
                }
                else
                {
                    btn_DirectDownload.Text = "Download";
                    btn_DirectDownload.ForeColor = Color.Gainsboro;
                }
                if (dlPercentage < 0)
                {
                    this.Text = "Lix Grabber";
                    progressBar1.Visible = false;
                    lbl_Progress.Text = "";
                }
                else
                {
                    progressBar1.Visible = true;
                }

                if (Clipboard.ContainsData(DataFormats.Text))
                {
                    var data = (string)Clipboard.GetData(DataFormats.Text);
                    if (check_Text(data))
                    {
                        txtB_Url.Text = data.Trim();
                        Clipboard.Clear();
                    }
                }
            }
            catch
            {
            }
        }

        private void tSB_KodiPlay_Click(object sender, EventArgs e)
        {
            if (kodiPlay == null) kodiPlay = new KodiPlay(this);
            if (kodiPlay.IsDisposed) kodiPlay = new KodiPlay(this);

            kodiPlay.Show();
            kodiPlay.BringToFront();
            btn_openKodiPlay.Visible = false;
        }

        private void tSB_Search_Click(object sender, EventArgs e)
        {
            new FormSearch(this).ShowDialog();
        }

        private void tSB_Settings_Click(object sender, EventArgs e)
        {
            new FormSettings().ShowDialog();
        }

        private void tSMi_DeleteSeries_Click(object sender, EventArgs e)
        {
            try
            {
                var sel = SelectedSeries;
                SavedSeries.Remove(sel.URL);
                cBL_Series.Items.Remove(sel);
                if (cBL_Series.Items.Count > 0) cBL_Series.SelectedIndex = 0;

                File.Copy(Path.Combine(SeriesLoader.SaveDir, sel.SelfFileName),
                    Path.Combine(SeriesLoader.SaveDir, sel.SelfFileName + ".bak"), true);
                File.Delete(Path.Combine(SeriesLoader.SaveDir, sel.SelfFileName));
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }
        }

        private async void tSMi_FetchAllEpisodes_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedSeries.SiteType != SiteType._9Anime)
                {
                    MessageBox.Show("This feature only works with 9Anime.to!");
                    return;
                }
                var items = cBL_Episodes.Items.Cast<EpisodeInfo>();
                cancelationToken.Reset();

                int i = 1;
                int length = items.Count();
                await Task.Run(() =>
                {
                    cancelationToken.Reset(true);
                    Parallel.ForEach(items, (item, loopstate) =>
                    {
                        if (cancelationToken.IsStopped)
                            loopstate.Break();
                        var lang = item.GetBest().GetBest();
                        if (lang.HasVideo == VideoType.None)
                        {
                            var url = SeriesLoader.FetchUrl(lang).Result;
                            if (!string.IsNullOrEmpty(url))
                            {
                                lang.Video_URL = url;
                                item.GetName().Wait();
                            }
                        }
                        else
                        {
                            item.GetName().Wait();
                        }
                        downloadInfo(i, i + "/" + length, item.Name, length);
                        i++;
                    });
                });
                items.First().Season.Series.Save();
                downloadInfo();
                cBL_Episodes.Refresh();
            }
            catch (Exception x)
            {
            }
            finally
            {
                cancelationToken.Reset();
            }
        }

        private void txtB_DirectUrl_MouseClick(object sender, MouseEventArgs e)
        {
            txtB_DirectUrl.SelectAll();
            txtB_DirectUrl.Copy();
        }

        private void txtB_DirectUrl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (new Uri(txtB_DirectUrl.Text).IsAbsoluteUri)
            {
                System.Diagnostics.Process.Start(txtB_DirectUrl.Text);
            }
        }

        private void txtb_output_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Effect == DragDropEffects.Copy)
            {
                var data = ((string[])e.Data.GetData(DataFormats.FileDrop))[0].ToString();
                if (!string.IsNullOrEmpty(data))
                {
                    data = data.RemoveInvalidPathChars();
                    data = Path.GetFullPath(data);
                    if (Directory.Exists(data))
                    {
                        txtb_output.Text = data;
                    }
                    else
                    {
                        if (MessageBox.Show("Directory doesn't exist.\r\nCreate Directory?", "Create Directory?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                        {
                            txtb_output.Text =
                            Directory.CreateDirectory(data).FullName;
                        }
                    }
                }
            }
        }

        private void txtb_output_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void txtb_output_DragLeave(object sender, EventArgs e)
        {
        }

        private void txtb_output_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Directory.Exists(txtb_output.Text))
            {
                System.Diagnostics.Process.Start(txtb_output.Text);
            }
        }

        private void txtb_output_TextChanged(object sender, EventArgs e)
        {
            //CheckDirectory(txtb_output.Text.Trim());
            Settings.Default.LAST_PATH = txtb_output.Text.RemoveInvalidPathChars();
            Settings.Default.Save();
        }

        private void txtB_Url_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Effect == DragDropEffects.Copy)
            {
                var data = (string)e.Data.GetData(DataFormats.Text);
                txtB_Url.Text = data;
            }
        }

        private void txtB_Url_DragEnter(object sender, DragEventArgs e)
        {
            var data = (string)e.Data.GetData(DataFormats.Text);
            if (check_Text(data))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                lbl_url_invalid.Visible = true;
                e.Effect = DragDropEffects.None;
            }
        }

        private void txtB_Url_DragLeave(object sender, EventArgs e)
        {
            lbl_url_invalid.Visible = false;
        }

        private void txtB_Url_TextChanged(object sender, EventArgs e)
        {
            txtB_Url.Text = txtB_Url.Text.Trim(' ', '/', '"');
        }
    }
}