using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lix.SeriesManager;

namespace LixGrabber
{
    public partial class FormSearch : Form
    {
        private FormMain Form1Main;

        private string lastSearch = "";

        public FormSearch(FormMain form1)
        {
            InitializeComponent();
            Form1Main = form1;
        }

        private void BrowserSetHtml(string html)
        {
            webBrowser1.AllowNavigation = true;
            webBrowser1.Navigate("about:blank");
            if (webBrowser1.Document != null)
            {
                webBrowser1.Document.Write(string.Empty);
            }
            webBrowser1.DocumentText = html;
            //webBrowser1.AllowNavigation = false;
        }

        private void btn_Add_Click_1(object sender, EventArgs e)
        {
            var item = (SearchResult)lstB_Series.SelectedItem;
            Form1Main.txtB_Url.Text = item.Link;
            Form1Main.btn_FetchSeriesInfo.PerformClick();
        }

        private async void btn_Search_Click(object sender, EventArgs e)
        {
            var key = txtB_keyword.Text.Trim().ToLower();
            try
            {
                if (key.Length > 0 && !lastSearch.Equals(key))
                {
                    this.UseWaitCursor = true;
                    btn_Search.Enabled = false;
                    lstB_Series.Items.Clear();
                    lbl_count.Text = "0";
                    int cnt = 0;
                    var list = await SearchResult.Search(key, (li) =>
                    {
                        this.Invoke(new Action(() =>
                        {
                            foreach (var item in li)
                            {
                                lstB_Series.Items.Add(item);
                                cnt++;
                            }
                        }));
                    });
                    lbl_count.Text = cnt.ToString();
                    //if (list != null)
                    //{
                    //    int cnt = 0;
                    //    foreach (var item in list)
                    //    {
                    //        lstB_Series.Items.Add(item);
                    //        cnt++;
                    //    }
                    //}
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }
            finally
            {
                lastSearch = key;
                this.UseWaitCursor = false;
                btn_Search.Enabled = true;
            }
        }

        private void FormSearch_Load(object sender, EventArgs e)
        {
            lstB_Series.DrawMode = DrawMode.OwnerDrawFixed;
            lstB_Series.DrawItem += LstB_Series_DrawItem;

            BrowserSetHtml("<style>body{background-color: black; color: white;}</style><html><body></body></html>");
        }

        private void LstB_Series_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawFocusRectangle();
            e.DrawBackground();
            if (e.Index >= 0)
            {
                var sr = (SearchResult)lstB_Series.Items[e.Index];
                var color = sr.Site == SiteType._9Anime ? Color.LightGreen : Color.LightCyan;
                TextRenderer.DrawText(e.Graphics, sr.ToString(), e.Font, e.Bounds, color, e.BackColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
            }
        }

        private async void lstB_Series_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (((ListBox)sender).SelectedIndex >= 0)
                {
                    var item = (SearchResult)((ListBox)sender).SelectedItem;
                    if (string.IsNullOrEmpty(item.Cover))
                    {
                        item.Cover = await SeriesLoader.GetSeriesCover(item.Link);
                    }
                    btn_Add.Enabled = true;
                    var descr = await item.Description;
                    var html = "<style>a{color: white;} body{background-color: black; color: white;}</style><html><body>"
                        + "<h2>" + Enum.GetName(typeof(SiteType), item.Site).Trim('_') + "</h2>"
                        + "<img src=\"" + item.Cover + "\" height=\"300\" alt=\"Cover\"><br>"
                        + descr.Replace("Watch now!", "").Replace("h1", "h2").Replace("<div class=\"preview\">Preview</div>", "")
                        + "<br>"
                        + "<a href=\"" + item.Link + "\">" + item.Link + "</a>"
                        + "</body></html>";

                    BrowserSetHtml(html);
                }
                else
                {
                    btn_Add.Enabled = false;
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }
        }

        private void richTextBox1_LinkClicked_1(object sender, LinkClickedEventArgs e)
        {
            var item = (SearchResult)lstB_Series.SelectedItem;
            System.Diagnostics.Process.Start(item.Link);
        }

        private void txtB_keyword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Search.PerformClick();
                e.SuppressKeyPress = false;
                e.Handled = false;
                return;
            }
        }

        private void txtB_keyword_TextChanged(object sender, EventArgs e)
        {
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webBrowser1.AllowNavigation = false;
        }
    }
}