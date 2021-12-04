using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NumberMethods
{
    public class Heming
    {
        public static double CalculateStep(double x, double y)
        {
            double f = Equation.CalculateFunction(x, y);
            double d = Math.Abs(Equation.CalculateDerivative(f));
            double h = 0.69f / d;
            return h;

        }
        public static List<(double, double)> CalculateHeming(double minError, double stepValue, double x, double y, double h, double n, double a, double b, bool withControlElement)
        {
            List<(double, double)> values = new List<(double, double)>();
            List<double> functions = new List<double>();
            int counter = 0;
            double lastP = 0;
            double p = 0;
            double m = 0;
            double yi = y;
            double f = 0;
            for (int i = 0; i < 3; i++)
            {
                x = Math.Round(x + h, 3);
                double yi2 = yi + (h / 2) * Equation.CalculateFunction(x, yi);
                yi = yi + h * Equation.CalculateFunction(x + (h / 2), yi2);
                f = Equation.CalculateFunction(x, yi);

                counter++;
                functions.Add(f);
                values.Add((x, yi));

            }
            while (x < n)
            {
                try
                {

                    x = Math.Round(x + h, 3);
                    double fk2 = functions[counter - 3];
                    double fk1 = functions[counter - 2];
                    double fk = functions[counter - 1];

                    p = values[counter - 3].Item2 + ((4 * h) / 3) * (2 * fk2 - fk1 + 2 * fk);

                    if (lastP != 0 && withControlElement)
                    {
                        m = p + 9 * (yi - lastP) / 121;

                    }
                    else
                    {
                        m = p;
                    }

                    f = Equation.CalculateFunction(x, m);
                    //if (withControlElement)
                    //{
                    //    f = Equation.CalculateFunction(x, m);
                    //}
                    //else
                    //{
                    //    f = Equation.CalculateFunction(x, p);
                    //}

                    // yi = ((-values[counter - 2].Item2 + 9 * yi) / 8)
                    //     + ((3 * h) / 8) * (-fk1 + (2 * fk) + f);

                    yi = ((-values[counter - 2].Item2+ 9 * yi ) / 8)
                         + ((3 * h) / 8) * (f - fk1 + (2 * fk));
                    if (minError != 0 || stepValue != 0)
                    {
                        double error = 0.2 * Math.Abs(values[counter - 1].Item2 - yi);
                        //double error = 0.31 * Math.Pow(h, 5) * values[counter - 1].Item2;
                        if (error > minError)
                        {
                            h -= stepValue;
                        }
                    }
                    lastP = p;


                    counter++;
                    functions.Add(f);
                    values.Add((x, yi));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            return values;
        }
    }
}
