using System;
using System.Windows.Forms;

namespace NumberMethods
{
    public partial class Graphic : Form
    {

        public Graphic()
        {
            InitializeComponent();


        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {


            pictureBox1 = Equation.DrawPlot(pictureBox1, pictureBox1.Width, pictureBox1.Height, (float)Convert.ToDouble(textBox1.Text), (float)Convert.ToDouble(textBox2.Text), trackBar1.Value, trackBar2.Value);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {

        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            pictureBox1 = Equation.DrawPlot(pictureBox1, pictureBox1.Width, pictureBox1.Height, (float)Convert.ToDouble(textBox1.Text), (float)Convert.ToDouble(textBox2.Text), trackBar1.Value, trackBar2.Value);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                pictureBox1 = Equation.DrawPlot(pictureBox1, pictureBox1.Width, pictureBox1.Height, (float)Convert.ToDouble(textBox1.Text), (float)Convert.ToDouble(textBox2.Text), trackBar1.Value, trackBar2.Value);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                pictureBox1 = Equation.DrawPlot(pictureBox1, pictureBox1.Width, pictureBox1.Height, (float)Convert.ToDouble(textBox1.Text), (float)Convert.ToDouble(textBox2.Text), trackBar1.Value, trackBar2.Value);
            }
        }
    }
}
