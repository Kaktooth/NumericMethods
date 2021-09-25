using System;
using System.Data;
using System.Windows.Forms;

namespace NumberMethods
{
    public partial class Data : Form
    {
        public Data()
        {
            InitializeComponent();
            dataGridView1.DataSource = listBox1;
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
