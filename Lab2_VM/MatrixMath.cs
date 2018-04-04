using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_VM
{
    public static class MatrixMath
    {

        /// <summary>
        /// Вычисляет значения корней СЛАУ по методу Гаусса
        /// </summary>
        /// <param name="matrix">Входная расширенная матрица</param>
        public static decimal[] CalculateGauss(decimal[,] matrix)
        {
            var triangleMatrix = GetTriangleGaussMatrix(matrix);
            return GetResultArray(triangleMatrix);
        }

        /// <summary>
        /// Вычисляет значения корней СЛАУ по методу Гаусса и возвращает треугольную матрицу
        /// как параметр
        /// </summary>
        /// <param name="matrix">Входная расширенная матрица</param>
        public static decimal[] CalculateGauss(decimal[,] matrix, out decimal[,] triangleMatrix)
        {
            triangleMatrix = GetTriangleGaussMatrix(matrix);
            return GetResultArray(triangleMatrix);
        }

        public static decimal[,] GetTriangleGaussMatrix(decimal[,] matrix)
        {
            var strCount = matrix.GetLength(0);
            var columnCount = matrix.GetLength(1);

            if (strCount + 1 != columnCount) throw new IncorrectMatrixException();

            var outMatrix = matrix.Clone() as decimal[,];

            for (var i = 0; i < strCount; i++)
            {
                var maxIndex = MaxAbsStringIndex(outMatrix, i, i, strCount - 1);

                if (outMatrix[i, maxIndex] == 0) throw new IncorrectMatrixException();

                SwapString(outMatrix, i, maxIndex);

                for (var j = i + 1; j < strCount; j++)
                {
                    var koeff = -(outMatrix[j, i] / outMatrix[i, i]);
                    StringSum(outMatrix, i, j, koeff);
                }

                DivideString(outMatrix, i, outMatrix[i, i]);
            }

            return outMatrix;
        }

        /// <summary>
        /// Находит индекс строки, в которой значение заданного элемента наибольшее
        /// </summary>
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

        /// <summary>
        /// Меняет местами строки в матрице
        /// </summary>
        /// <param name="matrix">Матрица</param>
        /// <param name="index1">Индекс первой строки</param>
        /// <param name="index2">Индекс второй строки</param>
        static void SwapString(decimal[,] matrix, int index1, int index2)
        {
            if (index1 == index2) return;
            for (int i = 0; i < matrix.GetLength(1); i++)
                (matrix[index1, i], matrix[index2, i]) = (matrix[index2, i], matrix[index1, i]);
        }

        /// <summary>
        /// К указанной строке прибавляется другая, домноженная на коэффициент
        /// </summary>
        static void StringSum(decimal[,] matrix, int from, int to, decimal koeff=1)
        {
            for (int i = 0; i < matrix.GetLength(1); i++)
                matrix[to, i] += matrix[from, i] * koeff;
        }

        /// <summary>
        /// Делит строку на указанный коэффициент
        /// </summary>
        static void DivideString(decimal[,] matrix, int index, decimal koeff=1)
        {
            var length = matrix.GetLength(1);
            for (var i = 0; i < length; i++)
                matrix[index, i] /= koeff;
        }

        static decimal[] GetResultArray(decimal[,] upTriangleMatrix)
        {
            var strCount = upTriangleMatrix.GetLength(0);
            var columnCount = upTriangleMatrix.GetLength(1);

            if (strCount + 1 != columnCount) throw new IncorrectMatrixException();

            var resArray = new decimal[strCount];

            for (var i = strCount - 1; i > -1; i--)
            {
                var result = upTriangleMatrix[i, columnCount - 1];

                for (var j = i + 1; j < strCount; j++)
                    result -= upTriangleMatrix[i, j] * resArray[j];

                resArray[i] = result;
            }

            return resArray;
        }

        public static decimal Abs(decimal value) => value > 0 ? value : -value;


        /// <summary>
        /// Вычисляет значения корней СЛАУ по методу Зейделя
        /// </summary>
        /// <param name="matrix">Входная расширенная матрица</param>
        public static decimal[] CalculateZeidel(decimal[,] matrix, decimal accuracy)
        {
            var strCount = matrix.GetLength(0);
            var columnCount = matrix.GetLength(1);


            if (strCount + 1 != columnCount) throw new IncorrectMatrixException();

            var poolMatrix = new decimal[strCount, columnCount];
            decimal[]
                lastResArray = new decimal[strCount],
                resArray = new decimal[strCount];

            for (var i = 0; i < strCount; i++)
            {
                for (var j = 0; j < strCount; j++)
                {
                    poolMatrix[i, j] = -(matrix[i, j] / matrix[i, i]);
                }
                poolMatrix[i, columnCount - 1] = (matrix[i, columnCount - 1] / matrix[i, i]);
                resArray[i] =  poolMatrix[i, columnCount - 1];
            }

            do
            {
                (lastResArray, resArray) = (resArray, lastResArray);

                for (var i = 0; i < strCount; i++)
                {
                    resArray[i] = 0;

                    for (var j = 0; j < i; j++)
                    {
                        resArray[i] += resArray[j] * poolMatrix[i, j];
                    }

                    for (var j = i+1; j < columnCount - 1; j++)
                    {
                        resArray[i] += lastResArray[j] * poolMatrix[i, j];
                    }

                    resArray[i] += poolMatrix[i, columnCount - 1];
                }
            } while (!CheckAccuracy(resArray, lastResArray, accuracy));

            return resArray;
        }


        /// <summary>
        /// Проверяет точность вычисления
        /// </summary>
        /// <param name="newRes">Новые значения корней</param>
        /// <param name="lastRes">Старые значения корней</param>
        /// <param name="eps">Точность</param>
        private static bool CheckAccuracy(decimal[] newRes, decimal[] lastRes, decimal eps)
        {
            var length = newRes.Length;
            var delta = 0.0m;

            if (length != lastRes.Length) throw new FormatException();

            for (int i = 0; i < length; i++)
                delta += Abs(newRes[i] - lastRes[i]);

            if (delta < eps)
                return true;

            return false;
        }

        /// <summary>
        /// Проверяет сходимость согласно критерию (матрица должна быть диагонально доминирующей)
        /// </summary>
        public static bool CheckСonvergence(decimal[,] matrix)
        {
            var strCount = matrix.GetLength(0);

            for (var i = 0; i < strCount; i++)
            {
                var norm = 0m;
                for (var j = 0; j < strCount; j++)
                    if (i != j) norm += Abs(matrix[i, j]);

                if (matrix[i,i] <= norm) return false;
            }

            return true;
        }



        class IncorrectMatrixException:Exception
        {
            public override string Message => "Matrix is incorrect";
        }
    }
}
