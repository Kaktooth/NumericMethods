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
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
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
            if (pictureBox1.Image == null)
            {
                pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            }
            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                //Lagrange
                Pen pen = new Pen(Color.Black, 3);
                Pen pen2 = new Pen(Color.Black, 1);
                Pen pen3 = new Pen(Color.Red, 2);
                Pen pen4 = new Pen(Color.Blue, 1);
                List<PointF> pointslist = new List<PointF>();
                List<PointF> pointslist2 = new List<PointF>();

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
                        for (var m = 0; m < n + 1; m++)
                        {
                            if (m == j)
                            {
                                continue;
                            }
                            dividend *= X - x[m];
                            divisor *= (x[j] - x[m]);
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
                for (int X = (int)minX; X < maxX + 1; X++)
                {
                    double L = 0;
                    double l = 0;
                    double dividend = 1;
                    double divisor = 1;
                    int j = 0;

                    do
                    {
                        for (var m = 0; m < n + 1; m++)
                        {
                            if (m == j)
                            {
                                continue;
                            }
                            dividend *= X - x[m];
                            divisor *= (x[j] - x[m]);
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

                    PointF p = new PointF(X * trackBar1.Value, -(float)L * trackBar2.Value + (pictureBox1.Height / 2));
                    if (!checkBox2.Checked)
                    {
                        g.DrawString(X.ToString(), new Font(FontFamily.GenericSerif, 8), Brushes.Black, new PointF(p.X, pictureBox1.Height - 30));
                        g.DrawString(L.ToString(), new Font(FontFamily.GenericSerif, 8), Brushes.Black, new PointF(0, p.Y));
                    }
                    if (x.Contains(MathF.Round(X, 0)))
                    {
                        listBox1.Items.Add($"X: {X}, Y: {L}");
                        g.DrawRectangle(pen3, p.X, p.Y, 5, 5);
                    }
                }
                
                if (checkBox4.Checked)
                {
                    double vL = 0;
                    for (int X = (int)minX; X < maxX + 1; X++)
                    {
                        double L = 0;
                        double l = 0;
                        double dividend = 1;
                        double divisor = 1;
                        int j = 0;

                        do
                        {
                            for (var m = 0; m < n + 1; m++)
                            {
                                if (m == j)
                                {
                                    continue;
                                }
                                dividend *= X - x[m];
                                divisor *= (x[j] - x[m]);
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

                        PointF p = new PointF(X * trackBar1.Value, -(float)(L-vL) * trackBar2.Value + (pictureBox1.Height / 2));
                        pointslist2.Add(new PointF(p.X, p.Y));
                        if (x.Contains(MathF.Round(X, 0)))
                        {
                            vL = L;
                        }
                    }
                }
                pointslist.Reverse();
                g.DrawLines(pen, pointslist.ToArray());
                pointslist2.Reverse();
                g.DrawLines(pen3, pointslist2.ToArray());

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

            if (pictureBox1.Image == null)
            {
                pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            }
            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {

                Pen pen = new Pen(Color.BlueViolet, 3);
                Pen pen2 = new Pen(Color.Black, 1);
                Pen pen3 = new Pen(Color.Red, 2);
                Pen pen4 = new Pen(Color.Blue, 1);
                List<PointF> pointslist = new List<PointF>();

                //Calculate spline
                double[] Sk0 = new double[n + 2];
                for (int i = 0; i < n + 1; i++)
                {
                    //S k,0
                    Sk0[i] = y[i];
                }

                double[] h = new double[n];
                for (int i = 0; i < n; i++)
                {
                    h[i] = x[i + 1] - x[i];

                }
                double[] sortedh = h;
                Array.Sort(sortedh);
                double maxh = sortedh[sortedh.Length - 1];
                for (int i = 0; i < n; i++)
                {
                    x[i + 1] /= maxh;
                }
                for (int i = 0; i < n; i++)
                {
                    h[i] = x[i + 1] - x[i];
                }

                double[] Sk1 = new double[n];
                double[] Sk2 = new double[n];
                double[] Sk3 = new double[n];
                double[] d = new double[n];
                double[] u = new double[n];
                for (int j = n - 1; j > -1; j--)
                {
                    d[j] = (y[j + 1] - y[j]) / h[j];
                }

                for (int i = 1; i < n; i++)
                {
                    u[i] = 6 * (d[i] - d[i - 1]);
                }
                double[] m = new double[n + 2];
                double[] L = new double[n + 2];
                double[] U = new double[n + 2];
                double[] Z = new double[n + 2];
                L[0] = 1.00;
                U[0] = 0.00;
                Z[0] = 0.00;
                for (int i = 1; i < n; i++)
                {
                    L[i] = 2 * (x[i + 1] - x[i - 1]) - h[i - 1] * U[i - 1];
                    U[i] = h[i] / L[i];
                    Z[i] = (u[i] - h[i - 1] * Z[i - 1]) / L[i];
                }
                L[n + 1] = 1.00;
                m[n + 1] = 0.00;
                Z[n + 1] = 0.00;
                for (int j = n - 1; j > -1; j--)
                {

                    m[j] = (Z[j] - U[j] * m[j + 1]);

                    //S k,1
                    Sk1[j] = d[j] - (h[j] * (2 * m[j] + m[j + 1])) / 6;

                    //S k,2
                    Sk2[j] = m[j] / 2;

                    //S k,3
                    Sk3[j] = (m[j + 1] - m[j]) / 6 * h[j];
                }
                Dictionary<int, (double, double, double, double, double)> splineSet
                    = new Dictionary<int, (double, double, double, double, double)>();

                for (int i = 0; i < n; i++)
                {
                    splineSet.Add(i, (Sk0[i], Sk1[i], Sk2[i], Sk3[i], x[i]));

                }

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

                    g.DrawRectangle(pen4, (float)(X * trackBar1.Value * maxh), (pictureBox1.Height / 2), 1, 1);
                  
                    pointslist.Add(new PointF((float)(X * trackBar1.Value * maxh), -(float)P * trackBar2.Value + (pictureBox1.Height / 2)));

                    

                }
                pointslist.Reverse();
                g.DrawLines(pen, pointslist.ToArray());

                for (float X = 0; X < splineSet.Count; X += 0.1f)
                {
                    double P = 0;
                    int i = (int)X;
                    double w = X - splineSet[i].Item5;
                    P = ((splineSet[i].Item4 * w + splineSet[i].Item3) * w + splineSet[i].Item2) * w + y[i];
                    if (checkBox3.Checked == true)
                    {
                        g.FillRectangle(Brushes.White, (float)(X * trackBar1.Value * maxh) - Convert.ToInt32(3 / 2), -(float)P * trackBar2.Value + (pictureBox1.Height / 2) - Convert.ToInt32(3 / 2), 3, 3);
                    }
                }

                //Draw points
                listBox1.Items.Clear();
                for (int X = 0; X < n + 1; X++)
                {
                    int i = X;

                    PointF p = new PointF((float)(x[i] * maxh) * trackBar1.Value, -(float)y[i] * trackBar2.Value + (pictureBox1.Height / 2));

                    g.DrawString((x[i] * maxh).ToString(), new Font(FontFamily.GenericSerif, 8), Brushes.Black, new PointF(p.X, pictureBox1.Height - 300));
                    g.DrawString(y[i].ToString(), new Font(FontFamily.GenericSerif, 8), Brushes.Black, new PointF(0, p.Y));

                    listBox1.Items.Add($"X: {x[i] * maxh}, Y: {y[i]}");
                    g.FillEllipse(Brushes.Blue, p.X - Convert.ToInt32(15 / 2), p.Y - Convert.ToInt32(15 / 2), 15, 15);
                }

              
                
               
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
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
