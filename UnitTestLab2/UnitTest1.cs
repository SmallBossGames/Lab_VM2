using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab2_VM;

namespace UnitTestLab2
{
    [TestClass]
    public class UnitTest1
    {
        

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
            decimal[,] matrix =
            {
                { 1,2,3,3 },
                { 3,5,7,0 },
                { 1,3,4,1 },
            };

            decimal[] validData = { -4, -13, 11 };
            decimal[] data = MatrixMath.CalculateGauss(matrix);
            decimal delta = 0.0001m;

            Assert.AreEqual(validData.Length, data.Length);

            for (var i = 0; i < validData.Length; i++)
                if (MatrixMath.Abs(data[i] - validData[i]) > delta)
                    Assert.Fail(MatrixMath.Abs(data[i] - validData[i]).ToString());
        }

        [TestMethod]
        public void TestGauss2()
        {
            decimal[,] matrix =
            {
                { -7,2,-2,-7 },
                { -2,5,-2,5 },
                { -3,-1,-5,-5 },
            };

            decimal[] validData = { 73m/49, 223m/147, -29m/147 };
            decimal[] data = MatrixMath.CalculateGauss(matrix);
            decimal delta = 0.01m;

            Assert.AreEqual(validData.Length, data.Length);

            for (var i = 0; i < validData.Length; i++)
                if (MatrixMath.Abs(data[i] - validData[i]) > delta)
                    Assert.Fail(MatrixMath.Abs(data[i] - validData[i]).ToString());
        }
    }
}
