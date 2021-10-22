﻿using System;
using System.Windows.Forms;

namespace NumberMethods
{
    public partial class NewtonMethod : Form
    {
        public NewtonMethod()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string f1 = "sin(x)-y^2";
            string f2 = "tan(x)^2-y";
            NewtonEquation nq = new NewtonEquation(f1, f2);
            double x1 = 0.5f;
            double x2 = 0.022f;
            float E = 0.000001f;
            NewtonEquation.ShowResult(x1, x2, E);
            foreach (var Text in NewtonEquation.stepText)
            {
                listBox1.Items.Add(Text);
            }

        }
    }
}