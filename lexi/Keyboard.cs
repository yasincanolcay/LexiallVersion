using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace lexi
{
    public partial class Keyboard : Form
    {
        bool capslock = false;
        bool shift = false;
        bool ctrl = false;
        bool alt = false;
        public Keyboard()
        {
            InitializeComponent();
        }
        //tum formların ustunde etkin kalsın
        //diger formların odagını etkilemesin
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams param = base.CreateParams;
                param.ExStyle |= 0x08000000;
                return param;

            }
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void guna2GradientButton15_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{q}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("q");

            }

            if (ctrl == true)
            {
                SendKeys.Send("^{q}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }

            if (alt == true)
            {
                SendKeys.Send("%{q}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }
        private void guna2GradientButton16_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{a}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                if (ctrl == false)
                {
                    SendKeys.Send("a");

                }
            }
            if (ctrl == true)
            {
                SendKeys.Send("^{a}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }

            if (alt == true)
            {
                SendKeys.Send("%{a}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton17_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{z}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                if (ctrl == false)
                {
                    SendKeys.Send("z");

                }

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{z}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }

            if (alt == true)
            {
                SendKeys.Send("%{z}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton18_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{w}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("w");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{w}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{w}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton19_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{s}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                if (ctrl == false)
                {
                    SendKeys.Send("s");

                }
            }
            if (ctrl == true)
            {
                SendKeys.Send("^{s}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{s}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton20_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{x}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                if (ctrl == false)
                {
                    SendKeys.Send("x");

                }
            }
            if (ctrl == true)
            {
                SendKeys.Send("^{x}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{x}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton21_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{e}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("e");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{e}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{e}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton22_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{d}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                if (ctrl == false)
                {
                    SendKeys.Send("d");

                }
            }
            if (ctrl == true)
            {
                SendKeys.Send("^{d}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{d}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton23_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{c}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                if (ctrl == false)
                {
                    SendKeys.Send("c");

                }

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{c}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{c}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton24_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{r}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("r");

            };
            if (ctrl == true)
            {
                SendKeys.Send("^{r}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{r}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton25_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{f}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("f");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{f}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{f}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton26_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{v}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                if (ctrl == false)
                {
                    SendKeys.Send("v");

                }
            }
            if (ctrl == true)
            {
                SendKeys.Send("^{v}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{v}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton27_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{t}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("t");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{t}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{t}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton28_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{g}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("g");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{g}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{g}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton29_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{b}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("b");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{b}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{b}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton30_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{y}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                if (ctrl == false)
                {
                    SendKeys.Send("y");

                }
            }
            if (ctrl == true)
            {
                SendKeys.Send("^{y}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{y}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton31_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{h}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("h");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{h}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{h}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton32_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{n}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("n");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{n}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{n}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton33_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{u}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("u");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{u}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{u}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton34_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{j}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("j");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{j}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{j}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton35_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{m}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("m");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{m}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{m}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton36_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{ı}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("ı");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{ı}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{ı}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton37_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{k}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("k");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{k}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{k}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton38_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{ö}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("ö");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{ö}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{ö}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton39_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{o}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("o");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{o}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{o}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton40_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{l}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("l");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{l}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{l}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton41_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{ç}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("ç");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{ç}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{ç}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton42_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{p}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("p");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{p}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{p}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton43_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{ş}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("ş");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{ş}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{ş}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton44_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{.}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send(".");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{.}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{.}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton45_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{ğ}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("ğ");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{ğ}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{ğ}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton46_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{i}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("i");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{i}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{i}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton48_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{ü}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("ü");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{ü}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{ü}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton49_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{'}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("'");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{'}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{'}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton50_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{ENTER}");

        }

        private void guna2GradientButton14_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{BACKSPACE}");

        }

        private void guna2GradientButton51_Click(object sender, EventArgs e)
        {
            if (capslock == false)
            {
                guna2GradientButton51.FillColor2 = Color.FromArgb(50, 77, 165);
                ToggleCapsLock(true);
                capslock = true;
            }
            else
            {
                guna2GradientButton51.FillColor2 = Color.FromArgb(255, 77, 165);
                ToggleCapsLock(false);
                capslock = false;
            }

        }

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
        static void ToggleCapsLock(bool onOrOff)
        {
            if (IsKeyLocked(Keys.CapsLock) == onOrOff) return;
            keybd_event(0x14, 0x45, 0x1, (UIntPtr)0);
            keybd_event(0x14, 0x45, 0x1 | 0x2, (UIntPtr)0);

        }

        private void guna2GradientButton56_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{TAB}");
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            
            if (shift == true)
            {
                SendKeys.Send("+{\"}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("{\"}");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{\"}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{\"}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton57_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{ESC}");
        }

        private void guna2GradientButton52_Click(object sender, EventArgs e)
        {
            if (shift == false)
            {
                shift = true;
                guna2GradientButton52.FillColor2 = Color.FromArgb(55, 77, 165);
            }
            else
            {
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
            }
        }

        private void guna2GradientButton47_Click(object sender, EventArgs e)
        {
            if (shift == false)
            {
                shift = true;
                guna2GradientButton47.FillColor2 = Color.FromArgb(55, 77, 165);
            }
            else
            {
                shift = false;
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);
            }
        }

        private void guna2GradientButton53_Click(object sender, EventArgs e)
        {
            if (ctrl == false)
            {
                ctrl = true;
                guna2GradientButton53.FillColor2 = Color.FromArgb(55, 77, 165);
            }
            else
            {
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
        }

        private void guna2GradientButton55_Click(object sender, EventArgs e)
        {
            if (alt == false)
            {
                alt = true;
                guna2GradientButton55.FillColor2 = Color.FromArgb(55, 77, 165);
            }
            else
            {
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);
            }
        }

        private void guna2GradientButton54_Click(object sender, EventArgs e)
        {
            SendKeys.Send(" ");
        }
        //
        //RAKAMLAR
        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{1}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("1");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{1}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{1}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{2}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("2");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{2}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{2}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }

        }

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{3}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("3");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{3}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{3}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }

        }

        private void guna2GradientButton5_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{4}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("4");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{4}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{4}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }

        }

        private void guna2GradientButton6_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{5}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("5");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{5}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{5}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }

        }

        private void guna2GradientButton7_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{6}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("6");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{6}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{6}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton8_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{7}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("7");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{7}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{7}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton9_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{8}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("8");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{8}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{8}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton10_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{9}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("9");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{9}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{9}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton11_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{0}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("0");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{0}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{0}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton12_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{*}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("*");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{*}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{*}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        private void guna2GradientButton13_Click(object sender, EventArgs e)
        {
            if (shift == true)
            {
                SendKeys.Send("+{-}");
                shift = false;
                guna2GradientButton52.FillColor2 = Color.FromArgb(255, 77, 165);
                guna2GradientButton47.FillColor2 = Color.FromArgb(255, 77, 165);

            }
            else
            {
                SendKeys.Send("-");

            }
            if (ctrl == true)
            {
                SendKeys.Send("^{-}");
                ctrl = false;
                guna2GradientButton53.FillColor2 = Color.FromArgb(255, 77, 165);
            }
            if (alt == true)
            {
                SendKeys.Send("%{-}");
                alt = false;
                guna2GradientButton55.FillColor2 = Color.FromArgb(255, 77, 165);

            }
        }

        //backspace - silme tusu icin basılı tutma islemi
        private void guna2GradientButton14_MouseDown(object sender, MouseEventArgs e)
        {
            backspaceTM.Enabled = true;
            backspaceTM.Start();
        }

        private void guna2GradientButton14_MouseUp(object sender, MouseEventArgs e)
        {
            delete.Enabled = false;
            delete.Stop();

            backspaceTM.Enabled = false;
            backspaceTM.Stop();
        }

        private void backspaceTM_Tick(object sender, EventArgs e)
        {
            delete.Enabled = true;
            delete.Start();

            backspaceTM.Enabled = false;
            backspaceTM.Stop();
        }

        private void delete_Tick(object sender, EventArgs e)
        {
            SendKeys.Send("{BACKSPACE}");
        }

        private void guna2GradientButton58_Click(object sender, EventArgs e)
        {
            SendKeys.Send("/");
        }
    }
}
