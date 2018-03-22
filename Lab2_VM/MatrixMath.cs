using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_VM
{
    public class MatrixMath
    {

        public static decimal[] CalculateGauss(decimal[,] matrix)
        {
            var strCount = matrix.GetLength(0);
            var columnCount = matrix.GetLength(1);
            var resArray = new decimal[strCount];

            if (strCount + 1 != columnCount) throw new IncorrectMatrixException();

            for (var i = 0; i < strCount; i++)
            {
                var maxIndex = MaxAbsStringIndex(matrix, i, i, strCount-1);

                if (matrix[i, maxIndex] == 0) throw new IncorrectMatrixException();

                SwapString(matrix, i, maxIndex);

                for (var j = i + 1; j < strCount; j++)
                {
                    var koeff = -(matrix[j, i] / matrix[i, i]);
                    StringSum(matrix, i, j, koeff);
                    matrix[j, i] = 0;
                }

                DivideString(matrix, i, matrix[i, i]);
            }

            for (var i = strCount - 1; i > -1; i--) 
            {
                var result = matrix[i, columnCount - 1];

                for (var j = i + 1; j < strCount; j++)
                    result -= matrix[i, j] * resArray[j];

                resArray[i] = result;
            }

            for (var i = 0 ; i < strCount; i++)
            {
                for (var j = 0; j < columnCount; j++)
                    Console.Write(matrix[i, j].ToString() + ' ');
                Console.WriteLine();
            }

            return resArray;
        }

        public static int MaxAbsStringIndex(decimal[,] matrix, int column, int startStr, int endStr)
        {
            int maxIndex = startStr;
            for (int i = startStr; i <= endStr; i++)
            {
                if (Abs(matrix[maxIndex, column]) < Abs(matrix[i, column]))
                    maxIndex = i;
            }
            return maxIndex;
        }

        static void SwapString(decimal[,] matrix, int index1, int index2)
        {
            if (index1 == index2) return;
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                var pool = matrix[index1, i];
                matrix[index1, i] = matrix[index2, i];
                matrix[index2, i] = pool;
            }
        }

        static void StringSum(decimal[,] matrix, int from, int to, decimal koeff)
        {
            for (int i = 0; i < matrix.GetLength(1); i++)
                matrix[to, i] += matrix[from, i] * koeff;
        }

        static void DivideString(decimal[,] matrix, int index, decimal koeff)
        {
            var length = matrix.GetLength(1);
            for (var i = 0; i < length; i++)
                matrix[index, i] /= koeff;
        }

        public static decimal Abs(decimal value) => value > 0 ? value : -value;

        class IncorrectMatrixException:Exception
        {
            public override string Message => "Matrix is incorrect";
        }
    }
}
