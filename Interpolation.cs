using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace NumberMethods
{
    public partial class Interpolation : Form
    {
        public Interpolation()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                Lagrange();
            }
            if (checkBox2.Checked == true)
            {
                CubicSpline();
            }
            pictureBox1.Refresh();
            GC.Collect();
        }
        private void Lagrange()
        {
            int n = Convert.ToInt32(textBox2.Text);
            double[] x = Array.ConvertAll(textBox1.Text.Split(',').Select(e => e.Replace('.', ',')).ToArray(), Convert.ToDouble);
            double[] y = Array.ConvertAll(textBox3.Text.Split(',').Select(e => e.Replace('.', ',')).ToArray(), Convert.ToDouble);
            double[] sortedX = x;
            Array.Sort(sortedX);
            double maxX = sortedX[sortedX.Length - 1];
            double minX = sortedX[0];

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                //Lagrange
                Pen pen = new Pen(Color.Black, 3);
                Pen pen2 = new Pen(Color.Black, 1);
                Pen pen3 = new Pen(Color.Red, 2);
                Pen pen4 = new Pen(Color.Blue, 1);
                List<PointF> pointslist = new List<PointF>();

                //Draw curve
                for (float X = (float)minX - 1; X < maxX + 1; X += 0.1f)
                {
                    double L = 0;
                    double l = 0;
                    double dividend = 1;
                    double divisor = 1;
                    int j = 0;
                    do
                    {
                        if (j == 0)
                        {
                            dividend *= (X - x[1]) * (X - x[2]);
                            divisor *= (x[j] - x[1]) * (x[j] - x[2]);
                            l = dividend / divisor;
                        }
                        else if (j >= n)
                        {
                            dividend *= (X - x[n - 2]) * (X - x[n - 1]);
                            divisor *= (x[j] - x[n - 2]) * (x[j] - x[n - 1]);
                            l = dividend / divisor;
                        }
                        else
                        {
                            dividend *= (X - x[j - 1]) * (X - x[j + 1]);
                            divisor *= (x[j] - x[j - 1]) * (x[j] - x[j + 1]);
                            l = dividend / divisor;
                        }
                        if (!Double.IsNaN(l) && !Double.IsInfinity(l))
                        {
                            L += y[j] * l;
                        }
                        dividend = 1;
                        divisor = 1;
                        j++;
                    }
                    while (j < n + 1);

                    g.DrawRectangle(pen4, X * trackBar1.Value, (pictureBox1.Height / 2), 1, 1);

                    pointslist.Add(new PointF(X * trackBar1.Value, -(float)L * trackBar2.Value + (pictureBox1.Height / 2)));
                }
                //Draw points
                listBox1.Items.Clear();
                for (int X = (int)minX - 1; X < maxX + 1; X++)
                {
                    double L = 0;
                    double l = 0;
                    double dividend = 1;
                    double divisor = 1;
                    int j = 0;
                    do
                    {
                        if (j == 0)
                        {
                            dividend *= (X - x[1]) * (X - x[2]);
                            divisor *= (x[j] - x[1]) * (x[j] - x[2]);
                            l = dividend / divisor;
                            //x = 0 - 2, L = 24
                        }
                        else if (j >= n)
                        {
                            dividend *= (X - x[n - 2]) * (X - x[n - 1]);
                            divisor *= (x[j] - x[n - 2]) * (x[j] - x[n - 1]);
                            l = dividend / divisor;
                            //x = 0 - 1, L = 6

                        }
                        else
                        {
                            dividend *= (X - x[j - 1]) * (X - x[j + 1]);
                            divisor *= (x[j] - x[j - 1]) * (x[j] - x[j + 1]);
                            l = dividend / divisor;
                            //x = 0 - -2, L = -8
                        }
                        if (!Double.IsNaN(l) && !Double.IsInfinity(l))
                        {
                            L += y[j] * l;
                            //x = 0, L= 22
                            //x = 1, L= 12
                        }
                        dividend = 1;
                        divisor = 1;
                        j++;
                    }
                    while (j < n + 1);

                    PointF p = new PointF(X * trackBar1.Value, -(float)L * trackBar2.Value + (pictureBox1.Height / 2));
                    g.DrawString(X.ToString(), new Font(FontFamily.GenericSerif, 8), Brushes.Black, new PointF(p.X, pictureBox1.Height - 30));
                    g.DrawString(L.ToString(), new Font(FontFamily.GenericSerif, 8), Brushes.Black, new PointF(0, p.Y));
                    if (x.Contains(MathF.Round(X, 0)))
                    {
                        listBox1.Items.Add($"X: {X}, Y: {L}");
                        g.DrawRectangle(pen3, p.X, p.Y, 5, 5);
                    }
                }
                //g.DrawLine(pen2, new Point(0, -pictureBox1.Height), new Point(0, pictureBox1.Height));
                //g.DrawLine(pen2, new Point(pictureBox1.Width, (pictureBox1.Height / 2)), new Point(-pictureBox1.Width, (pictureBox1.Height / 2)));
                pointslist.Reverse();
                g.DrawLines(pen, pointslist.ToArray());

            }
        }
        private void CubicSpline()
        {
            int n = Convert.ToInt32(textBox2.Text);
            double[] x = Array.ConvertAll(textBox1.Text.Split(',').Select(e => e.Replace('.', ',')).ToArray(), Convert.ToDouble);
            double[] y = Array.ConvertAll(textBox3.Text.Split(',').Select(e => e.Replace('.', ',')).ToArray(), Convert.ToDouble);
            
            double[] sortedX = x;
            Array.Sort(sortedX);
            double maxX = sortedX[sortedX.Length - 1];
            double minX = sortedX[0];

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {

                //Lagrange
                Pen pen = new Pen(Color.BlueViolet, 3);
                Pen pen2 = new Pen(Color.Black, 1);
                Pen pen3 = new Pen(Color.Red, 2);
                Pen pen4 = new Pen(Color.Blue, 1);
                List<PointF> pointslist = new List<PointF>();

                //Calculate spline

                double[] a = new double[n + 2];
                for (int i = 0; i < n + 1; i++)
                {
                    //S k,0
                    a[i] = y[i];
                }

                double[] h = new double[n];
                double[] H = new double[n];
                for (int i = 0; i < n; i++)
                {
                    h[i] = x[i + 1] - x[i];
                   
                }
                double[] sortedh = h;
                Array.Sort(sortedh);
                double maxh = sortedh[sortedh.Length - 1];
                for (int i = 0; i < n; i++)
                {
                    H[i] = h[i];
                    x[i + 1] /= maxh;
                }
                for (int i = 0; i < n; i++)
                {
                    h[i]= x[i + 1] - x[i];
                }


                double[] b = new double[n];
                double[] d = new double[n];
                double[] L = new double[n];
                for (int i = 1; i < n; i++)
                {
                    L[i] = (3 / h[i]) * (a[i + 1] - a[i]) - (3 / h[i-1]) * (a[i] - a[i - 1]);
                }
                double[] m = new double[n + 2];
                double[] l = new double[n + 2];
                double[] u = new double[n + 2];
                double[] z = new double[n + 2];
                l[0] = 1.00;
                u[0] = 0.00;
                z[0] = 0.00;
                for (int i = 1; i < n; i++)
                {
                    l[i] = 2 * (x[i + 1] - x[i - 1]) - h[i-1] * u[i - 1];
                    u[i] = h[i] / l[i];
                    z[i] = (L[i] - h[i-1] * z[i - 1]) / l[i];
                }
                l[n] = 1.00;
                m[n] = 0.00;
                z[n] = 0.00;
                for (int j = n - 1; j > -1; j--)
                {
                    //S k,2
                    m[j] = (z[j] - u[j] * m[j + 1]);

                    //S k,1
                    b[j] = (a[j + 1] - a[j]) / h[j] - (h[j] * (m[j + 1] + 2 * m[j])) / 3;
                    
                    //S k,3
                    d[j] = (m[j + 1] - m[j]) / 3 * h[j];
                }
                Dictionary<int, (double, double, double, double, double)> splineSet
                    = new Dictionary<int, (double, double, double, double, double)>();
               
                for (int i = 0; i < n; i++)
                {
                 
                    splineSet.Add(i, (a[i], b[i], m[i], d[i], x[i]));

                }
                //double[] d = new double[n];
                //double h = x[1] - x[0];
                //double[] u = new double[n - 1];
                //for (int i = 1; i < d.Length; i++)
                //{
                //    d[i - 1] = (y[i] - y[i - 1]) / h;
                //}
                //for (int i = 1; i < u.Length; i++)
                //{
                //    u[i - 1] = 6 * (d[i] - d[i - 1]);
                //}

                //double m = 0;
                //double[] S0 = new double[n + 2];
                //double[] S1 = new double[n+1];
                //double[] S2 = new double[n+1];
                //double[] S3 = new double[n+1];
                //for (int i = 0; i < n + 1; i++)
                //{
                //    S0[i] = y[i];
                //}


                //Dictionary<int, (double, double, double, double, double)> splineSet
                //    = new Dictionary<int, (double, double, double, double, double)>();
                //for (int i = 0; i < n; i++)
                //{
                //   // splineSet.Add(i, (a[i], b[i], m[i], d[i], x[i]));

                //}

                //Draw curve
                for (float X = 0; X < splineSet.Count; X += 0.1f)
                {
                    double P = 0;

                    int i = (int)X;
                    double w = X - splineSet[i].Item5;
                    //P = splineSet[i].Item1
                    //    + splineSet[i].Item2 * w
                    //    + splineSet[i].Item3 * Math.Pow(w, 2)
                    //    + splineSet[i].Item4 * Math.Pow(w, 3);

                    P = ((splineSet[i].Item4 * w + splineSet[i].Item3) * w + splineSet[i].Item2) * w + y[i];

                    g.DrawRectangle(pen4, (float)(X * trackBar1.Value* maxh), (pictureBox1.Height / 2), 1, 1);

                    pointslist.Add(new PointF((float)(X * trackBar1.Value * maxh), -(float)P * trackBar2.Value + (pictureBox1.Height / 2)));


                }
               
                //Draw points
                listBox1.Items.Clear();
                for (int X = 0; X < n+1; X++)
                {
                    int i = X;

                    PointF p = new PointF((float)(x[i] * maxh) * trackBar1.Value, -(float)y[i] * trackBar2.Value + (pictureBox1.Height / 2));
                    
                    g.DrawString((x[i]*maxh).ToString(), new Font(FontFamily.GenericSerif, 8), Brushes.Black, new PointF(p.X, pictureBox1.Height - 300));
                    g.DrawString(y[i].ToString(), new Font(FontFamily.GenericSerif, 8), Brushes.Black, new PointF(0, p.Y));

                    listBox1.Items.Add($"X: {x[i] * maxh}, Y: {y[i]}");
                    g.FillEllipse(Brushes.Blue, p.X - Convert.ToInt32(15 / 2), p.Y - Convert.ToInt32(15 / 2), 15, 15);
                    

                }

                pointslist.Reverse();
                g.DrawLines(pen, pointslist.ToArray());

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                Lagrange();
            }
            if (checkBox2.Checked == true)
            {
                CubicSpline();
            }
            pictureBox1.Refresh();
            GC.Collect();
        }
    }
}
