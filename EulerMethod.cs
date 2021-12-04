using System;
using System.Collections.Generic;

namespace NumberMethods
{
    public class EulerMethod
    {
        public static void CalculateEuler(double x, double y, double h, double a, double b, List<(double, double, double)> values)
        {
            double yi2 = y + (h / 2) * Equation.CalculateFunction(x, y);
            double yi = y + h * Equation.CalculateFunction(x + (h / 2), yi2);
            values.Add((x, yi2, yi));

            x = Math.Round(x + h, 1);
            //double error = Math.Abs((yi - yi2) / (Math.Pow(2, 1) - 1));
            if (x > b)
            {
                return;
            }
            else
            {
                CalculateEuler(x, yi, h, a, b, values);
            }
        }

        public static void CalculateEuler(double x, double y, double h, double a, double b, Dictionary<double, (double, double)> values)
        {
            double yi2 = y + (h / 2) * Equation.CalculateFunction(x, y);
            double yi = y + h * Equation.CalculateFunction(x + (h / 2), yi2);
            values.Add(x, (yi2, yi));
            x = Math.Round(x + h, 1);
            if (x > b)
            {
                return;
            }
            else
            {
                CalculateEuler(x, yi, h, a, b, values);
            }
        }
    }
}
