using System;
using System.Collections.Generic;

namespace NumberMethods
{

    public class NewtonEquation
    {
        static string expression1 { get; set; }
        static string expression2 { get; set; }
        private static MSScriptControl.ScriptControl sc;
        public static List<string> stepText = new List<string>();

        public NewtonEquation(string expression1, string expression2)
        {
            NewtonEquation.expression1 = expression1;
            NewtonEquation.expression2 = expression2;
            sc = new MSScriptControl.ScriptControl();
            sc.Language = "VBScript";
        }

        public static double CalculateFirstFunc(double x1, double x2)
        {
            string replacedExpression = expression1
                .Replace("x", x1.ToString().Replace(',', '.'))
                .Replace("y", x2.ToString().Replace(',', '.'));
            double result = sc.Eval(replacedExpression);
            return result;
        }
        public static double CalculateSecondFunc(double x1, double x2)
        {
            string replacedExpression = expression2
                .Replace("x", x1.ToString().Replace(',', '.'))
                .Replace("y", x2.ToString().Replace(',', '.'));
            double result = sc.Eval(replacedExpression);
            return result;
        }

        static double[] Multiplication(double[,] J, double[] F)
        {
            double[] P = new double[2];
            double[,] aT = new double[2, 2];

            double[,] revA = new double[2, 2];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    aT[i, j] = J[j, i];
                }

            }
            double matrixDet = (J[0, 0] * J[1, 1]) - (J[0, 1] * J[1, 0]);
            double s = 1 / matrixDet;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (J[i, j] < 0 || aT[i, j] < 0)
                    {
                        revA[i, j] = -J[i, j] * s;
                    }
                    else
                    {
                        revA[i, j] = J[i, j] * s;
                    }
                }

            }

            for (int i = 0; i < 2; i++)
            {
                P[i] = -(revA[i, 0] * F[0] + revA[i, 1] * F[1]);
            }
            double[] Pcopy = new double[2];
            Pcopy[0] = P[0];
            Pcopy[1] = P[1];
            P[0] = Pcopy[1];
            P[1] = Pcopy[0];
            return P;
        }

        public static void ShowResult(double x1, double x2, float e)
        {
            double[] F = new double[2];
            double[] P0 = new double[2];
            P0[0] = x1;
            P0[1] = x2;
            F[0] = NewtonEquation.CalculateFirstFunc(x1, x2);
            F[1] = NewtonEquation.CalculateSecondFunc(x1, x2);
            stepText.Add($"func: {F[0]} {F[1]}");
            double[,] J = new double[2, 2];
            J[0, 0] = Math.Cos(x1);
            J[0, 1] = -2 * x2;
            J[1, 0] = 2 * Math.Tan(x1) * (Math.Pow(Math.Tan(x1), 2) + 1);
            J[1, 1] = -1;
            //J[0, 0] = 2 * x1 - 2;
            //J[0, 1] = 2 * x1;
            //J[1, 0] = -1;
            //J[1, 1] = 8 * x2;
            stepText.Add($"deratives: 1) {J[0, 0]}, 3) {J[0, 1]}, 3) {J[1, 0]}, 4) {J[1, 1]}");
            stepText.Add($"|{J[0, 0]}|+ |{J[0, 1]}| = {J[0, 0] + J[0, 1]} < 1");
            stepText.Add($"|{J[1, 0]}|+ |{J[1, 1]}| = {J[1, 0] + J[1, 1]} < 1");
            //var P = NewtonEquation.Multiplication(F, J);
            double[] P = NewtonEquation.Multiplication(J, F);
            double[] prevP = new double[2];
            double[] prevDeltaP = new double[2];
            stepText.Add($"P: {P[0]} {P[1]}");
            Newton(0, P0, x1, x2, prevDeltaP, e, prevP);


        }
        public static void Newton(int counter, double[] P, double x1, double x2, double[] prevDeltaP, float e, double[] prevP)
        {
            double[] F = new double[2];
            P[0] = x1;
            P[1] = x2;
            F[0] = NewtonEquation.CalculateFirstFunc(x1, x2);
            F[1] = NewtonEquation.CalculateSecondFunc(x1, x2);
            stepText.Add($"func: {F[0]} {F[1]}");
            double[,] J = new double[2, 2];
            J[0, 0] = Math.Cos(x1);
            J[0, 1] = -2 * x2;
            J[1, 0] = 2 * Math.Tan(x1) * (Math.Pow(Math.Tan(x1), 2) + 1);
            J[1, 1] = -1;
            double[] deltaP = NewtonEquation.Multiplication(J, F);
            if (counter != 0)
            {
                deltaP[0] = deltaP[0] * prevDeltaP[0];
                deltaP[1] = deltaP[1] * prevDeltaP[1];
            }
            stepText.Add($"P: {P[0]} {P[1]}");
            prevP[0] = P[0];
            prevP[1] = P[1];
            prevDeltaP[0] = deltaP[0];
            prevDeltaP[1] = deltaP[1];
            P[0] = deltaP[0] + P[0];
            P[1] = deltaP[1] + P[1];
            stepText.Add($"Step: {counter}, X: {P[0]}, Y: {P[1]}");
            stepText.Add($"Check: {NewtonEquation.CalculateFirstFunc(P[0], P[1])}");
            double checkValue = Math.Abs(P[0] - prevP[0]);
            if (checkValue < e && counter != 0)
            {
                counter++;
                stepText.Add($"Result: X: {P[0]}, Y: {P[1]} < e: {e}");
                stepText.Add($"Check: {NewtonEquation.CalculateFirstFunc(P[0], P[1])}");
            }
            else
            {
                counter++;
                Newton(counter, P, P[0], P[1], prevDeltaP, e, prevP);
            }
        }
    }
}
