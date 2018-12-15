using System;
using System.Drawing;
using System.Windows.Forms;
using Lix.Extensions;
using Lix.Kodi;
using Lix.SeriesManager;
using LixGrabber.Properties;

namespace LixGrabber
{
    public partial class KodiPlay : Form
    {
        private Form1 Form1Main;
        private Kodi kodiRPC = new Kodi();
        private int refreshCnt = 0;

        public KodiPlay(Form1 form)
        {
            InitializeComponent();
            Form1Main = form;
        }

        public new void BringToFront()
        {
            base.BringToFront();
            this.Top = Form1Main.Top;
            this.Left = Form1Main.Right - 10;
        }

        private async void btn_ConnectKodi_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtB_Kodi_Address.TextLength > 3)
                {
                    if (await kodiRPC.SetCredentials(txtB_Kodi_Address.Text, txtB_Kodi_User.Text, txtB_Kodi_Pass.Text))
                    {
                        btn_ConnectKodi.ForeColor = Color.LimeGreen;
                        setEnabled(true);
                        nUD_Volume.Value = kodiRPC.PlayerInfo.Volume;
                    }
                    else
                    {
                        setEnabled(false);
                    }
                }

                Settings.Default.KODI_ADDRESS = txtB_Kodi_Address.Text;
                Settings.Default.KODI_USER = txtB_Kodi_User.Text;
                Settings.Default.KODI_PASS = txtB_Kodi_Pass.Text;
                Settings.Default.Save();
            }
            catch (Exception x)
            {
            }
        }

        private async void btn_Input_Back_Click(object sender, EventArgs e)
        {
            await kodiRPC.Input(KodiInput.Back);
        }

        private async void btn_Input_ContexMenue_Click(object sender, EventArgs e)
        {
            await kodiRPC.Input(KodiInput.ContextMenu);
        }

        private async void btn_Input_Down_Click(object sender, EventArgs e)
        {
            await kodiRPC.Input(KodiInput.Down);
        }

        private async void btn_Input_Home_Click(object sender, EventArgs e)
        {
            await kodiRPC.Input(KodiInput.Home);
        }

        private async void btn_Input_Info_Click(object sender, EventArgs e)
        {
            await kodiRPC.Input(KodiInput.Info);
        }

        private async void btn_Input_Left_Click(object sender, EventArgs e)
        {
            await kodiRPC.Input(KodiInput.Left);
        }

        private async void btn_Input_Right_Click(object sender, EventArgs e)
        {
            await kodiRPC.Input(KodiInput.Right);
        }

        private async void btn_Input_Select_Click(object sender, EventArgs e)
        {
            await kodiRPC.Input(KodiInput.Select);
        }

        private async void btn_Input_Up_Click(object sender, EventArgs e)
        {
            await kodiRPC.Input(KodiInput.Up);
        }

        private async void btn_Play_Click(object sender, EventArgs e)
        {
            try
            {
                var item = (LanguageInfo)Form1Main.cBL_Languages.SelectedItem;

                Form1Main.cancelationToken.Reset();
                if (kodiRPC.PlayerInfo.Status != KodiPlayerStatus.Playing)
                {
                    //await DownLoader.Download(DownloadType.YT_DL, Form1Main.downloadInfo);

                    string url = await item.FetchVideoUrl(Form1Main.downloadInfo);
                    item.Hoster.Episode.Season.Series.Save();
                    var result = await kodiRPC.Play(url);
                    if (result.Status == KodiPlayerStatus.Playing)
                    {
                        btn_ConnectKodi.ForeColor = Color.LimeGreen;
                        btn_ConnectKodi.Enabled = false;

                        btn_Play.Text = "Pause";

                        Form1Main.txtB_HosterUrl.Text = url;
                        Form1Main.txtB_DirectUrl.Text = url;
                    }
                }
                else
                {
                    switch ((await kodiRPC.Pause()).Status)
                    {
                        case KodiPlayerStatus.Playing:
                            btn_Play.Text = "Pause";
                            break;

                        case KodiPlayerStatus.Pause:
                            btn_Play.Text = "Resume";
                            break;

                        case KodiPlayerStatus.Stopped:
                            btn_Play.Text = "Play";
                            break;

                        default:
                            btn_Play.Text = "Play";
                            break;
                    }
                }
            }
            catch (Exception x)
            {
            }
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            kodiRPC.Stop();
            btn_Play.Text = "Play";
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            kodiRPC.Input(KodiInput.Fullscreen);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await kodiRPC.Mute();
            setEnabled(null);
        }

        private void KodiPlay_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1Main.kodiPlay.Dispose();
        }

        private void KodiPlay_FormClosing(object sender, FormClosingEventArgs e)
        {
            // this.Visible = false;
            // e.Cancel = true;
            kodiRPC.Dispose();
            Form1Main.btn_openKodiPlay.Visible = true;
        }

        private void KodiPlay_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            numericUpDown1.Value = Settings.Default.KODI_BUFFER_SIZE;// kodiRPC.BufferSize / HTTP_Stream_Server.MEGABYTE;
            LoadKodiInfos();
        }

        private async void LoadKodiInfos()
        {
            try
            {
                bool kenable = false;
                if (await kodiRPC.SetCredentials(Settings.Default.KODI_ADDRESS, Settings.Default.KODI_USER, Settings.Default.KODI_PASS))
                {
                    btn_ConnectKodi.ForeColor = Color.LimeGreen;
                    nUD_Volume.Value = kodiRPC.PlayerInfo.Volume;
                    btn_Input_Mute.Text = kodiRPC.PlayerInfo.Muted ? "🔇" : "🔊";
                    kenable = true;
                }
                txtB_Kodi_Address.Text = Settings.Default.KODI_ADDRESS;
                txtB_Kodi_User.Text = Settings.Default.KODI_USER;
                txtB_Kodi_Pass.Text = Settings.Default.KODI_PASS;

                setEnabled(kenable);

                this.Cursor = Cursors.Arrow;
                this.Visible = true;
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Settings.Default.KODI_BUFFER_SIZE = (int)numericUpDown1.Value;
            kodiRPC.BufferSize = LiXMath.MIBIBYTE * (int)numericUpDown1.Value;
            Settings.Default.Save();
        }

        private async void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            await kodiRPC.Volume((int)nUD_Volume.Value);
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!string.IsNullOrEmpty(kodiRPC.PlayerInfo.Thumbnail))
                System.Diagnostics.Process.Start(kodiRPC.PlayerInfo.Thumbnail);
        }

        private void setEnabled(bool? enabled)
        {
            try
            {
                if (this.IsHandleCreated)
                {
                    this?.Invoke(new Action(() =>
                    {
                        if (enabled != null)
                        {
                            btn_ConnectKodi.Enabled = !((bool)enabled);
                            pictureBox1.Visible =
                            pn_Kodi_Input.Enabled =
                                btn_Play.Enabled =
                                btn_Stop.Enabled =
                                nUD_Volume.Enabled = (bool)enabled;
                        }

                        nUD_Volume.Value = kodiRPC.PlayerInfo.Volume;
                        btn_Input_Mute.Text = kodiRPC.PlayerInfo.Muted ? "🔇" : "🔊";
                    }));
                }
            }
            catch (Exception x)
            {
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (refreshCnt >= 10)
            {
                kodiRPC.GetProperties();
                if (kodiRPC.PlayerInfo.Status != KodiPlayerStatus.Error)
                {
                    setEnabled(true);
                    if (kodiRPC.PlayerInfo.Status == KodiPlayerStatus.Playing)
                    {
                        if (!string.IsNullOrEmpty(kodiRPC.PlayerInfo.Thumbnail))
                        {
                            pictureBox1.LoadAsync(kodiRPC.PlayerInfo.Thumbnail);
                        }
                    }
                }
                refreshCnt = 0;
            }
            if (kodiRPC.PlayerInfo.Status == KodiPlayerStatus.Playing)
            {
                if (!string.IsNullOrEmpty(kodiRPC.PlayerInfo.Thumbnail))
                {
                    pictureBox1.LoadAsync(kodiRPC.PlayerInfo.Thumbnail);
                }

                if (refreshCnt < 10)
                {
                    kodiRPC.PlayerInfo.PositionInfo.Time.Seconds++;
                }

                lbl_KodiPlayInfo.Text = kodiRPC.PlayerInfo.CurrentTime.ToString(@"hh\:mm\:ss")
                    + " / " +
                    kodiRPC.PlayerInfo.Duration.ToString(@"hh\:mm\:ss")
                    + " (" + (int)(kodiRPC.PlayerInfo.Percentage) + "%"
                    + ")\r\n" +
                    kodiRPC.PlayerInfo.Title;
            }
            else
            {
                if (kodiRPC.PlayerInfo.Status == KodiPlayerStatus.Error)
                {
                    setEnabled(false);
                }
                lbl_KodiPlayInfo.Text = "";
                pictureBox1.Visible = false;
            }
            refreshCnt++;
        }

        private void txtB_Kodi_Address_TextChanged(object sender, EventArgs e)
        {
            btn_ConnectKodi.Enabled = true;
        }

        private void txtB_Kodi_Pass_MouseHover_1(object sender, EventArgs e)
        {
            toolTip1.Show(txtB_Kodi_Pass.Text, txtB_Kodi_Pass, txtB_Kodi_Pass.PointToClient(MousePosition));
        }

        private void txtB_Kodi_Pass_MouseLeave_1(object sender, EventArgs e)
        {
            toolTip1.Hide(txtB_Kodi_Pass);
        }

        private void txtB_Kodi_User_TextChanged(object sender, EventArgs e)
        {
            btn_ConnectKodi.Enabled = true;
        }
    }
}