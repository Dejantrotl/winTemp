using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Net;
using System.Diagnostics;

namespace temp01
{
    public partial class Form1 : Form
    {

        private int i;
        private int counter;
        int _word = 0;
        string txt;

        public Form1()
        {
            InitializeComponent();
            getTemp();
            timer1.Start();
            timer2.Start();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            txt = "Loading...";
            _word = txt.Length;
            lbLoading.Text = "";
            
            // Link 
            LinkLabel.Link link = new LinkLabel.Link();
            link.LinkData = "https://www.popel.eu/rasp/index.php";
            linkPopel.Links.Add(link);
        }

        public string getTemp()
        {
            using (WebClient client = new WebClient())
            {
                                    // test link temp3.php(working)
                string url = "http://192.168.0.147/temp3.php";
                string content = client.DownloadString(url);
                return content;
            }
        }
        public void startTimer()
        {
            i++;
            if (i > _word)
            {
                i = 0;
                lbLoading.Text = "";
            }
            else
            {
                lbLoading.Text = txt.Substring(0, i);
            }
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            i++;
            lbTemp.Text = getTemp();
        }
        
        private void timer2_Tick(object sender, EventArgs e)
        {
            i++;
            if (i > _word)
            {
                i = 0;
                lbLoading.Text = "";
            }
            else
            {
                lbLoading.Text = txt.Substring(0, i);
                counter++;
                if (counter == 90)
                {
                    timer2.Stop();
                    lbLoading.Text = "Temp loaded";
                }
            }
        }

        private void linkPopel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }
    }
}
