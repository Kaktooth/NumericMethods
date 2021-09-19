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
    }
}
