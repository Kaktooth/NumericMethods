using System;
using System.Windows.Forms;

namespace NumberMethods
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChordMethod newform = new ChordMethod();
            newform.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Relaxation relaxationForm = new Relaxation();
            relaxationForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NewtonMethod newtonMethod = new NewtonMethod();
            newtonMethod.Show();
        }
    }
}
