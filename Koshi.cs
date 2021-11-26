using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NumberMethods
{
    public partial class Koshi : Form
    {
        public Koshi()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            string expression = textBox1.Text;
            string accurateExpression = textBox2.Text;
            double a = Convert.ToDouble(textBox3.Text.Split(',')[0]);
            double b = Convert.ToDouble(textBox3.Text.Split(',')[1]);
            double h = Convert.ToDouble(textBox4.Text);
            double x0 = Convert.ToDouble(textBox5.Text);
            double y0 = Convert.ToDouble(textBox6.Text);
            Equation equation2 = new Equation(accurateExpression);
            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                Pen pen4 = new Pen(Color.Blue, 1);
                List<PointF> pointslist2 = new List<PointF>();
                for (float i = (float)a; i < b; i += 0.1f)
                {
                    if (i != 0)
                    {
                        double y = Equation.CalculateF(i);

                        pointslist2.Add(new PointF(i * trackBar1.Value,
                            -(float)y * trackBar2.Value + (pictureBox1.Height / 2)));
                    }
                }

                pointslist2.Reverse();
                g.DrawLines(new Pen(Color.Blue, 3), pointslist2.ToArray());
                for (float i = (float)a; i < b; i += 0.1f)
                {
                    if (i != 0)
                    {
                        double y = Equation.CalculateF(i);
                        g.FillRectangle(Brushes.White, (i * trackBar1.Value) - Convert.ToInt32(4 / 2),
                            -(float)y * trackBar2.Value + (pictureBox1.Height / 2) - Convert.ToInt32(4 / 2), 4, 4);
                    }
                }
            }
            Equation equation = new Equation(expression);
            List<(double, double, double)> values = new List<(double, double, double)>();
            EulerMethod.CalculateEuler(x0, y0, h, a, b, values);

            DrawPlot(values, new Pen(Color.Black, 3), Brushes.Blue);



            pictureBox1.Refresh();
        }

        public void DrawPlot(List<(double, double, double)> values, Pen graphicPen, Brush dotsBrush)
        {

            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                Pen pen4 = new Pen(Color.Blue, 1);
                List<PointF> pointslist = new List<PointF>();
                List<PointF> pointslist2 = new List<PointF>();
                for (int i = 0; i < trackBar1.Value*5; i++)
                {
                    if (i % 3 == 0)
                    {
                        g.DrawRectangle(pen4, (int) i, (pictureBox1.Height / 2), 1, 1);
                    }
                }

                for (int i = 0; i < values.Count; i++)
                {
                    double x = values[i].Item1;
                    double yi2 = values[i].Item2;
                    double y = values[i].Item3;
                    
                    //pointslist.Add(new PointF((float)x/2 * trackBar1.Value, -(float)yi2 * trackBar2.Value + (pictureBox1.Height / 2)));
                    pointslist.Add(new PointF((float)x * trackBar1.Value,
                        -(float)y * trackBar2.Value + (pictureBox1.Height / 2)));
                    pointslist2.Add(new PointF((float)x * trackBar1.Value,
                        -(float)yi2 * trackBar2.Value + (pictureBox1.Height / 2)));

                }
                pointslist.Reverse();
                g.DrawLines(graphicPen, pointslist.ToArray());
                pointslist2.Reverse();
                g.DrawLines(new Pen(Color.Green,3), pointslist2.ToArray());
                for (int i = 0; i < values.Count; i++)
                {
                    double x = values[i].Item1;
                    double yi2 = values[i].Item2;
                    double y = values[i].Item3;
                    if (x % 1 == 0)
                    {
                        PointF p = new PointF((float)x * trackBar1.Value, -(float)y * trackBar2.Value + (pictureBox1.Height / 2));
                        g.DrawString(x.ToString(), new Font(FontFamily.GenericSerif, 8), Brushes.Black,
                            new PointF(p.X, (pictureBox1.Height / 2) + 10));


                        g.DrawString(Math.Round(y).ToString(), new Font(FontFamily.GenericSerif, 8), Brushes.Black,
                            new PointF(0, p.Y));

                        // Draw dots
                        //g.FillEllipse(dotsBrush, p.X - Convert.ToInt32(10 / 2), p.Y - Convert.ToInt32(10 / 2), 9, 9);
                    }
                }

                pictureBox1.Refresh();
            }
        }
    }
}
