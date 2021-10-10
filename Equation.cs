using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NumberMethods
{
    public class Equation
    {
        public Equation(string expression)
        {
            Equation.expression = expression;
            sc = new MSScriptControl.ScriptControl();
            sc.Language = "VBScript";

        }
        static string expression { get; set; }
        static MSScriptControl.ScriptControl sc;
        public static string stepText;

        public static double CalculateF(double x)
        {
            //    if (x > 2147483647 && x < -2147483647)
            //    {
            //        MessageBox.Show("Argument Exception");
            //        throw new ArgumentException();
            //    }
            //if (x.ToString().Length >= 15)
            //{
            //    x = Math.Round(x, 4);
            //}

            //try
            //{

            Math.Round(x, 4);
            var replacedExpression = expression.Replace("x", x.ToString().Replace(',', '.'));
            return sc.Eval(replacedExpression);

            //}
            //catch(System.Runtime.InteropServices.COMException ex)
            //{
            //MessageBox.Show(ex.Message);
            //}
            //return 0;
            //try
            //{

            //}
            //catch (Exception ex)
            //{
            //double sin = 0;
            //double num = 0;

            //for (int i = 0; i < expression.Length; i++)
            //{
            //    bool func = false;

            //    if(expression[i] == 's')
            //    {
            //        func = true;
            //    }
            //    if(expression[i] == '^'&&func == true)
            //    {
            //         num = Convert.ToDouble(expression.Split('^')[1].Split(')')[0]);
            //    }

            //}
            //if (num != 0)
            //{
            //     sin = Math.Sin(Math.Pow(num, 3));
            //}
            //else
            //{
            //    sin = Math.Sin(num);
            //}
            //string s = expression.Split('s')[1].Split(')')[0];
            //replacedExpression = expression.Replace(s, sin.ToString().Replace(',', '.'));
            //result = sc.Eval(replacedExpression);
            //    result = 0;

            //}


        }
        public static double CalculateDerivative(double fx)
        {
            double result;

            double dx = 10e-10;
            result = ((CalculateF(fx) + dx) - CalculateF(fx)) / dx;

            return result;

        }
        public static double CalculateSecondDerivative(double fx, int sign)
        {
            double result;

            double dx = Math.Pow(10, -5);
            bool r = false;
            if (CalculateF(fx) == 0)
            {
                r = true;
            }
            result = Math.Abs((2 / Math.Pow(dx, 2)) * ((CalculateF(fx + dx)
            + CalculateF(fx - dx)) / 2 - CalculateF(fx)));
            if (sign == -1)
            {
                return -result;
            }
            else
            {
                return result;
            }

        }
        public static void ClearSteps()
        {
            stepText = "";
        }
        public static void Iteration(int counter, double x, double a, double b, double e, Dictionary<int, List<double>> values)
        {
            //try
            //{
            counter++;
            double new_x = 0;
            double fx = CalculateF(x);
            double fa = CalculateF(a);
            double fb = CalculateF(b);
            var list = new List<double>();
            double y = fa + ((x - a) / (b - a)) * (fb - fa);
            list.Add(x);
            list.Add(fx);
            list.Add(y);


            if (fx * fb < 0)
            {
                stepText += $"fx * fb < 0 for {x}\r\n";
                new_x = x - ((fx * (b - x) / (fb - fx)));
                //new_x = x - fx * ((b - x) / (fb - fx));
                values.Add(counter, list);
                if (Math.Abs(CalculateF(new_x)) < e)
                {
                    fx = CalculateF(new_x);
                    fa = CalculateF(a);
                    fb = CalculateF(b);
                    y = fa + ((new_x - a) / (b - a)) * (fb - fa);
                    list.Add(new_x);
                    list.Add(fx);
                    list.Add(y);
                    values.Add(counter + 1, list);
                    stepText += $"Answer: x = {new_x}, f(x) = {fx}\r\n";
                    return;
                }
                Iteration(counter, new_x, a, b, e, values);
            }
            else
            {
                stepText += $"fx * fa < 0 for {x}\r\n";
                new_x = a - ((fa * (x - a) / (fx - fa)));
                //new_x = a - fa * ((x - a) / (fx - fa));
                values.Add(counter, list);
                if (Math.Abs(CalculateF(new_x)) < e)
                {
                    fx = CalculateF(new_x);
                    fa = CalculateF(a);
                    fb = CalculateF(b);
                    y = fa + ((new_x - a) / (b - a)) * (fb - fa);
                    list.Add(new_x);
                    list.Add(fx);
                    list.Add(y);
                    values.Add(counter + 1, list);
                    stepText += $"Answer: x = {new_x}, f(x) = {fx}\r\n";
                    return;
                }
                Iteration(counter, new_x, a, b, e, values);
            }
        }
        public static PictureBox DrawPlot(PictureBox p, int width, int height, float a, float b, int scaleX, int scaleY)
        {

            p.Image = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(p.Image))
            {


                Pen pen = new Pen(Color.Black, 3);
                Pen pen2 = new Pen(Color.Black, 1);
                List<PointF> pointslist = new List<PointF>();


                for (float j = (float)a; j < (float)b; j += 0.005f)
                {
                    pointslist.Add(new PointF(j * scaleX + (width / 2), -(float)Equation.CalculateF(j) * scaleY + (height / 2)));
                }
                //(float)values[j + 1][1] * 30 +
                g.DrawLine(pen2, new Point(0, -height), new Point(0, height));
                g.DrawLine(pen2, new Point(width, (height / 2)), new Point(-width, (height / 2)));
                pointslist.Reverse();
                g.DrawLines(pen, pointslist.ToArray());
                p.Refresh();
            }
            return p;
        }
        //catch (Exception ex)
        //{
        //    MessageBox.Show(ex.StackTrace);
        //    return null;
        //}
        public static void Iteration2(int counter, double a, double x, double e, Dictionary<int, List<double>> values)
        {

            counter++;
            double fa = CalculateF(a);
            double fx = CalculateF(x);
            var list = new List<double>();
            list.Add(x);
            list.Add(fx);
            //double y = fa + ((x - a) / (x - a)) * (fx - fa);
            //list.Add(y);
            values.Add(counter, list);
            double xi = 0;
            xi = a - fa * (x - a) / (fx - fa);
            double fnext = CalculateF(xi);
            if (Math.Abs(fnext) < e)
            {

                list.Add(xi);
                list.Add(fnext);
                //y = fa + ((x - a) / (x - a)) * (fx - fa);
                //list.Add(y);
                values.Add(counter + 1, list);
                stepText += $"Answer: x = {xi}, f(x) = {Math.Round(fnext, 4)}\r\n";
                return;
            }
            Iteration2(counter, a, xi, e, values);

        }
        public static void RelaxIteration(int xcounter, int counter, double[,] a, double[] b, double[] x, double e, Dictionary<int, List<(double, double, double, double, double, double)>> values, double[] prevdelta, int previndex,List<double> x1Sum, List<double> x2Sum, List<double> x3Sum)
        {
            counter++;

            var list = new List<(double, double, double, double, double, double)>();
            var delta = new double[3];

            if (counter == 1)
            {
                delta[0] = (a[0, 0] * x[0]) + (a[0, 1] * x[1]) + (a[0, 2] * x[2]) - b[0];
                delta[1] = (a[1, 0] * x[0]) + (a[1, 1] * x[1]) + (a[1, 2] * x[2]) - b[1];
                delta[2] = (a[2, 0] * x[0]) + (a[2, 1] * x[1]) + (a[2, 2] * x[2]) - b[2];
            }
            else
            {
                delta[0] = prevdelta[0] + (a[0, previndex] * x[previndex]);
                delta[1] = prevdelta[1] + (a[1, previndex] * x[previndex]);
                delta[2] = prevdelta[2] + (a[2, previndex] * x[previndex]);
            }
           
            var deltaAbs = new double[3];
            deltaAbs[0] = Math.Abs(delta[0]);
            deltaAbs[1] = Math.Abs(delta[1]);
            deltaAbs[2] = Math.Abs(delta[2]);
            var sorted = deltaAbs;
            Array.Sort(sorted);
            int index = 0;
            for (int i = 0; i < deltaAbs.Length; i++)
            {
                if (sorted[2] == Math.Abs(delta[i]))
                {
                    index = i;
                }
            }
            double maxDelta = delta[index];
            if (index == 0)
            {
                x1Sum.Add(Math.Abs(maxDelta));
            }
            else if(index == 1)
            {
                x2Sum.Add(Math.Abs(maxDelta));
            }
            else if(index == 2)
            {
                x3Sum.Add(Math.Abs(maxDelta));
            }
            x[index] = maxDelta;
            list.Add((x[0], delta[0], x[1], delta[1], x[2], delta[2]));
            values.Add(counter, list);
            if (Math.Abs(maxDelta) <= e)
            {
                stepText += $"Answer: Max Delta = {Math.Round(maxDelta, 4)}, x = [ {Math.Round(x[0], 4)}, {Math.Round(x[1], 4)}, {Math.Round(x[2], 4)}]\r\n";
                return;
            }
            else
            {
                RelaxIteration(xcounter, counter, a, b, x, e, values, delta, index,x1Sum,x2Sum,x3Sum);
            }

        }
    }
}
