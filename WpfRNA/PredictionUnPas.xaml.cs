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
using System.Windows.Shapes;
using OxyPlot;
using OxyPlot.Series;
using WpfRNA;

namespace WpfRNA
{
    /// <summary>
    /// Interaction logic for PredictionUnPas.xaml
    /// </summary>
    public partial class PredictionUnPas : Window
    {
        public PlotModel Prediction { get; private set; }
        public PredictionUnPas()
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
            }
            //prediction
            int nombreCouche = DataShared.neuroneParCouche.Length;

            Reseau ReseauMi = new Reseau(nombreCouche, DataShared.neuroneParCouche);

            ReseauMi.Entrainement(DataShared.dataEntrée, DataShared.dataPasApprentissage, DataShared.dataEntrée);
            double[] suite = new double[100];
            for (int i = 0; i < suite.Length; i++)
            {
                suite[i] = x[i];
            }

            double[] valeurPredit = ReseauMi.predictionUnPas(suite, DataShared.dataPasApprentissage, DataShared.dataSortieDesirée);


            //Tracage de la courbe
            Prediction = new PlotModel { Title = "Prediction un pas en avant" };

            //valeurs attendues
            var ValeursAttendues = new LineSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 2,
                MarkerStroke = OxyColors.DarkGray,
                MarkerStrokeThickness = 1,
                MarkerFill = OxyColors.DarkGray
            };
            for (int i = 0; i < suite.Length; i++)
            {
                ValeursAttendues.Points.Add(new DataPoint(i,x[i]));
            }

            //valeurs predites
            var ValeursPredites = new LineSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 2,
                MarkerStroke = OxyColors.BlueViolet,
                MarkerStrokeThickness = 1,
                MarkerFill = OxyColors.DarkGray
            };
            for (int i = 0; i < suite.Length; i++)
            {
                ValeursPredites.Points.Add(new DataPoint(i, valeurPredit[i]));
            }

            Prediction.Series.Add(ValeursAttendues);
            Prediction.Series.Add(ValeursPredites);

            DataContext = this;


           




        }
        private void OpenFenetrePredictionUn_click(object sender, RoutedEventArgs e)
        {
            PredictionUnPas PredictionUn = new PredictionUnPas();
            PredictionUn.Show();

        }
    }
}
