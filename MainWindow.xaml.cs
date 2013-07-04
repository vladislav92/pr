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

namespace WpfApplication1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        

        static double[] plus(double[] a, double[] b)//сложение одномерных массивов(срок)
        {
            double[] t = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                t[i] = a[i] + b[i];
            }
            return t;
        }

        static double[] mul(double[] a, double b)//умножение массива на число
        {
            double[] t = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                t[i] = a[i] * b;
            }
            return t;
        }

        static double[][] MatrixCreate(int rows, int cols) // создавать пустую матрицу заданного размера
        {
            double[][] result = new double[rows][];
            for (int i = 0; i < rows; ++i)
                result[i] = new double[cols];
            return result;
        }

        static void GAUSS(double[][] a, double[][] b, double[][] x)// основная функция
        //GAUSS(A,B,X);
        //А - заданная матрица
        //В - столбец свободных членов
        //Х - столбец решений <- для вывода
        //тянет за собой mul(),plus(),MatrixCreate()
        {
            double[][] g = MatrixCreate(a.Length, a[0].Length + 1);
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < a[0].Length; j++)
                {
                    g[i][j] = a[i][j];
                }
                g[i][a.Length] = b[0][i];
            }

            for (int i = 0; i < g.Length; i++) //прямой ход
            {
                g[i] = mul(g[i], 1 / (g[i][i]));

                for (int k = i + 1; k < g.Length; k++)
                {
                    g[k] = plus(g[k], mul(g[i], -g[k][i]));
                }


            }


            for (int i = g.Length - 1; i >= 0; i--) //обратнй ход
            {
                double s = 0;
                for (int k = g.Length - 1; k > i; k--)
                {
                    s += g[i][k] * x[0][k];
                }

                x[0][i] = (g[i][g[0].Length - 1] - s) / g[i][i];

            }



        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //считываем А
            string[] sarrn;
            sarrn = boxA.Text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            string[][] sarr = new string[sarrn.Length][];
            for (int i = 0; i < sarrn.Length; i++)
            {
                sarrn[i] = sarrn[i].Trim(new char[] { '\r' });
                sarr[i] = sarrn[i].Split(new char[] { ' ', '\t' });

            }

            double[][] A = new double[sarr.Length][];
            for (int i = 0; i < sarr.Length; i++)
            {
                A[i] = new double[sarr[0].Length];
                for (int j = 0; j < sarr[0].Length; j++)
                {

                    A[i][j] = Convert.ToDouble(sarr[i][j]);
                }

            }
            //считываем B
            string sbstr = boxB.Text;
            string[][] bstr = new string[1][];
            bstr[0] = sbstr.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            double[][] B = MatrixCreate(1, bstr[0].Length); ;

            for (int i = 0; i < B[0].Length; i++)
            {

                B[0][i] = Convert.ToDouble(bstr[0][i]);
            }


            double[][] X = MatrixCreate(1, A.Length);

            GAUSS(A, B, X);
            boxX.Text = "";
            for (int i = 0; i < X[0].Length; i++)
            {
                boxX.Text += Convert.ToString(X[0][i]);
                boxX.Text += "\n";
            }




        } 


    }
}
