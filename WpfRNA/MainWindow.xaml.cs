using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfRNA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {   
        public PlotModel PlotModel { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

           

            double[] x = new double[501];
            double[] y = new double[501];

            double a = 1.4;
            double b = 0.3;
            x[0] = 0;
            y[0] = 0;

            Console.WriteLine("voici les 500 premières valeurs:");
            for (int i = 0; i < 500; i++)
            {
                x[i + 1] = y[i] + 1 - (a * Math.Pow(x[i], 2));
                y[i + 1] = b * x[i];
                listex.Items.Add(x[i]);
                listey.Items.Add(y[i]);
                Console.WriteLine("x[" + (i + 1) + "]=" + x[i + 1]);
                Console.WriteLine("y[" + (i + 1) + "]=" + y[i + 1]);
            }

            PlotModel = new PlotModel { Title = "Ma courbe" };


            var series = new ScatterSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 2,
                MarkerStroke = OxyColors.DarkGray,
                MarkerStrokeThickness = 1,
                MarkerFill = OxyColors.DarkGray
            };

            for (int i = 0; i < 500; i++)
            {
                series.Points.Add(new ScatterPoint(x[i], y[i]));
            }
            

            PlotModel.Series.Add(series);

            DataContext = this;

            double[,] matrice1 = { {1,2,3},{ 2, 2, 2 } };
            double[,] matrice2 = { { 1, 0, 0},{ 0,1,0},{ 0, 0, 1 } };

            AffichageMatrice(multiplication(matrice1, matrice2));
        }

        //fonction des matrices
        public double[,] multiplication(double[,] matriceA, double[,] matriceB)
        {
            int liA = matriceA.GetLength(0); 
            int colA = matriceA.GetLength(1);

            int liB = matriceB.GetLength(0);
            int colB = matriceB.GetLength(1);

            if (liA != colB)
            {
                Console.WriteLine("on ne peut pas multiplier ces deux matrice"+ matriceA+" et "+matriceB);
            }
           
             double[,] resultat = new double[liA, colB];
             for (int i = 0; i < liA; i++)
             {
                    for (int j = 0; j < colB; j++)
                    {
                        for (int k = 0; k < colA; k++)
                        {
                            resultat[i, j] = matriceA[i, k] * matriceB[k, j];
                        }
                    }
             }
                          
            return resultat;

        }

        static void AffichageMatrice(double[,] matrice)
        {
            int li = matrice.GetLength(0);
            int col = matrice.GetLength(1);

            for(int i = 0; i < li; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Console.Write(matrice[i, j] + "/t");
                }
                Console.WriteLine();
            }
        }

        public void Apprentissage()
        {
            //nombre de couche
            int m = 2;

            //nombre d'entrer
            int entrees = 3;

            //nombre de neurone dans première couche ici le nombre de neuronne dans la couche 2 et 3 est le meme 
            int cNeurone1 = 2;

           
            int i= cNeurone1;
            int j=m;

            //Calcul de la sortie des differents noeuds noté V
            //les valeurs V de la première couches noté X dans le carnet
            double[,] V = new double[j,i];

            //h= somme de Wij*Vmj
            double[,] H = new double[j, i];

            //poids de connexion 
            double[,] W = new double[m, m];

            //Neurones d'entrées
            V[0, 0] = 0;
            V[0, 1] = 1;
            V[0, 2] = 1;


            



        }




        public double FonctionActivation(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }

        private void ArchtectureOptimale_click(object sender, RoutedEventArgs e)
        {
            ArchitectureOptimale architecture =new ArchitectureOptimale();
            architecture.Show();

        }
    }

}
