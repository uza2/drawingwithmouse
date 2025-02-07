using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace drawingwithmouse
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        Graphics g;
        int w, h;
        int xc, yc;
        int r, oldr;
        int x0, y0, xn, yn;
        bool firstclick=true , fstclk=true ;
        List<Point> curve = new List<Point>(); int pc=0;
        Color frontclr, backclr;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            w = pictureBox1.Width;
            h = pictureBox1.Height;
            bmp = new Bitmap(w, h);
            g = Graphics.FromImage(bmp);
            frontclr = Color.Black;
            backclr = Color.White;
            pictureBox1.BackColor = backclr;
            
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (radioButton1.Checked)
            {
                if (e.Button == MouseButtons.Left)
                    if (firstclick)
                    {
                        x0 = e.X;
                        y0 = e.Y;
                        xn = x0;
                        yn = y0;
                        firstclick = false ;
                    }
                    else
                    {
                        g.DrawLine(new Pen(backclr, 2), x0, y0, xn,yn);
                        xn = e.X;
                        yn = e.Y;
                        g.DrawLine (new Pen(frontclr, 2), x0,y0,xn,yn);
                        pictureBox1.Image = bmp;
                    }
            }
            else if (radioButton2.Checked)
            {
                if (e.Button == MouseButtons.Left)
                    if (firstclick)
                    {
                        x0 = e.X;
                        y0 = e.Y;
                        xn = x0;
                        yn = y0;
                        firstclick = false ;
                    }
                    else
                    {
                       // g.DrawLine(new Pen(backclr, 2), x0, y0, xn, yn);
                        xn = e.X;
                        yn = e.Y;
                        g.DrawLine(new Pen(frontclr, 2), x0, y0, xn, yn);
                        x0 = xn;
                        y0 = yn;
                        pictureBox1.Image = bmp;
                    }
            }
            else if (radioButton3.Checked)
            {
               if(!fstclk)
                {
                    g.DrawLine(new Pen(backclr, 2), x0, y0, xn, yn);
                    xn = e.X;
                    yn = e.Y;
                    g.DrawLine(new Pen(frontclr, 2), x0, y0, xn, yn);
                    pictureBox1.Image = bmp;
                }
            }
            if (radioButton4.Checked)
            {
                if (e.Button == MouseButtons.Left)
                    if (firstclick)
                    {
                        xc = e.X;
                        yc = e.Y;
                        oldr = 0;
                        firstclick = false;
                    }
                    else
                    {
                        g.DrawEllipse(new Pen(backclr, 2), xc - oldr / 2, yc - oldr / 2, oldr, oldr);
                        r = (int)(Math.Sqrt(Math.Pow(e.X - xc, 2) + Math.Pow(e.Y - yc, 2)));
                        oldr = r;
                        g.DrawEllipse(new Pen(frontclr, 2), xc - r / 2, yc - r / 2, r, r);
                        pictureBox1.Image = bmp;
                    }
            }
        
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            firstclick = true ;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bmp = new Bitmap(w,h);
            pictureBox1.Image = bmp;
            g = Graphics.FromImage(bmp);
            firstclick = true ;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            
            if (radioButton3.Checked)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (fstclk)
                    {
                        x0 = e.X;
                        y0 = e.Y;
                        xn = x0;
                        yn = y0;
                        fstclk = false ;
                    }
                    else
                    {
                        xn = e.X;
                        yn = e.Y;
                        g.DrawLine(new Pen(frontclr, 2), x0, y0, xn, yn);
                        x0 = xn;
                        y0 = yn;
                        pictureBox1.Image = bmp;

                    }
                }
                if (pc < 4)
                {
                    curve.Add(new Point(xn,yn));
                    pc++;
                }
                
            }
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            fstclk = true ;
            pc = 0;
            
            if (checkBox1.Checked && curve.Count > 3)
            {
                g.DrawBezier(new Pen(Color.Blue, 2), curve[0], curve[1], curve[2], curve[3]);
                
                pictureBox1.Image = bmp;
            }
            curve.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            frontclr = colorDialog1.Color;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            backclr = colorDialog1.Color;
            pictureBox1.BackColor = backclr;
        }
    }
}
