using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lexi
{
    public partial class radiochannels : Form
    {
        public radiochannels()
        {
            InitializeComponent();
        }
        
        private void label1_Click(object sender, EventArgs e)
        {
            label1Play();
        }
        public void label1Play()
        {
            pic1.Width = 30;
            pic2.Width = 40;
            pic3.Width = 40;
            pic4.Width = 40;
            pic5.Width = 40;
            axWindowsMediaPlayer1.URL = "http://powerfm.listenpowerapp.com/powerfm/mpeg/icecast.audio";
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }
        private void label2_Click(object sender, EventArgs e)
        {
            label2play();
        }
        public void label2play()
        {
            pic2.Width = 30;
            pic1.Width = 40;
            pic3.Width = 40;
            pic4.Width = 40;
            pic5.Width = 40;
            axWindowsMediaPlayer1.URL = "http://fenomen.listenfenomen.com/fenomen/128/icecast.audio";
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }
        private void label3_Click(object sender, EventArgs e)
        {
            label3play();
        }
        public void label3play()
        {
            pic1.Width = 40;
            pic2.Width = 40;
            pic3.Width = 30;
            pic4.Width = 40;
            pic5.Width = 40;
            axWindowsMediaPlayer1.URL = "http://shoutcast.radyogrup.com:1030/;";
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }
        private void label4_Click(object sender, EventArgs e)
        {
            label4play();
        }
        public void label4play()
        {
            pic1.Width = 40;
            pic2.Width = 40;
            pic3.Width = 40;
            pic4.Width = 30;
            pic5.Width = 40;
            axWindowsMediaPlayer1.URL = "https://playerservices.streamtheworld.com/api/livestream-redirect/JOY_FM.mp3";
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }
        private void label5_Click(object sender, EventArgs e)
        {
            label5play();
        }
        public void label5play()
        {
            pic1.Width = 40;
            pic2.Width = 40;
            pic3.Width = 40;
            pic4.Width = 40;
            pic5.Width = 30;
            axWindowsMediaPlayer1.URL = "http://n10101m.mediatriple.net/videoonlylive/mtkgeuihrlfwlive/u_stream_5c9e2f95dcb16_1/playlist.m3u8";
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }
        private void label1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            RadioStop();
        }
        public void RadioStop()
        {
            pic1.Width = 40;
            pic2.Width = 40;
            pic3.Width = 40;
            pic4.Width = 40;
            pic5.Width = 40;
            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }

        private void label2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            RadioStop();

        }

        private void label3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            RadioStop();

        }

        private void label4_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            RadioStop();

        }

        private void label5_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            RadioStop();

        }
    }
}
