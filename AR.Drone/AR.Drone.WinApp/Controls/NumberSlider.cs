using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AR.Drone.WinApp.Controls
{
    public partial class NumberSlider : UserControl
    {
        public event EventHandler ValueChanged { add { tbLowH.ValueChanged += value; } remove { tbLowH.ValueChanged -= value; } }
        public int Value { get { return tbLowH.Value; } set { tbLowH.Value = value; } }
        public NumberSlider()
        {
            InitializeComponent();
        }

        private void tbLowH_ValueChanged(object sender, EventArgs e)
        {
            lbLowH.Text = tbLowH.Value.ToString();
        }
    }
}
