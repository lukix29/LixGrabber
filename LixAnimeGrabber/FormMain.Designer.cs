namespace LixGrabber
{
    partial class FormMain
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.btn_FetchSeriesInfo = new System.Windows.Forms.Button();
            this.txtB_Url = new System.Windows.Forms.TextBox();
            this.txtb_output = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lbl_url_invalid = new System.Windows.Forms.Label();
            this.fBD_Output = new System.Windows.Forms.FolderBrowserDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_FetchVideo = new System.Windows.Forms.Button();
            this.cMS_Seasons = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tSMi_FetchAllEpisodes = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadAllEpisodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.txtB_DirectUrl = new System.Windows.Forms.TextBox();
            this.btn_DirectDownload = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button3 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.txtB_HosterUrl = new System.Windows.Forms.TextBox();
            this.cMS_Episode = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteEpisodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToDownloaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cBL_Hoster = new LixGrabber.DoubleBufferedListBox();
            this.cBL_Episodes = new LixGrabber.DoubleBufferedListBox();
            this.cBL_Languages = new LixGrabber.DoubleBufferedListBox();
            this.cBL_Series = new LixGrabber.DoubleBufferedListBox();
            this.cMS_Series = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tSMi_DeleteSeries = new System.Windows.Forms.ToolStripMenuItem();
            this.checkAllForNewEpisodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cBL_Seasons = new LixGrabber.DoubleBufferedListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tSB_Search = new System.Windows.Forms.ToolStripButton();
            this.tsB_Settings = new System.Windows.Forms.ToolStripButton();
            this.btn_openKodiPlay = new System.Windows.Forms.ToolStripButton();
            this.progressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.lbl_Progress = new System.Windows.Forms.ToolStripTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.cMS_Seasons.SuspendLayout();
            this.panel2.SuspendLayout();
            this.cMS_Episode.SuspendLayout();
            this.cMS_Series.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_FetchSeriesInfo
            // 
            this.btn_FetchSeriesInfo.AutoSize = true;
            this.btn_FetchSeriesInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btn_FetchSeriesInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_FetchSeriesInfo.Location = new System.Drawing.Point(6, 79);
            this.btn_FetchSeriesInfo.Name = "btn_FetchSeriesInfo";
            this.btn_FetchSeriesInfo.Size = new System.Drawing.Size(136, 28);
            this.btn_FetchSeriesInfo.TabIndex = 1;
            this.btn_FetchSeriesInfo.Text = "Add Series from Url";
            this.btn_FetchSeriesInfo.UseVisualStyleBackColor = false;
            this.btn_FetchSeriesInfo.Click += new System.EventHandler(this.bt_Fetch_Click);
            // 
            // txtB_Url
            // 
            this.txtB_Url.AllowDrop = true;
            this.txtB_Url.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.txtB_Url.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtB_Url.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtB_Url.ForeColor = System.Drawing.Color.White;
            this.txtB_Url.Location = new System.Drawing.Point(6, 51);
            this.txtB_Url.Name = "txtB_Url";
            this.txtB_Url.Size = new System.Drawing.Size(328, 22);
            this.txtB_Url.TabIndex = 2;
            this.txtB_Url.WordWrap = false;
            this.txtB_Url.TextChanged += new System.EventHandler(this.txtB_Url_TextChanged);
            this.txtB_Url.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtB_Url_DragDrop);
            this.txtB_Url.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtB_Url_DragEnter);
            this.txtB_Url.DragLeave += new System.EventHandler(this.txtB_Url_DragLeave);
            // 
            // txtb_output
            // 
            this.txtb_output.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.txtb_output.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtb_output.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtb_output.ForeColor = System.Drawing.Color.White;
            this.txtb_output.Location = new System.Drawing.Point(3, 109);
            this.txtb_output.Name = "txtb_output";
            this.txtb_output.Size = new System.Drawing.Size(328, 22);
            this.txtb_output.TabIndex = 3;
            this.txtb_output.TextChanged += new System.EventHandler(this.txtb_output_TextChanged);
            this.txtb_output.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtb_output_DragDrop);
            this.txtb_output.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtb_output_DragEnter);
            this.txtb_output.DragLeave += new System.EventHandler(this.txtb_output_DragLeave);
            this.txtb_output.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtb_output_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Output Directory";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label2.Location = new System.Drawing.Point(4, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(176, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "s.to / 9anime.to - Series URL";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(3, 137);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 28);
            this.button1.TabIndex = 6;
            this.button1.Text = "Change Directory";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbl_url_invalid
            // 
            this.lbl_url_invalid.AutoSize = true;
            this.lbl_url_invalid.BackColor = System.Drawing.Color.Black;
            this.lbl_url_invalid.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_url_invalid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbl_url_invalid.Location = new System.Drawing.Point(208, 26);
            this.lbl_url_invalid.Name = "lbl_url_invalid";
            this.lbl_url_invalid.Size = new System.Drawing.Size(96, 24);
            this.lbl_url_invalid.TabIndex = 7;
            this.lbl_url_invalid.Text = "Invalid Url!";
            this.lbl_url_invalid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_url_invalid.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Location = new System.Drawing.Point(2, 296);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(336, 296);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // btn_FetchVideo
            // 
            this.btn_FetchVideo.AutoSize = true;
            this.btn_FetchVideo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btn_FetchVideo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_FetchVideo.Location = new System.Drawing.Point(239, 137);
            this.btn_FetchVideo.Name = "btn_FetchVideo";
            this.btn_FetchVideo.Size = new System.Drawing.Size(92, 28);
            this.btn_FetchVideo.TabIndex = 12;
            this.btn_FetchVideo.Text = "Fetch Video";
            this.btn_FetchVideo.UseVisualStyleBackColor = false;
            this.btn_FetchVideo.Click += new System.EventHandler(this.button2_Click);
            // 
            // cMS_Seasons
            // 
            this.cMS_Seasons.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.cMS_Seasons.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSMi_FetchAllEpisodes,
            this.downloadAllEpisodesToolStripMenuItem});
            this.cMS_Seasons.Name = "contextMenuStrip1";
            this.cMS_Seasons.Size = new System.Drawing.Size(193, 48);
            // 
            // tSMi_FetchAllEpisodes
            // 
            this.tSMi_FetchAllEpisodes.Name = "tSMi_FetchAllEpisodes";
            this.tSMi_FetchAllEpisodes.Size = new System.Drawing.Size(192, 22);
            this.tSMi_FetchAllEpisodes.Text = "Fetch all Episodes";
            this.tSMi_FetchAllEpisodes.Click += new System.EventHandler(this.tSMi_FetchAllEpisodes_Click);
            // 
            // downloadAllEpisodesToolStripMenuItem
            // 
            this.downloadAllEpisodesToolStripMenuItem.Name = "downloadAllEpisodesToolStripMenuItem";
            this.downloadAllEpisodesToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.downloadAllEpisodesToolStripMenuItem.Text = "Download all Episodes";
            this.downloadAllEpisodesToolStripMenuItem.Click += new System.EventHandler(this.downloadAllEpisodesToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(0, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(112, 16);
            this.label8.TabIndex = 25;
            this.label8.Text = "Direct Video URL";
            // 
            // txtB_DirectUrl
            // 
            this.txtB_DirectUrl.AllowDrop = true;
            this.txtB_DirectUrl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.txtB_DirectUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtB_DirectUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtB_DirectUrl.ForeColor = System.Drawing.Color.White;
            this.txtB_DirectUrl.Location = new System.Drawing.Point(3, 65);
            this.txtB_DirectUrl.Name = "txtB_DirectUrl";
            this.txtB_DirectUrl.ReadOnly = true;
            this.txtB_DirectUrl.Size = new System.Drawing.Size(328, 22);
            this.txtB_DirectUrl.TabIndex = 24;
            this.txtB_DirectUrl.WordWrap = false;
            this.txtB_DirectUrl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtB_DirectUrl_MouseClick);
            this.txtB_DirectUrl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtB_DirectUrl_MouseDoubleClick);
            // 
            // btn_DirectDownload
            // 
            this.btn_DirectDownload.AutoSize = true;
            this.btn_DirectDownload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btn_DirectDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DirectDownload.Location = new System.Drawing.Point(152, 137);
            this.btn_DirectDownload.Name = "btn_DirectDownload";
            this.btn_DirectDownload.Size = new System.Drawing.Size(81, 28);
            this.btn_DirectDownload.TabIndex = 26;
            this.btn_DirectDownload.Text = "Download";
            this.btn_DirectDownload.UseVisualStyleBackColor = false;
            this.btn_DirectDownload.Click += new System.EventHandler(this.btn_DirectDownload_Click);
            // 
            // button3
            // 
            this.button3.AutoSize = true;
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(177, 79);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(157, 28);
            this.button3.TabIndex = 37;
            this.button3.Text = "Check for new Epsiode";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.txtb_output);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btn_DirectDownload);
            this.panel2.Controls.Add(this.txtB_DirectUrl);
            this.panel2.Controls.Add(this.txtB_HosterUrl);
            this.panel2.Controls.Add(this.btn_FetchVideo);
            this.panel2.Location = new System.Drawing.Point(2, 115);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(336, 175);
            this.panel2.TabIndex = 38;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 16);
            this.label9.TabIndex = 40;
            this.label9.Text = "Hoster URL";
            // 
            // txtB_HosterUrl
            // 
            this.txtB_HosterUrl.AllowDrop = true;
            this.txtB_HosterUrl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.txtB_HosterUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtB_HosterUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtB_HosterUrl.ForeColor = System.Drawing.Color.White;
            this.txtB_HosterUrl.Location = new System.Drawing.Point(3, 21);
            this.txtB_HosterUrl.Name = "txtB_HosterUrl";
            this.txtB_HosterUrl.ReadOnly = true;
            this.txtB_HosterUrl.Size = new System.Drawing.Size(328, 22);
            this.txtB_HosterUrl.TabIndex = 39;
            this.txtB_HosterUrl.WordWrap = false;
            this.txtB_HosterUrl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_Url_MouseClick);
            this.txtB_HosterUrl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbl_Url_MouseDoubleClick);
            // 
            // cMS_Episode
            // 
            this.cMS_Episode.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.cMS_Episode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteEpisodeToolStripMenuItem,
            this.addToDownloaderToolStripMenuItem});
            this.cMS_Episode.Name = "contextMenuStrip1";
            this.cMS_Episode.Size = new System.Drawing.Size(178, 48);
            // 
            // deleteEpisodeToolStripMenuItem
            // 
            this.deleteEpisodeToolStripMenuItem.Name = "deleteEpisodeToolStripMenuItem";
            this.deleteEpisodeToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.deleteEpisodeToolStripMenuItem.Text = "Delete Episode";
            this.deleteEpisodeToolStripMenuItem.Click += new System.EventHandler(this.deleteEpisodeToolStripMenuItem_Click);
            // 
            // addToDownloaderToolStripMenuItem
            // 
            this.addToDownloaderToolStripMenuItem.Name = "addToDownloaderToolStripMenuItem";
            this.addToDownloaderToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.addToDownloaderToolStripMenuItem.Text = "Add to Downloader";
            this.addToDownloaderToolStripMenuItem.Click += new System.EventHandler(this.addToDownloaderToolStripMenuItem_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(197, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 16);
            this.label5.TabIndex = 18;
            this.label5.Text = "Episodes";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(456, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 16);
            this.label7.TabIndex = 20;
            this.label7.Text = "Series";
            // 
            // cBL_Hoster
            // 
            this.cBL_Hoster.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.cBL_Hoster.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cBL_Hoster.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cBL_Hoster.ForeColor = System.Drawing.Color.Gainsboro;
            this.cBL_Hoster.FormattingEnabled = true;
            this.cBL_Hoster.IntegralHeight = false;
            this.cBL_Hoster.ItemHeight = 18;
            this.cBL_Hoster.Location = new System.Drawing.Point(4, 22);
            this.cBL_Hoster.Name = "cBL_Hoster";
            this.cBL_Hoster.Size = new System.Drawing.Size(190, 268);
            this.cBL_Hoster.TabIndex = 15;
            this.cBL_Hoster.SelectedIndexChanged += new System.EventHandler(this.cBL_Hoster_SelectedIndexChanged);
            // 
            // cBL_Episodes
            // 
            this.cBL_Episodes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cBL_Episodes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.cBL_Episodes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cBL_Episodes.ContextMenuStrip = this.cMS_Episode;
            this.cBL_Episodes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cBL_Episodes.ForeColor = System.Drawing.Color.Gainsboro;
            this.cBL_Episodes.FormattingEnabled = true;
            this.cBL_Episodes.IntegralHeight = false;
            this.cBL_Episodes.ItemHeight = 18;
            this.cBL_Episodes.Location = new System.Drawing.Point(200, 22);
            this.cBL_Episodes.Name = "cBL_Episodes";
            this.cBL_Episodes.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.cBL_Episodes.Size = new System.Drawing.Size(253, 545);
            this.cBL_Episodes.TabIndex = 10;
            this.cBL_Episodes.SelectedIndexChanged += new System.EventHandler(this.cBL_Episodes_SelectedIndexChanged);
            this.cBL_Episodes.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.cBL_Episodes_MouseDoubleClick);
            // 
            // cBL_Languages
            // 
            this.cBL_Languages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.cBL_Languages.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.cBL_Languages.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cBL_Languages.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cBL_Languages.ForeColor = System.Drawing.Color.Gainsboro;
            this.cBL_Languages.FormattingEnabled = true;
            this.cBL_Languages.IntegralHeight = false;
            this.cBL_Languages.ItemHeight = 18;
            this.cBL_Languages.Location = new System.Drawing.Point(4, 312);
            this.cBL_Languages.Name = "cBL_Languages";
            this.cBL_Languages.Size = new System.Drawing.Size(190, 255);
            this.cBL_Languages.TabIndex = 16;
            this.cBL_Languages.SelectedIndexChanged += new System.EventHandler(this.cBL_Languages_SelectedIndexChanged);
            // 
            // cBL_Series
            // 
            this.cBL_Series.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cBL_Series.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.cBL_Series.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cBL_Series.ContextMenuStrip = this.cMS_Series;
            this.cBL_Series.ForeColor = System.Drawing.Color.Gainsboro;
            this.cBL_Series.FormattingEnabled = true;
            this.cBL_Series.IntegralHeight = false;
            this.cBL_Series.ItemHeight = 16;
            this.cBL_Series.Location = new System.Drawing.Point(459, 22);
            this.cBL_Series.Name = "cBL_Series";
            this.cBL_Series.Size = new System.Drawing.Size(219, 268);
            this.cBL_Series.TabIndex = 11;
            this.cBL_Series.SelectedIndexChanged += new System.EventHandler(this.cBL_saved_series_SelectedIndexChanged);
            this.cBL_Series.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.cBL_Series_MouseDoubleClick);
            // 
            // cMS_Series
            // 
            this.cMS_Series.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.cMS_Series.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSMi_DeleteSeries,
            this.checkAllForNewEpisodeToolStripMenuItem});
            this.cMS_Series.Name = "contextMenuStrip1";
            this.cMS_Series.Size = new System.Drawing.Size(210, 48);
            // 
            // tSMi_DeleteSeries
            // 
            this.tSMi_DeleteSeries.Name = "tSMi_DeleteSeries";
            this.tSMi_DeleteSeries.Size = new System.Drawing.Size(209, 22);
            this.tSMi_DeleteSeries.Text = "Delete Series";
            this.tSMi_DeleteSeries.Click += new System.EventHandler(this.tSMi_DeleteSeries_Click);
            // 
            // checkAllForNewEpisodeToolStripMenuItem
            // 
            this.checkAllForNewEpisodeToolStripMenuItem.Name = "checkAllForNewEpisodeToolStripMenuItem";
            this.checkAllForNewEpisodeToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.checkAllForNewEpisodeToolStripMenuItem.Text = "Check all for new Episode";
            this.checkAllForNewEpisodeToolStripMenuItem.Click += new System.EventHandler(this.checkAllForNewEpisodeToolStripMenuItem_Click);
            // 
            // cBL_Seasons
            // 
            this.cBL_Seasons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cBL_Seasons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.cBL_Seasons.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cBL_Seasons.ContextMenuStrip = this.cMS_Seasons;
            this.cBL_Seasons.ForeColor = System.Drawing.Color.Gainsboro;
            this.cBL_Seasons.FormattingEnabled = true;
            this.cBL_Seasons.IntegralHeight = false;
            this.cBL_Seasons.ItemHeight = 16;
            this.cBL_Seasons.Location = new System.Drawing.Point(459, 312);
            this.cBL_Seasons.Name = "cBL_Seasons";
            this.cBL_Seasons.Size = new System.Drawing.Size(219, 255);
            this.cBL_Seasons.Sorted = true;
            this.cBL_Seasons.TabIndex = 11;
            this.cBL_Seasons.SelectedIndexChanged += new System.EventHandler(this.cBL_Seasons_SelectedIndexChanged);
            this.cBL_Seasons.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.cBL_Seasons_MouseDoubleClick);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(456, 293);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 16);
            this.label6.TabIndex = 19;
            this.label6.Text = "Seasons";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1, 293);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 16);
            this.label4.TabIndex = 18;
            this.label4.Text = "Languages";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 17;
            this.label3.Text = "Hoster";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cBL_Seasons);
            this.panel1.Controls.Add(this.cBL_Series);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.cBL_Languages);
            this.panel1.Controls.Add(this.cBL_Episodes);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.cBL_Hoster);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(341, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(683, 580);
            this.panel1.TabIndex = 21;
            // 
            // tSB_Search
            // 
            this.tSB_Search.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.tSB_Search.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tSB_Search.Image = ((System.Drawing.Image)(resources.GetObject("tSB_Search.Image")));
            this.tSB_Search.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSB_Search.Name = "tSB_Search";
            this.tSB_Search.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tSB_Search.Size = new System.Drawing.Size(51, 22);
            this.tSB_Search.Text = "Search";
            this.tSB_Search.Click += new System.EventHandler(this.tSB_Search_Click);
            // 
            // tsB_Settings
            // 
            this.tsB_Settings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.tsB_Settings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsB_Settings.Image = ((System.Drawing.Image)(resources.GetObject("tsB_Settings.Image")));
            this.tsB_Settings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsB_Settings.Margin = new System.Windows.Forms.Padding(4, 1, 0, 2);
            this.tsB_Settings.Name = "tsB_Settings";
            this.tsB_Settings.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tsB_Settings.Size = new System.Drawing.Size(58, 22);
            this.tsB_Settings.Text = "Settings";
            this.tsB_Settings.Click += new System.EventHandler(this.tSB_Settings_Click);
            // 
            // btn_openKodiPlay
            // 
            this.btn_openKodiPlay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.btn_openKodiPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn_openKodiPlay.Image = ((System.Drawing.Image)(resources.GetObject("btn_openKodiPlay.Image")));
            this.btn_openKodiPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_openKodiPlay.Margin = new System.Windows.Forms.Padding(4, 1, 0, 2);
            this.btn_openKodiPlay.Name = "btn_openKodiPlay";
            this.btn_openKodiPlay.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.btn_openKodiPlay.Size = new System.Drawing.Size(66, 22);
            this.btn_openKodiPlay.Text = "Kodi Play";
            this.btn_openKodiPlay.Click += new System.EventHandler(this.tSB_KodiPlay_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.AutoSize = false;
            this.progressBar1.BackColor = System.Drawing.Color.Black;
            this.progressBar1.ForeColor = System.Drawing.Color.SeaGreen;
            this.progressBar1.Margin = new System.Windows.Forms.Padding(5, 2, 1, 1);
            this.progressBar1.MarqueeAnimationSpeed = 50;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.progressBar1.Size = new System.Drawing.Size(200, 22);
            this.progressBar1.Step = 1;
            // 
            // lbl_Progress
            // 
            this.lbl_Progress.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lbl_Progress.BackColor = System.Drawing.Color.Black;
            this.lbl_Progress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbl_Progress.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Progress.ForeColor = System.Drawing.Color.Gainsboro;
            this.lbl_Progress.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.lbl_Progress.Name = "lbl_Progress";
            this.lbl_Progress.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.lbl_Progress.ReadOnly = true;
            this.lbl_Progress.Size = new System.Drawing.Size(625, 25);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.White;
            this.toolStrip1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("toolStrip1.BackgroundImage")));
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSB_Search,
            this.tsB_Settings,
            this.btn_openKodiPlay,
            this.progressBar1,
            this.lbl_Progress});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1024, 25);
            this.toolStrip1.TabIndex = 43;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1024, 601);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtB_Url);
            this.Controls.Add(this.btn_FetchSeriesInfo);
            this.Controls.Add(this.lbl_url_invalid);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1040, 640);
            this.Name = "Form1";
            this.Text = "Lix Grabber";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            this.LocationChanged += new System.EventHandler(this.Form1_LocationChanged);
            this.MouseEnter += new System.EventHandler(this.Form1_MouseEnter);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.cMS_Seasons.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.cMS_Episode.ResumeLayout(false);
            this.cMS_Series.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Button btn_FetchSeriesInfo;
        public System.Windows.Forms.TextBox txtB_Url;
        public System.Windows.Forms.TextBox txtb_output;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.Label lbl_url_invalid;
        public System.Windows.Forms.FolderBrowserDialog fBD_Output;
        public System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.Button btn_FetchVideo;
        public System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.ContextMenuStrip cMS_Seasons;
        public System.Windows.Forms.ToolStripMenuItem tSMi_FetchAllEpisodes;
        public System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox txtB_DirectUrl;
        public System.Windows.Forms.Button btn_DirectDownload;
        public System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.Button button3;
        public System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.TextBox txtB_HosterUrl;
        private System.Windows.Forms.ToolStripMenuItem downloadAllEpisodesToolStripMenuItem;
        public System.Windows.Forms.ContextMenuStrip cMS_Episode;
        private System.Windows.Forms.ToolStripMenuItem deleteEpisodeToolStripMenuItem;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.Label label6;
        public LixGrabber.DoubleBufferedListBox cBL_Hoster;
        public LixGrabber.DoubleBufferedListBox cBL_Episodes;
        public LixGrabber.DoubleBufferedListBox cBL_Languages;
        public LixGrabber.DoubleBufferedListBox cBL_Series;
        public LixGrabber.DoubleBufferedListBox cBL_Seasons;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label label9;
        public System.Windows.Forms.ToolStripButton tSB_Search;
        public System.Windows.Forms.ToolStripButton tsB_Settings;
        public System.Windows.Forms.ToolStripButton btn_openKodiPlay;
        public System.Windows.Forms.ToolStripProgressBar progressBar1;
        public System.Windows.Forms.ToolStripTextBox lbl_Progress;
        public System.Windows.Forms.ToolStrip toolStrip1;
        public System.Windows.Forms.ContextMenuStrip cMS_Series;
        public System.Windows.Forms.ToolStripMenuItem tSMi_DeleteSeries;
        private System.Windows.Forms.ToolStripMenuItem checkAllForNewEpisodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToDownloaderToolStripMenuItem;
    }
}

