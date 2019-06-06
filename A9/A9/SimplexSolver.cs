using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A9
{
    class SimplexSolver
    {
        public int Variables { get; set; }
        public int Constraints { get; set; }
        public bool Infinity { get; set; }
        public int PivotRow { get; set; }
        public int PivotCol { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public double[,] Matrix { get; set; }
        public SimplexSolver(double[,] m, int c, int v)
        {
            Constraints = c;
            Variables = v;
            Matrix = new double[m.GetLength(0), m.GetLength(1) + c + 1];
            for (int i = 0; i < m.GetLength(0); i++)
                for (int j = 0; j < m.GetLength(1) - 1; j++)
                    Matrix[i, j] = m[i, j];
            for (int i = 0; i < m.GetLength(1) - 1; i++)
                Matrix[m.GetLength(0) - 1, i] *= -1;
            for (int i = m.GetLength(1) - 1, j = 0; i <= m.GetLength(1) + c && j < m.GetLength(0); i++, j++)
                Matrix[j,i] = 1;
            for (int i = 0; i < m.GetLength(0); i++)
                Matrix[i, Matrix.GetLength(1) - 1] = m[i, m.GetLength(1) - 1];
            Row = Matrix.GetLength(0);
            Col = Matrix.GetLength(1);
        }
        public string DriverCode()
        {
            while (!EndSatisfacttion())
            {
                FindPivotCol();
                if (CheckInfinity())
                    break;
                FindPivotRow();
                MultiplyRow(PivotRow, 1 / Matrix[PivotRow, PivotCol]);
                MakeZero();
            }
            return HandleAnswertype();
        }

        public bool CheckInfinity()
        {
            int c = 0;
            for (int i = 0; i < Row - 1; i++)
                if (Math.Round(Matrix[i, Col - 1] / Matrix[i, PivotCol], 2) < 0)
                    c++;
            if (c == Row - 1)
            {
                Infinity = true;
                return true;
            }
            return false;
        }
        private string HandleAnswertype()
        {
            string res = string.Empty;
            if (Infinity)
            {
                res = "Infinity";
                return res;
            }
            else
            {
                var values =FindAnswers();
                if (CheckAnswers(values))
                {
                    values = Round(values);
                    string s = string.Join(" ", values.Select(x => x.ToString()).ToArray());
                    res = "Bounded Solution\n" + s;
                    return res;
                }
                else
                {
                    res = "No Solution";
                    return res;
                }
            }
        }
        private double[] Round(double[] answers)
        {
            for (int i = 0; i < answers.Length; i++)
                answers[i] = Math.Round(2 * answers[i]) / 2;
            return answers;
        }
        private bool CheckAnswers(double[] answers)
        {
            for(int i = 0; i < Row - 1; i++)
            {
                double sum = 0;
                for (int j = 0; j < Variables; j++)
                    sum += answers[j] * Matrix[i, j];
                if (sum > Matrix[i,Col - 1])
                    return false;
            }
            return true;
        }
        private bool EndSatisfacttion()
        {
            int negCounter = 0;
            for (int i = 0; i < Col - 1; i++)
                if (Math.Round(Matrix[Row - 1,i], 2) < 0)
                    negCounter++;
            if (negCounter == 0)
                return true;
            else
                return false;
        }
        public double[] FindAnswers()
        {
            double[] res = new double[Variables];
            for (int i = 0; i < Variables; i++)
            {
                double answer = 0;
                for (int j = 0; j < Row; j++)
                    if (Matrix[j,i] == 1 && answer == 0)
                        answer = Matrix[j, Col - 1];
                    else if (Matrix[j, i] == 0)
                        continue;
                    else
                    {
                        answer = 0;
                        break;
                    }
                    
                res[i] = answer;
            }
            return res;
        }
        public void MakeZero()
        {
            for (int i = 0; i < Row; i++)
            {
                if (i == PivotRow)
                    continue;
                double[] temps = new double[Col];
                double ratio = Matrix[i, PivotCol] / Matrix[PivotRow, PivotCol];
                if (ratio == 0)
                    continue;
                for (int j = 0; j < Col; j++)
                    temps[j] = Matrix[PivotRow, j] * ratio;
                for (int j = 0; j < Col; j++)
                    Matrix[i, j] -= temps[j];
            }
            return;
        }
        
        public void MultiplyRow(int n, double k)
        {
            for (int i = 0; i < Col; i++)
                Matrix[n, i] *= k;
        }
        public int FindPivotCol()
        {
            var minIndex = 0;
            var min = double.MaxValue;
            for (int i = 0; i < Matrix.GetLength(1); i++)
                if (Matrix[Row - 1, i] < min)
                {
                    min = Matrix[Row - 1, i];
                    minIndex = i;
                }
            PivotCol = minIndex;
            return minIndex;
        }
        public int FindPivotRow()
        {
            var minIndex = 0;
            var min = double.MaxValue;
            for (int i = 0; i < Row - 1; i++)
                if (Math.Round(Matrix[i, Col - 1] / Matrix[i, PivotCol], 2) < min && Matrix[i, PivotCol] != 0)
                    if (Math.Round(Matrix[i, Col - 1] / Matrix[i, PivotCol], 2) >= 0)
                    {
                        minIndex = i;
                        min = Matrix[i, Col - 1] / Matrix[i, PivotCol];
                    }
            PivotRow = minIndex;
            return minIndex;
        }
    }
}
