using System;
using System.Collections.Generic;
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
            double result = 0;
            try
            {
                Math.Round(x, 4);
                string replacedExpression = expression.Replace("x", x.ToString().Replace(',', '.'));
                result = sc.Eval(replacedExpression);
            }
            catch(System.Runtime.InteropServices.COMException ex)
            {
                MessageBox.Show(ex.Message);
            }
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

            return result;
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
            bool r =false;
            if (CalculateF(fx) == 0)
            {
                r = true;
            }
                result = Math.Abs((2 / Math.Pow(dx, 2)) * (( CalculateF(fx + dx)
                + CalculateF(fx-dx)) / 2 - CalculateF(fx)));
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
            double y = fa + ((x - a) / (x - a)) * (fx - fa);
            list.Add(y);
            values.Add(counter, list);
            double xi = 0;

            xi = a - fa * (x - a) / (fx - fa);
           
            double fnext = CalculateF(xi);
            if (Math.Abs(fnext) < e)
            {
                list.Add(xi);
                list.Add(fnext);
                y = fa + ((x - a) / (x - a)) * (fx - fa);
                list.Add(y);
                values.Add(counter+1, list);
                stepText += $"Answer: x = {xi}, f(x) = {Math.Round(fnext,4)}\r\n";
                return;
            }
            Iteration2(counter, a, xi, e, values);

        }
    }
}
