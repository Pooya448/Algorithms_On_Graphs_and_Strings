using System;
using TestCommon;

namespace A9
{
    public class Q1InferEnergyValues : Processor
    {
        public Q1InferEnergyValues(string testDataName) : base(testDataName)
        {
            ExcludeTestCases(28);
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, double[,], double[]>)Solve);

        public double[] Solve(long MATRIX_SIZE, double[,] matrix)
        {
            EquationSolver s = new EquationSolver(matrix, (int)MATRIX_SIZE);
            return s.GaussianElimination();
            
        }    
     }
}
