using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.InteropServices;
using LixGrabber.Properties;

namespace LixGrabber
{
    public partial class FormSettings : Form
    {
        private bool changed = false;
        private Dictionary<string, Control> controls = new Dictionary<string, Control>();

        public FormSettings()
        {
            InitializeComponent();
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            Settings.Default.Reset();
            loadSettings();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            Settings.Default.Save();
            loadSettings();
        }

        private void FormSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (changed)
            {
                var res = MessageBox.Show("Save Settings and restart Lix-Grabber?", "Settings Changed", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (res == DialogResult.Yes)
                {
                    Settings.Default.Save();
                }
                else if (res == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void loadSettings()
        {
            changed = false;
            this.panel1.Controls.Clear();
            controls.Clear();
            int y = 10;
            List<System.Configuration.SettingsPropertyValue> list = new List<System.Configuration.SettingsPropertyValue>();
            int width = 0;
            using (Graphics g = this.CreateGraphics())
            {
                foreach (System.Configuration.SettingsPropertyValue currentProperty in Properties.Settings.Default.PropertyValues)
                {
                    list.Add(currentProperty);
                    float siz = g.MeasureString(currentProperty.Name, this.Font).Width;
                    if (width < siz) width = (int)(siz + 0.5f);
                }
            }
            width += 20;
            foreach (var currentProperty in list.OrderBy(t => t.Name))
            {
                Panel p = new Panel();
                p.Left = 0;
                p.Top = y;
                p.BorderStyle = BorderStyle.FixedSingle;

                Label l = new Label();
                l.Text = currentProperty.Name;
                l.Left = 10;
                l.Top = 5;
                l.AutoSize = true;
                p.Controls.Add(l);

                if (currentProperty.PropertyValue.GetType().Equals(typeof(int)))
                {
                    NumericUpDown nud = new NumericUpDown();

                    nud.Minimum = 1;
                    nud.Maximum = 16;
                    nud.BackColor = Color.FromArgb(42, 42, 42);
                    nud.ForeColor = Color.White;
                    nud.BorderStyle = BorderStyle.FixedSingle;
                    nud.Value = (int)currentProperty.PropertyValue;
                    nud.Left = width;
                    nud.Top = 5;
                    nud.Width = 400;
                    p.Height = nud.Height + 10;
                    nud.ValueChanged += (o, e) =>
                    {
                        changed = true;
                        currentProperty.PropertyValue = (int)nud.Value;
                    };
                    controls.Add(currentProperty.Name, nud);
                    p.Controls.Add(nud);
                }
                else if (currentProperty.PropertyValue.GetType().Equals(typeof(string)))
                {
                    TextBox nud = new TextBox();
                    nud.BackColor = Color.FromArgb(42, 42, 42);
                    nud.ForeColor = Color.White;
                    nud.BorderStyle = BorderStyle.FixedSingle;
                    nud.Text = currentProperty.PropertyValue.ToString();
                    nud.Left = width;
                    nud.Top = 5;
                    nud.Width = 400;
                    p.Height = nud.Height + 10;
                    nud.TextChanged += (o, e) =>
                    {
                        changed = true;
                        currentProperty.PropertyValue = nud.Text;
                    };
                    controls.Add(currentProperty.Name, nud);
                    p.Controls.Add(nud);
                }
                else if (currentProperty.PropertyValue.GetType().Equals(typeof(bool)))
                {
                    CheckBox nud = new CheckBox();
                    nud.BackColor = Color.FromArgb(42, 42, 42);
                    nud.ForeColor = Color.White;
                    nud.FlatStyle = FlatStyle.Flat;
                    nud.Text = "";
                    nud.Checked = (bool)currentProperty.PropertyValue;
                    nud.Left = width;
                    nud.Top = 5;
                    p.Height = nud.Height + 10;
                    nud.CheckedChanged += (o, e) =>
                    {
                        changed = true;
                        currentProperty.PropertyValue = nud.Checked;
                    };
                    controls.Add(currentProperty.Name, nud);
                    p.Controls.Add(nud);
                }
                p.Width = this.Width;
                y += p.Height + 5;
                this.panel1.Controls.Add(p);
            }
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            loadSettings();
        }
    }
}