namespace LixGrabber
{
    partial class FormKodiPlay
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pn_kodi = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pn_Kodi_Input = new System.Windows.Forms.Panel();
            this.btn_Input_Mute = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_Input_ContexMenue = new System.Windows.Forms.Button();
            this.btn_Input_Home = new System.Windows.Forms.Button();
            this.btn_Input_Info = new System.Windows.Forms.Button();
            this.btn_Input_Up = new System.Windows.Forms.Button();
            this.btn_Input_Back = new System.Windows.Forms.Button();
            this.btn_Input_Right = new System.Windows.Forms.Button();
            this.btn_Input_Select = new System.Windows.Forms.Button();
            this.btn_Input_Left = new System.Windows.Forms.Button();
            this.btn_Input_Down = new System.Windows.Forms.Button();
            this.nUD_Volume = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.lbl_KodiPlayInfo = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btn_ConnectKodi = new System.Windows.Forms.Button();
            this.btn_Play = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.txtB_Kodi_User = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtB_Kodi_Address = new System.Windows.Forms.TextBox();
            this.txtB_Kodi_Pass = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pn_kodi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pn_Kodi_Input.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_Volume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // pn_kodi
            // 
            this.pn_kodi.AutoSize = true;
            this.pn_kodi.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pn_kodi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pn_kodi.Controls.Add(this.pictureBox1);
            this.pn_kodi.Controls.Add(this.pn_Kodi_Input);
            this.pn_kodi.Controls.Add(this.nUD_Volume);
            this.pn_kodi.Controls.Add(this.numericUpDown1);
            this.pn_kodi.Controls.Add(this.btn_Stop);
            this.pn_kodi.Controls.Add(this.lbl_KodiPlayInfo);
            this.pn_kodi.Controls.Add(this.label9);
            this.pn_kodi.Controls.Add(this.btn_ConnectKodi);
            this.pn_kodi.Controls.Add(this.btn_Play);
            this.pn_kodi.Controls.Add(this.label11);
            this.pn_kodi.Controls.Add(this.txtB_Kodi_User);
            this.pn_kodi.Controls.Add(this.label10);
            this.pn_kodi.Controls.Add(this.txtB_Kodi_Address);
            this.pn_kodi.Controls.Add(this.txtB_Kodi_Pass);
            this.pn_kodi.Controls.Add(this.label1);
            this.pn_kodi.Controls.Add(this.label2);
            this.pn_kodi.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.pn_kodi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pn_kodi.Location = new System.Drawing.Point(0, 0);
            this.pn_kodi.Name = "pn_kodi";
            this.pn_kodi.Size = new System.Drawing.Size(330, 447);
            this.pn_kodi.TabIndex = 37;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Location = new System.Drawing.Point(4, 261);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(320, 180);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 51;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // pn_Kodi_Input
            // 
            this.pn_Kodi_Input.Controls.Add(this.btn_Input_Mute);
            this.pn_Kodi_Input.Controls.Add(this.button1);
            this.pn_Kodi_Input.Controls.Add(this.btn_Input_ContexMenue);
            this.pn_Kodi_Input.Controls.Add(this.btn_Input_Home);
            this.pn_Kodi_Input.Controls.Add(this.btn_Input_Info);
            this.pn_Kodi_Input.Controls.Add(this.btn_Input_Up);
            this.pn_Kodi_Input.Controls.Add(this.btn_Input_Back);
            this.pn_Kodi_Input.Controls.Add(this.btn_Input_Right);
            this.pn_Kodi_Input.Controls.Add(this.btn_Input_Select);
            this.pn_Kodi_Input.Controls.Add(this.btn_Input_Left);
            this.pn_Kodi_Input.Controls.Add(this.btn_Input_Down);
            this.pn_Kodi_Input.Location = new System.Drawing.Point(171, 3);
            this.pn_Kodi_Input.Name = "pn_Kodi_Input";
            this.pn_Kodi_Input.Size = new System.Drawing.Size(154, 255);
            this.pn_Kodi_Input.TabIndex = 50;
            // 
            // btn_Input_Mute
            // 
            this.btn_Input_Mute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Input_Mute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btn_Input_Mute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Input_Mute.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Input_Mute.Location = new System.Drawing.Point(105, 105);
            this.btn_Input_Mute.MaximumSize = new System.Drawing.Size(45, 45);
            this.btn_Input_Mute.Name = "btn_Input_Mute";
            this.btn_Input_Mute.Size = new System.Drawing.Size(45, 45);
            this.btn_Input_Mute.TabIndex = 54;
            this.btn_Input_Mute.Text = "🔇";
            this.btn_Input_Mute.UseVisualStyleBackColor = false;
            this.btn_Input_Mute.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(54, 54);
            this.button1.MaximumSize = new System.Drawing.Size(45, 45);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(45, 45);
            this.button1.TabIndex = 53;
            this.button1.Text = "⛶";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btn_Input_ContexMenue
            // 
            this.btn_Input_ContexMenue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Input_ContexMenue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btn_Input_ContexMenue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Input_ContexMenue.Font = new System.Drawing.Font("Times New Roman", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Input_ContexMenue.Location = new System.Drawing.Point(105, 207);
            this.btn_Input_ContexMenue.MaximumSize = new System.Drawing.Size(45, 45);
            this.btn_Input_ContexMenue.Name = "btn_Input_ContexMenue";
            this.btn_Input_ContexMenue.Size = new System.Drawing.Size(45, 45);
            this.btn_Input_ContexMenue.TabIndex = 52;
            this.btn_Input_ContexMenue.Text = "▤";
            this.btn_Input_ContexMenue.UseVisualStyleBackColor = false;
            this.btn_Input_ContexMenue.Click += new System.EventHandler(this.btn_Input_ContexMenue_Click);
            // 
            // btn_Input_Home
            // 
            this.btn_Input_Home.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Input_Home.AutoSize = true;
            this.btn_Input_Home.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btn_Input_Home.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Input_Home.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Input_Home.Location = new System.Drawing.Point(3, 54);
            this.btn_Input_Home.Name = "btn_Input_Home";
            this.btn_Input_Home.Size = new System.Drawing.Size(45, 45);
            this.btn_Input_Home.TabIndex = 51;
            this.btn_Input_Home.Text = "🏠";
            this.btn_Input_Home.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Input_Home.UseVisualStyleBackColor = false;
            this.btn_Input_Home.Click += new System.EventHandler(this.btn_Input_Home_Click);
            // 
            // btn_Input_Info
            // 
            this.btn_Input_Info.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Input_Info.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btn_Input_Info.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Input_Info.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Input_Info.Location = new System.Drawing.Point(105, 54);
            this.btn_Input_Info.MaximumSize = new System.Drawing.Size(45, 45);
            this.btn_Input_Info.Name = "btn_Input_Info";
            this.btn_Input_Info.Size = new System.Drawing.Size(45, 45);
            this.btn_Input_Info.TabIndex = 50;
            this.btn_Input_Info.Text = "ℹ";
            this.btn_Input_Info.UseVisualStyleBackColor = false;
            this.btn_Input_Info.Click += new System.EventHandler(this.btn_Input_Info_Click);
            // 
            // btn_Input_Up
            // 
            this.btn_Input_Up.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Input_Up.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btn_Input_Up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Input_Up.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Input_Up.Location = new System.Drawing.Point(54, 105);
            this.btn_Input_Up.MaximumSize = new System.Drawing.Size(45, 45);
            this.btn_Input_Up.Name = "btn_Input_Up";
            this.btn_Input_Up.Size = new System.Drawing.Size(45, 45);
            this.btn_Input_Up.TabIndex = 44;
            this.btn_Input_Up.Text = "▲";
            this.btn_Input_Up.UseVisualStyleBackColor = false;
            this.btn_Input_Up.Click += new System.EventHandler(this.btn_Input_Up_Click);
            // 
            // btn_Input_Back
            // 
            this.btn_Input_Back.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Input_Back.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btn_Input_Back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Input_Back.Font = new System.Drawing.Font("Times New Roman", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Input_Back.Location = new System.Drawing.Point(3, 207);
            this.btn_Input_Back.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.btn_Input_Back.MaximumSize = new System.Drawing.Size(45, 45);
            this.btn_Input_Back.Name = "btn_Input_Back";
            this.btn_Input_Back.Size = new System.Drawing.Size(45, 45);
            this.btn_Input_Back.TabIndex = 49;
            this.btn_Input_Back.Text = "⏎";
            this.btn_Input_Back.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btn_Input_Back.UseVisualStyleBackColor = false;
            this.btn_Input_Back.Click += new System.EventHandler(this.btn_Input_Back_Click);
            // 
            // btn_Input_Right
            // 
            this.btn_Input_Right.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Input_Right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btn_Input_Right.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Input_Right.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Input_Right.Location = new System.Drawing.Point(105, 156);
            this.btn_Input_Right.MaximumSize = new System.Drawing.Size(45, 45);
            this.btn_Input_Right.Name = "btn_Input_Right";
            this.btn_Input_Right.Size = new System.Drawing.Size(45, 45);
            this.btn_Input_Right.TabIndex = 45;
            this.btn_Input_Right.Text = "▶";
            this.btn_Input_Right.UseVisualStyleBackColor = false;
            this.btn_Input_Right.Click += new System.EventHandler(this.btn_Input_Right_Click);
            // 
            // btn_Input_Select
            // 
            this.btn_Input_Select.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Input_Select.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btn_Input_Select.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Input_Select.Font = new System.Drawing.Font("Lucida Console", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Input_Select.Location = new System.Drawing.Point(54, 156);
            this.btn_Input_Select.MaximumSize = new System.Drawing.Size(45, 45);
            this.btn_Input_Select.Name = "btn_Input_Select";
            this.btn_Input_Select.Size = new System.Drawing.Size(45, 45);
            this.btn_Input_Select.TabIndex = 48;
            this.btn_Input_Select.Text = "OK";
            this.btn_Input_Select.UseVisualStyleBackColor = false;
            this.btn_Input_Select.Click += new System.EventHandler(this.btn_Input_Select_Click);
            // 
            // btn_Input_Left
            // 
            this.btn_Input_Left.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Input_Left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btn_Input_Left.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Input_Left.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Input_Left.Location = new System.Drawing.Point(3, 156);
            this.btn_Input_Left.MaximumSize = new System.Drawing.Size(45, 45);
            this.btn_Input_Left.Name = "btn_Input_Left";
            this.btn_Input_Left.Size = new System.Drawing.Size(45, 45);
            this.btn_Input_Left.TabIndex = 46;
            this.btn_Input_Left.Text = "◀";
            this.btn_Input_Left.UseVisualStyleBackColor = false;
            this.btn_Input_Left.Click += new System.EventHandler(this.btn_Input_Left_Click);
            // 
            // btn_Input_Down
            // 
            this.btn_Input_Down.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Input_Down.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btn_Input_Down.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Input_Down.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Input_Down.Location = new System.Drawing.Point(54, 207);
            this.btn_Input_Down.MaximumSize = new System.Drawing.Size(45, 45);
            this.btn_Input_Down.Name = "btn_Input_Down";
            this.btn_Input_Down.Size = new System.Drawing.Size(45, 45);
            this.btn_Input_Down.TabIndex = 47;
            this.btn_Input_Down.Text = "▼";
            this.btn_Input_Down.UseVisualStyleBackColor = false;
            this.btn_Input_Down.Click += new System.EventHandler(this.btn_Input_Down_Click);
            // 
            // nUD_Volume
            // 
            this.nUD_Volume.BackColor = System.Drawing.Color.Black;
            this.nUD_Volume.ForeColor = System.Drawing.Color.White;
            this.nUD_Volume.Location = new System.Drawing.Point(123, 184);
            this.nUD_Volume.Name = "nUD_Volume";
            this.nUD_Volume.Size = new System.Drawing.Size(41, 20);
            this.nUD_Volume.TabIndex = 42;
            this.nUD_Volume.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nUD_Volume.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.BackColor = System.Drawing.Color.Black;
            this.numericUpDown1.ForeColor = System.Drawing.Color.White;
            this.numericUpDown1.Location = new System.Drawing.Point(81, 146);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(38, 20);
            this.numericUpDown1.TabIndex = 40;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // btn_Stop
            // 
            this.btn_Stop.AutoSize = true;
            this.btn_Stop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btn_Stop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Stop.Location = new System.Drawing.Point(72, 178);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(47, 28);
            this.btn_Stop.TabIndex = 37;
            this.btn_Stop.Text = "Stop";
            this.btn_Stop.UseVisualStyleBackColor = false;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // lbl_KodiPlayInfo
            // 
            this.lbl_KodiPlayInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_KodiPlayInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_KodiPlayInfo.Location = new System.Drawing.Point(6, 209);
            this.lbl_KodiPlayInfo.Name = "lbl_KodiPlayInfo";
            this.lbl_KodiPlayInfo.Size = new System.Drawing.Size(158, 49);
            this.lbl_KodiPlayInfo.TabIndex = 36;
            this.lbl_KodiPlayInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 13);
            this.label9.TabIndex = 32;
            this.label9.Text = "Kodi Adress && Port";
            // 
            // btn_ConnectKodi
            // 
            this.btn_ConnectKodi.AutoSize = true;
            this.btn_ConnectKodi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btn_ConnectKodi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ConnectKodi.Location = new System.Drawing.Point(6, 138);
            this.btn_ConnectKodi.Name = "btn_ConnectKodi";
            this.btn_ConnectKodi.Size = new System.Drawing.Size(69, 28);
            this.btn_ConnectKodi.TabIndex = 35;
            this.btn_ConnectKodi.Text = "Connect";
            this.btn_ConnectKodi.UseVisualStyleBackColor = false;
            this.btn_ConnectKodi.Click += new System.EventHandler(this.btn_ConnectKodi_Click);
            // 
            // btn_Play
            // 
            this.btn_Play.AutoSize = true;
            this.btn_Play.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btn_Play.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Play.Location = new System.Drawing.Point(6, 178);
            this.btn_Play.Name = "btn_Play";
            this.btn_Play.Size = new System.Drawing.Size(60, 28);
            this.btn_Play.TabIndex = 27;
            this.btn_Play.Text = "Play";
            this.btn_Play.UseVisualStyleBackColor = false;
            this.btn_Play.Click += new System.EventHandler(this.btn_Play_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 91);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 13);
            this.label11.TabIndex = 34;
            this.label11.Text = "Password";
            // 
            // txtB_Kodi_User
            // 
            this.txtB_Kodi_User.AllowDrop = true;
            this.txtB_Kodi_User.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.txtB_Kodi_User.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtB_Kodi_User.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtB_Kodi_User.ForeColor = System.Drawing.Color.White;
            this.txtB_Kodi_User.Location = new System.Drawing.Point(6, 66);
            this.txtB_Kodi_User.Name = "txtB_Kodi_User";
            this.txtB_Kodi_User.Size = new System.Drawing.Size(158, 22);
            this.txtB_Kodi_User.TabIndex = 28;
            this.txtB_Kodi_User.WordWrap = false;
            this.txtB_Kodi_User.TextChanged += new System.EventHandler(this.txtB_Kodi_User_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 47);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 13);
            this.label10.TabIndex = 33;
            this.label10.Text = "Username";
            // 
            // txtB_Kodi_Address
            // 
            this.txtB_Kodi_Address.AllowDrop = true;
            this.txtB_Kodi_Address.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.txtB_Kodi_Address.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtB_Kodi_Address.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtB_Kodi_Address.ForeColor = System.Drawing.Color.White;
            this.txtB_Kodi_Address.Location = new System.Drawing.Point(6, 22);
            this.txtB_Kodi_Address.Name = "txtB_Kodi_Address";
            this.txtB_Kodi_Address.Size = new System.Drawing.Size(158, 22);
            this.txtB_Kodi_Address.TabIndex = 30;
            this.txtB_Kodi_Address.Text = "192.168.0.178:80";
            this.txtB_Kodi_Address.WordWrap = false;
            this.txtB_Kodi_Address.TextChanged += new System.EventHandler(this.txtB_Kodi_Address_TextChanged);
            // 
            // txtB_Kodi_Pass
            // 
            this.txtB_Kodi_Pass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.txtB_Kodi_Pass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtB_Kodi_Pass.ForeColor = System.Drawing.SystemColors.Window;
            this.txtB_Kodi_Pass.Location = new System.Drawing.Point(6, 110);
            this.txtB_Kodi_Pass.Name = "txtB_Kodi_Pass";
            this.txtB_Kodi_Pass.PasswordChar = '•';
            this.txtB_Kodi_Pass.Size = new System.Drawing.Size(158, 20);
            this.txtB_Kodi_Pass.TabIndex = 31;
            this.txtB_Kodi_Pass.MouseLeave += new System.EventHandler(this.txtB_Kodi_Pass_MouseLeave_1);
            this.txtB_Kodi_Pass.MouseHover += new System.EventHandler(this.txtB_Kodi_Pass_MouseHover_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(78, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 41;
            this.label1.Text = "Buffer (MB)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(120, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 43;
            this.label2.Text = "Volume";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // KodiPlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(330, 447);
            this.Controls.Add(this.pn_kodi);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KodiPlay";
            this.Text = "KodiPlay";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KodiPlay_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.KodiPlay_FormClosed);
            this.Load += new System.EventHandler(this.KodiPlay_Load);
            this.pn_kodi.ResumeLayout(false);
            this.pn_kodi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pn_Kodi_Input.ResumeLayout(false);
            this.pn_Kodi_Input.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_Volume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pn_kodi;
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.Label lbl_KodiPlayInfo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btn_ConnectKodi;
        private System.Windows.Forms.Button btn_Play;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtB_Kodi_User;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtB_Kodi_Address;
        private System.Windows.Forms.MaskedTextBox txtB_Kodi_Pass;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown nUD_Volume;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Input_Select;
        private System.Windows.Forms.Button btn_Input_Down;
        private System.Windows.Forms.Button btn_Input_Left;
        private System.Windows.Forms.Button btn_Input_Right;
        private System.Windows.Forms.Button btn_Input_Up;
        private System.Windows.Forms.Button btn_Input_Back;
        private System.Windows.Forms.Panel pn_Kodi_Input;
        private System.Windows.Forms.Button btn_Input_ContexMenue;
        private System.Windows.Forms.Button btn_Input_Home;
        private System.Windows.Forms.Button btn_Input_Info;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_Input_Mute;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}