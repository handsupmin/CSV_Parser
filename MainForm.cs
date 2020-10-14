using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarketBrowser
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader(new FileStream("market.csv", FileMode.Open), Encoding.Default);

            string str = "";

            var lines = sr.ReadLine();
            var headers = lines.Split(',');

            foreach (string header in headers)
            {
                textBoxCSVView.Text += header + "\t";
            }

            /*
            while (sr.EndOfStream == false)
            {
                lines = sr.ReadLine();
                var values = lines.Split(',');
                str += values[0] + "\r\n";
            }

            textBoxCSVView.Text = str;
            */

            sr.Close();
        }
    }
}
