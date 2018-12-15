using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Lix.Downloader;
using Lix.Extensions;
using Lix.SeriesManager;
using LixGrabber.Properties;

namespace LixGrabber
{
    public partial class FormDownload : Form
    {
        public MultiDownloader downloader = new MultiDownloader();

        private Dictionary<HosterSites, Icon> icons = new Dictionary<HosterSites, Icon>
        {
            { HosterSites.Openload, Resources.openload },
            { HosterSites.OpenloadHD, Resources.openload },
            { HosterSites.Streamango, Resources.streamangoo },
            { HosterSites.StreamangoHD, Resources.streamangoo },
            { HosterSites.Rapidvideo, Resources.rapidvideo },
            { HosterSites.Vivo, Resources.vivo },
        };

        public FormDownload()
        {
            InitializeComponent();
            lstB_Main.DrawItem += listBox1_DrawItem;
        }

        public void AddDownload(IEnumerable<LanguageInfo> info)
        {
            foreach (var episode in info)
            {
                var item = new DownloadItem(episode);
                if (!downloader.Items.Contains(item))
                {
                    downloader.Items.Add(item);
                    lstB_Main.Items.Add(item);
                }
            }
        }

        private void btn_pause_Click(object sender, EventArgs e)
        {
            foreach (var item in downloader.Items)
            {
                item.Pause();
            }
            btn_pause.Text = downloader.Items.Any(t => t.IsPaused) ? "Resume" : "Pause";
        }

        private async void btn_Start_Click(object sender, EventArgs e)
        {
            try
            {
                downloader.Items.ForEach(t => t.Reset());
                btn_Stop.Visible = btn_pause.Visible = true;
                btn_Start.Visible = false;
                var b = await downloader.Start(this);
                btn_Start.Visible = true;
                btn_Stop.Visible = btn_pause.Visible = false;
            }
            catch (Exception x)
            {
            }
        }

        private async void btn_Stop_Click(object sender, EventArgs e)
        {
            await downloader.Stop();
            btn_Stop.Visible = false;
        }

        private void cB_useAria_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.USE_ARIA2C_DOWNLOADER = downloader.UseAria2c = cB_useAria.Checked;
            Settings.Default.Save();
        }

        private async void FormDownload_FormClosing(object sender, FormClosingEventArgs e)
        {
            await downloader.Stop();
        }

        private void FormDownload_Load(object sender, EventArgs e)
        {
            cB_useAria.Checked = downloader.UseAria2c = Settings.Default.USE_ARIA2C_DOWNLOADER;
            nUD_ParallelDLs.Value = downloader.Max_Parallel_Downloads = Settings.Default.PARALLEL_DOWNLOADS;
            nUD_consPerDL.Value = downloader.Download_Connections = Settings.Default.CONENCTIONS_PER_DOWNLOAD;
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                if (!lstB_Main.ClientRectangle.IntersectsWith(e.Bounds)) return;
                e.DrawBackground();
                if (e.Index >= 0)
                {
                    var item = downloader.Items[e.Index];
                    //if (!item.IsRunning) return;
                    var w = item.IsSuccess ? e.Bounds.Width : ((item.Percentage / 100f) * e.Bounds.Width);
                    e.Graphics.FillRectangle(item.IsFinished ? Brushes.DarkBlue : Brushes.DarkGreen, e.Bounds.X, e.Bounds.Y + 1, w, e.Bounds.Height - 2);

                    var rect = e.Bounds;
                    int h = lstB_Main.ItemHeight;
                    e.Graphics.DrawIcon(icons[item.Language.Hoster.Type], new Rectangle(rect.X + 2, rect.Y + 2, h - 4, h - 4));

                    rect.X = h - 2;
                    TextRenderer.DrawText(e.Graphics, item.ToString(), e.Font, rect, Color.White, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                }
            }
            catch { }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void nUD_consPerDL_ValueChanged(object sender, EventArgs e)
        {
            downloader.Download_Connections = (int)nUD_consPerDL.Value;
            Settings.Default.CONENCTIONS_PER_DOWNLOAD = downloader.Download_Connections;
            Settings.Default.Save();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            downloader.Max_Parallel_Downloads = (int)nUD_ParallelDLs.Value;
            Settings.Default.PARALLEL_DOWNLOADS = downloader.Max_Parallel_Downloads;
            Settings.Default.Save();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                var cn = downloader.Items.Any(t => t.IsRunning);
                if (downloader.Items.Count > 0 && cn)
                {
                    //var rd = new Random();
                    //foreach (var item in downloader.Items)
                    //{
                    //    item.Progress = (item.Progress == 100) ? 0 : item.Progress + rd.Next(1, 5);
                    //}
                    var cur = downloader.CurrrentSize;
                    var all = downloader.Size;
                    var speed = downloader.Speed;
                    lbl_info.Text = speed.SizeSuffix(1) + "/s " +
                       cur.SizeSuffix(1) + "/" + all.SizeSuffix(1) + " " +
                       TimeSpan.FromSeconds((speed > 0) ? ((all - cur) / speed) : TimeSpan.MaxValue.Seconds).ToString(@"hh\:mm\:ss");
                    lstB_Main.Invalidate();
                }
            }
            catch (Exception x)
            {
            }
        }
    }
}