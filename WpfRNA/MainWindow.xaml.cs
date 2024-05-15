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
         
            var series = new LineSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 5,
                MarkerStroke = OxyColors.Red,
                MarkerStrokeThickness=2,
                MarkerFill=OxyColors.Red
                
            };

            for (int i = 0; i < 500; i++)
            {
                series.Points.Add(new DataPoint(x[i], y[i]));
            }
            

            PlotModel.Series.Add(series);

            DataContext = this;
        }
    }
}
