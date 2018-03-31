using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab2_VM;

namespace UnitTestLab2
{
    [TestClass]
    public class UnitTest1
    {
        decimal[,] matrix1 =
            {
                { -7,2,-2,-7 },
                { -2,5,-2,5 },
                { -3,-1,-5,-5 },
            };

        decimal[] validData1 = { 73m / 49, 223m / 147, -29m / 147 };

        decimal[,] matrix2 =
            {
                { 1,2,3,3 },
                { 3,5,7,0 },
                { 1,3,4,1 },
            };

        decimal[] validData2 = { -4, -13, 11 };

        decimal[,] matrix3 =
        {
                { 100,5,6,7 },
                { 6,100,7,0 },
                { 9,3,300,14 },
        };



        [TestMethod]
        public void TestMaxStringIndex()
        {
            decimal[,] matrix =
            {
                { 1,2,3,4,5 },
                { 4,6,1,8,2 },
                { 6,228,14,-3,-6 },
                { -99,6,1,8,2 },

            };

            var validData = 3;
            var testData = MatrixMath.MaxAbsStringIndex(matrix, 0, 0, matrix.GetLength(0) - 1);
            Assert.AreEqual(testData, validData, 0, "Wrong value");

            validData = 3;
            testData= MatrixMath.MaxAbsStringIndex(matrix, 3, matrix.GetLength(0) - 1, matrix.GetLength(0) - 1);
            Assert.AreEqual(testData, validData, 0, "Wrong value");
        }

        [TestMethod]
        public void TestGauss1()
        {
            decimal[] data = MatrixMath.CalculateGauss(matrix2);
            decimal delta = 0.0001m;

            Assert.AreEqual(validData2.Length, data.Length);

            for (var i = 0; i < validData2.Length; i++)
                if (MatrixMath.Abs(data[i] - validData2[i]) > delta)
                    Assert.Fail(MatrixMath.Abs(data[i] - validData2[i]).ToString());
        }

        [TestMethod]
        public void TestGauss2()
        {
            decimal[] data = MatrixMath.CalculateGauss(matrix1);
            decimal delta = 0.01m;

            Assert.AreEqual(validData1.Length, data.Length);

            for (var i = 0; i < validData1.Length; i++)
                if (MatrixMath.Abs(data[i] - validData1[i]) > delta)
                    Assert.Fail(MatrixMath.Abs(data[i] - validData1[i]).ToString());
        }

        [TestMethod]
        public void TestZeidel1()
        {
            decimal[] data = MatrixMath.CalculateZeidel(matrix2, 0.00001m);
            decimal delta = 0.0001m;

            Assert.AreEqual(validData2.Length, data.Length);

            for (var i = 0; i < validData2.Length; i++)
                if (MatrixMath.Abs(data[i] - validData2[i]) > delta)
                    Assert.Fail(MatrixMath.Abs(data[i] - validData2[i]).ToString());
        }

        [TestMethod]
        public void TestZeidel2()
        {
            decimal[] data = MatrixMath.CalculateZeidel(matrix1, 0.00001m);
            decimal delta = 0.0001m;

            Assert.AreEqual(validData1.Length, data.Length);

            for (var i = 0; i < validData1.Length; i++)
                if (MatrixMath.Abs(data[i] - validData1[i]) > delta)
                    Assert.Fail(MatrixMath.Abs(data[i] - validData1[i]).ToString());
        }

        [TestMethod]
        public void TestCheckConvergence()
        {
            Assert.AreEqual(MatrixMath.CheckСonvergence(matrix3), true);
            Assert.AreEqual(MatrixMath.CheckСonvergence(matrix1), false);
        }
    }
}
