using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A9
{
    class EquationSolver
    {
        public double[,] Matrix { get; set; }
        public int MatrixSize { get; set; }
        public EquationSolver(double[,] m,int n)
        {
            this.Matrix = m;
            this.MatrixSize = n;
        }
        public double[] GaussianElimination()
        {
            for (int i = 0; i < MatrixSize; i++)
            {
                var pivot = Matrix[i, i];
                if (pivot == 0)
                    for (int m = i + 1; m < MatrixSize; m++)
                        if (Matrix[m,i] != 0)
                            SwapRows(m, i);
                MultiplyRow(i, 1 / pivot);
                for (int j = i+1; j < MatrixSize; j++)
                    if (Matrix[j , i] != 0)
                        MakeZero(i, j);
            }
            return Round(BackSubstitution());
        }

        private double[] Round(double[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] = Math.Round(2 * arr[i]) / 2;
            return arr;
        }

        private double[] BackSubstitution()
        {
            double[] res = new double[MatrixSize];
            res[MatrixSize - 1] = Matrix[MatrixSize - 1, MatrixSize];
            for (int i = MatrixSize -2; i >= 0; i--)
            {
                res[i] = Matrix[i, MatrixSize];
                for (int j = i + 1; j < MatrixSize; j++)
                    res[i] = res[i] - Matrix[i, j] * res[j];
                res[i] = res[i] / Matrix[i, i];
            }
            return res;
        }

        public void MakeZero(int i, int j)
        {
            double[] temps = new double[MatrixSize + 1];
            for (int k = 0; k <= MatrixSize; k++)
                temps[k] = Matrix[i, k] * Matrix[j, i];
            for (int k = 0; k <= MatrixSize; k++)
                Matrix[j, k] = Matrix[j,k] - temps[k];
            return;
        }
        public void Substract(int n, int m)
        {
            for (int i = 0; i <= MatrixSize; i++)
                Matrix[n, i] = Matrix[n, i] - Matrix[m, i];
            return;
        }
        public void MultiplyRow(int n, double k)
        {
            for (int i = 0; i <= MatrixSize; i++)
                Matrix[n, i] *= k;
        }
        public void SwapRows(int n, int m)
        {
            for (int i = 0; i <= MatrixSize; i++)
                Swap(ref Matrix[n, i], ref Matrix[m, i]);
            return;
        }
        public void Swap(ref double n, ref double m)
        {
            var temp = n;
            n = m;
            m = temp;
            return;
        }
    }
}
