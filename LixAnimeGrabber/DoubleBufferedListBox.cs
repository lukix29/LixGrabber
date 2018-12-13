using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LixGrabber
{
    public partial class DoubleBufferedListBox : ListBox
    {
        public DoubleBufferedListBox()
        {
            InitializeComponent();
            //this.SetStyle(ControlStyles.UserPaint, this.DrawMode != DrawMode.Normal);
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, this.DrawMode != DrawMode.Normal);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }
    }
}