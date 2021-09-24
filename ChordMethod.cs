using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace NumberMethods
{
    public partial class ChordMethod : Form
    {
        public ChordMethod()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string expression = textBox1.Text;
            double a = Convert.ToDouble(textBox2.Text.Split(',')[0]);
            double b = Convert.ToDouble(textBox2.Text.Split(',')[1]);
            double E = Convert.ToDouble(textBox3.Text);
            double n = Convert.ToInt32(textBox4.Text);

            Equation equation = new Equation(expression);

            System.Data.DataSet dataSet;
            DataTable dt = new DataTable($"Таблиця значень на інтервалі [{a},{b}]");
            DataColumn column;
            DataRow row;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "x";
            column.ReadOnly = false;
            column.Unique = false;

            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "f(x)";
            column.AutoIncrement = false;
            column.Caption = "f(x)";
            column.ReadOnly = false;
            column.Unique = false;

            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "f''(x)";
            column.AutoIncrement = false;
            column.Caption = "f''(x)";
            column.ReadOnly = false;
            column.Unique = false;

            dt.Columns.Add(column);

            //DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            //PrimaryKeyColumns[0] = dt.Columns["x"];
            //dt.PrimaryKey = PrimaryKeyColumns;

            dataSet = new DataSet();
            dataSet.Tables.Add(dt);

            var interval = Math.Round(Math.Abs((a - b) / n), 1);
            MessageBox.Show(interval.ToString());
            for (double i = a; i < b + 0.01; i += Math.Round(interval,1))
            {
            
                var fx = Equation.CalculateF(i);
                var der = Equation.CalculateSecondDerivative(i, Math.Sign(fx));
                          //Equation.CalculateDerivative(Equation.CalculateDerivative(i));
                row = dt.NewRow();
                row["x"] = Math.Round(i, 1);
                row["f(x)"] = fx;
                row["f''(x)"] = der;
                dt.Rows.Add(row);
            }
            listBox1.Items.Add(dt);
            dataGridView1.DataSource = dt;

            PictureBox p = new PictureBox();
            p = Equation.DrawPlot(p, pictureBox1.Width, pictureBox1.Height, (float)a, (float)b,50,90);
            listBox1.Items.Add(p);
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                //f(a)*f(b)<0
                if ((double)dt.Rows[i - 1].ItemArray[1] * (double)dt.Rows[i].ItemArray[1] < 0)
                {
                    listBox1.Items.Add($"[{(double)dt.Rows[i - 1].ItemArray[0]},{(double)dt.Rows[i].ItemArray[0]}] have root");
                    //double x0 = 0;
                    //double x0f = 0;
                    //if f(a)*f''(a)>0 { x0 = b } else(f(b)*f''(b)>0) { x0 = a}

                    //if ((double)dt.Rows[i - 1].ItemArray[1] * (double)dt.Rows[i - 1].ItemArray[2] > 0)
                    //{
                    //    x0 = (double)dt.Rows[i].ItemArray[0];
                    //    x0f = (double)dt.Rows[i].ItemArray[1];
                    //    listBox1.Items.Add($"f(a)*f''(a) > 0, so x0 = {x0}( {x0f} )");
                    //}
                    //else if ((double)dt.Rows[i].ItemArray[1] * (double)dt.Rows[i].ItemArray[2] > 0)
                    //{
                    //    x0 = (double)dt.Rows[i-1].ItemArray[0];
                    //    x0f = (double)dt.Rows[i-1].ItemArray[1];
                    //    listBox1.Items.Add($"f(b) * f''(b) > 0, so x0 = {x0}( {x0f} )");
                    //}
                    Dictionary<int, List<double>> values = new Dictionary<int, List<double>>();
                    IterationData iterationData = new IterationData();
                    //var list = new List<double>();
                    //double fa = (double)dt.Rows[i - 1].ItemArray[1];
                    //double fb = (double)dt.Rows[i].ItemArray[1];

                    //double y = fa + ((x0 - a) / (b - a)) * (fb - fa);
                    //double x1 = a - ((fa * (x0 - a) / (x0f - fa)));
                    //double x1 = a - (fa / (fb - fa)) * (b - a);
                    //list.Add(x0);
                    //list.Add(x0f);
                    //list.Add(y);

                    //values.Add(counter, list);

                    int counter = 0;
                    var thread = new Thread(
                    () => {
                      Equation.Iteration2(counter, (double)dt.Rows[i-1].ItemArray[0], (double)dt.Rows[i].ItemArray[0], E, values);
                    });
                    thread.Start();
                    thread.Join();
                   
                    for (int j = 1; j < values.Count; j++)
                    {

                        iterationData.row = iterationData.dt.NewRow();
                        iterationData.row["n"] = j;
                        iterationData.row["x"] = values[j][0];
                        iterationData.row["f(x)"] = values[j][1];
                        //iterationData.row["y"] = values[j][2];
                        iterationData.dt.Rows.Add(iterationData.row);
                        
                    }
                    listBox1.Items.Add(iterationData.dt);
                    
                    listBox1.Items.Add(Equation.stepText);
                    Equation.ClearSteps();
                    GC.Collect();


                }

            }


        }
        
        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is DataTable)
            {
                dataGridView2.DataSource = listBox1.SelectedItem;
            }
            else if (listBox1.SelectedItem is PictureBox)
            {
                pictureBox1.Image = ((PictureBox)listBox1.SelectedItem).Image;
                pictureBox1.Refresh();
            }
            else if (listBox1.SelectedItem is string)
            {
                richTextBox1.Text = (string)listBox1.SelectedItem;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            GC.Collect();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void PlotButton_Click(object sender, EventArgs e)
        {
            
            Graphic g = new Graphic();
            g.textBox1.Text = ((float)Convert.ToDouble(textBox2.Text.Split(',')[0])).ToString();
            g.textBox2.Text = ((float)Convert.ToDouble(textBox2.Text.Split(',')[1])).ToString();
            g.ShowDialog();
        }

        private void TableButton_Click(object sender, EventArgs e)
        {
            Data d = new Data();
            foreach (var item in listBox1.Items)
            {
                if (!(item is PictureBox))
                {
                    d.listBox1.Items.Add(item);
                }
            }
            d.ShowDialog();

        }
    }
}
