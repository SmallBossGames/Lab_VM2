using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Lab2_VM;

namespace Lab2_VM
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            decimal[,] matrix =
            {
                { 1,2,3,3 },
                { 3,5,7,0 },
                { 1,3,4,1 },
            };
            decimal[] data = MatrixMath.CalculateZeidel(matrix, 0.0001m);
            decimal[] data2 = MatrixMath.CalculateGauss(matrix);
            InitializeComponent();
        }
    }
}
