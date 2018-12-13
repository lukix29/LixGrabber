namespace LixGrabber
{
    partial class FormDownload
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
            this.btn_Start = new System.Windows.Forms.Button();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.nUD_ParallelDLs = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.cB_useAria = new System.Windows.Forms.CheckBox();
            this.nUD_consPerDL = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.lstB_Main = new LixGrabber.DoubleBufferedListBox();
            this.lbl_info = new System.Windows.Forms.Label();
            this.btn_pause = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_ParallelDLs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_consPerDL)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Start
            // 
            this.btn_Start.AutoSize = true;
            this.btn_Start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Start.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Start.Location = new System.Drawing.Point(4, 4);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(47, 28);
            this.btn_Start.TabIndex = 1;
            this.btn_Start.Text = "Start";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // btn_Stop
            // 
            this.btn_Stop.AutoSize = true;
            this.btn_Stop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Stop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Stop.Location = new System.Drawing.Point(67, 4);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(48, 28);
            this.btn_Stop.TabIndex = 2;
            this.btn_Stop.Text = "Stop";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // nUD_ParallelDLs
            // 
            this.nUD_ParallelDLs.BackColor = System.Drawing.Color.Black;
            this.nUD_ParallelDLs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nUD_ParallelDLs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nUD_ParallelDLs.ForeColor = System.Drawing.Color.White;
            this.nUD_ParallelDLs.Location = new System.Drawing.Point(247, 5);
            this.nUD_ParallelDLs.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nUD_ParallelDLs.Name = "nUD_ParallelDLs";
            this.nUD_ParallelDLs.Size = new System.Drawing.Size(47, 26);
            this.nUD_ParallelDLs.TabIndex = 6;
            this.nUD_ParallelDLs.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nUD_ParallelDLs.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(121, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Parallel Downloads";
            // 
            // cB_useAria
            // 
            this.cB_useAria.AutoSize = true;
            this.cB_useAria.Checked = true;
            this.cB_useAria.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cB_useAria.Enabled = false;
            this.cB_useAria.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cB_useAria.Location = new System.Drawing.Point(536, 92);
            this.cB_useAria.Name = "cB_useAria";
            this.cB_useAria.Size = new System.Drawing.Size(169, 20);
            this.cB_useAria.TabIndex = 9;
            this.cB_useAria.Text = "Use Aria2c Downloader";
            this.cB_useAria.UseVisualStyleBackColor = true;
            this.cB_useAria.Visible = false;
            this.cB_useAria.CheckedChanged += new System.EventHandler(this.cB_useAria_CheckedChanged);
            // 
            // nUD_consPerDL
            // 
            this.nUD_consPerDL.BackColor = System.Drawing.Color.Black;
            this.nUD_consPerDL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nUD_consPerDL.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nUD_consPerDL.ForeColor = System.Drawing.Color.White;
            this.nUD_consPerDL.Location = new System.Drawing.Point(479, 5);
            this.nUD_consPerDL.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nUD_consPerDL.Name = "nUD_consPerDL";
            this.nUD_consPerDL.Size = new System.Drawing.Size(47, 26);
            this.nUD_consPerDL.TabIndex = 10;
            this.nUD_consPerDL.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nUD_consPerDL.ValueChanged += new System.EventHandler(this.nUD_consPerDL_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(300, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(176, 16);
            this.label2.TabIndex = 11;
            this.label2.Text = "Connections per Downloads";
            // 
            // lstB_Main
            // 
            this.lstB_Main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstB_Main.BackColor = System.Drawing.Color.Black;
            this.lstB_Main.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstB_Main.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstB_Main.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstB_Main.ForeColor = System.Drawing.Color.White;
            this.lstB_Main.FormattingEnabled = true;
            this.lstB_Main.IntegralHeight = false;
            this.lstB_Main.ItemHeight = 25;
            this.lstB_Main.Location = new System.Drawing.Point(-1, 38);
            this.lstB_Main.Name = "lstB_Main";
            this.lstB_Main.Size = new System.Drawing.Size(1009, 499);
            this.lstB_Main.TabIndex = 7;
            // 
            // lbl_info
            // 
            this.lbl_info.AutoSize = true;
            this.lbl_info.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_info.Location = new System.Drawing.Point(533, 10);
            this.lbl_info.Name = "lbl_info";
            this.lbl_info.Size = new System.Drawing.Size(32, 16);
            this.lbl_info.TabIndex = 12;
            this.lbl_info.Text = "Info:";
            // 
            // btn_pause
            // 
            this.btn_pause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_pause.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_pause.Location = new System.Drawing.Point(4, 4);
            this.btn_pause.Name = "btn_pause";
            this.btn_pause.Size = new System.Drawing.Size(57, 28);
            this.btn_pause.TabIndex = 13;
            this.btn_pause.Text = "Pause";
            this.btn_pause.UseVisualStyleBackColor = true;
            this.btn_pause.Visible = false;
            this.btn_pause.Click += new System.EventHandler(this.btn_pause_Click);
            // 
            // FormDownload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1008, 536);
            this.Controls.Add(this.btn_pause);
            this.Controls.Add(this.lbl_info);
            this.Controls.Add(this.nUD_consPerDL);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cB_useAria);
            this.Controls.Add(this.lstB_Main);
            this.Controls.Add(this.nUD_ParallelDLs);
            this.Controls.Add(this.btn_Stop);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "FormDownload";
            this.Text = "Downloader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDownload_FormClosing);
            this.Load += new System.EventHandler(this.FormDownload_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nUD_ParallelDLs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_consPerDL)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NumericUpDown nUD_ParallelDLs;
        private DoubleBufferedListBox lstB_Main;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cB_useAria;
        private System.Windows.Forms.NumericUpDown nUD_consPerDL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_info;
        private System.Windows.Forms.Button btn_pause;
    }
}