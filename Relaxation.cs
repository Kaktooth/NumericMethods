using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Windows.Forms;

namespace NumberMethods
{
    public partial class Relaxation : Form
    {
        public Relaxation()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            double[,] a = new double[3, 3];
            double[,] aT = new double[3, 3];
            double[,] P = new double[3, 3];
            double[] b = new double[3];
            double[] c = new double[3];
            double[] x = new double[3];
            x[0] = Convert.ToDouble(textBox13.Text);
            x[1] = Convert.ToDouble(textBox13.Text);
            x[2] = Convert.ToDouble(textBox13.Text);
            double E = Convert.ToDouble(textBox14.Text);

            a[0, 0] = textBox1.Text == "" ? a[0, 0] = 1 : textBox1.Text == "-" ? a[0, 0] = -1 : a[0, 0] = Convert.ToDouble(textBox1.Text);
            a[0, 1] = textBox2.Text == "" ? a[0, 1] = 1 : textBox2.Text == "-" ? a[0, 1] = -1 : a[0, 1] = Convert.ToDouble(textBox2.Text);
            a[0, 2] = textBox3.Text == "" ? a[0, 2] = 1 : textBox3.Text == "-" ? a[0, 2] = -1 : a[0, 2] = Convert.ToDouble(textBox3.Text);
            b[0] = Convert.ToDouble(textBox4.Text);
            a[1, 0] = textBox5.Text == "" ? a[1, 0] = 1 : textBox5.Text == "-" ? a[1, 0] = -1 : a[1, 0] = Convert.ToDouble(textBox5.Text);
            a[1, 1] = textBox6.Text == "" ? a[1, 1] = 1 : textBox6.Text == "-" ? a[1, 1] = -1 : a[1, 1] = Convert.ToDouble(textBox6.Text);
            a[1, 2] = textBox7.Text == "" ? a[1, 2] = 1 : textBox7.Text == "-" ? a[1, 2] = -1 : a[1, 2] = Convert.ToDouble(textBox7.Text);
            b[1] = Convert.ToDouble(textBox8.Text);
            a[2, 0] = textBox9.Text == "" ? a[2, 0] = 1 : textBox9.Text == "-" ? a[2, 0] = -1 : a[2, 0] = Convert.ToDouble(textBox9.Text);
            a[2, 1] = textBox10.Text == "" ? a[2, 1] = 1 : textBox10.Text == "-" ? a[2, 1] = -1 : a[2, 1] = Convert.ToDouble(textBox10.Text);
            a[2, 2] = textBox11.Text == "" ? a[2, 2] = 1 : textBox11.Text == "-" ? a[2, 2] = -1 : a[2, 2] = Convert.ToDouble(textBox11.Text);
            b[2] = Convert.ToDouble(textBox12.Text);


            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    aT[i, j] = a[j, i];
                }

            }

           a = Multiplication(aT, a);
            for (int i = 0; i < 3; i++)
            {
               b[i] = aT[i, 0] * b[0] + aT[i, 1] * b[1] + aT[i, 2] * b[2];
             }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    P[i, j] = -(a[i, j] / a[i, i]);
                }
                c[i] = b[i] / a[i, i];
            }

            Dictionary<int, List<(double, double, double, double, double, double)>> values = new Dictionary<int, List<(double, double, double, double, double, double)>>();
            var list = new List<(double, double, double, double, double, double)>();

            list.Add((0, 0, 0, x[0], x[1], x[2]));
            values.Add(0, list);

            var thread = new Thread(
            () =>
            {
                Equation.RelaxIteration(0, 0, P, c, x, E, values, new double[3], 0);
            });
            thread.Start();
            thread.Join();
            RelaxData data = new RelaxData();
            for (int j = 1; j < values.Count; j++)
            {

                data.row = data.dt.NewRow();
                data.row["i"] = j;
                data.row["delta 1"] = values[j][0].Item2;
                data.row["delta 2"] = values[j][0].Item4;
                data.row["delta 3"] = values[j][0].Item6;
                data.row["x1"] = values[j][0].Item1;
                data.row["x2"] = values[j][0].Item3;
                data.row["x3"] = values[j][0].Item5;
                data.dt.Rows.Add(data.row);
            }
            listBox1.Items.Add(data.dt);
            listBox1.Items.Add(Equation.stepText);
            Equation.ClearSteps();
        }
        static double[,] Multiplication(double[,] a, double[,] b)
        {

            double[,] r = new double[a.GetLength(0), b.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    for (int k = 0; k < b.GetLength(0); k++)
                    {
                        r[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            return r;
        }
        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is DataTable)
            {
                dataGridView1.DataSource = listBox1.SelectedItem;
            }
        }
    }
}
