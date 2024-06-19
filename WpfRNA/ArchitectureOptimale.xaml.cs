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

namespace WpfRNA
{
    /// <summary>
    /// Interaction logic for ArchitectureOptimale.xaml
    /// </summary>
    public partial class ArchitectureOptimale : Window
    {
        
        public ArchitectureOptimale()
        {
            InitializeComponent();
            //tableau nombre de neurone par couche
            DataShared.tabDataEntrée(3);
            DataShared.tabNeuroneCouche(3);
            DataShared.tabSortieDesirée(1);
            double[] valeurEntree = {0, 1,-0.39999};
            double[] valeurSortie = { 1.076 };

            DataShared.setValue(valeurEntree, DataShared.dataEntrée);
            DataShared.setValue(valeurSortie, DataShared.dataSortieDesirée);
            DataShared.dataPasApprentissage = 0.1;

            int[] valeursNeuroneParC = { DataShared.dataEntrée.Length,17,1 };
            DataShared.setValueint(valeursNeuroneParC,DataShared.neuroneParCouche);

            int nombreCouche = DataShared.neuroneParCouche.Length;

            Reseau ReseauMi = new Reseau(nombreCouche, DataShared.neuroneParCouche);


            //les donnée d'entrée

           

           

            AffichagenbrCouches.Text = nombreCouche.ToString();

            

            ReseauMi.Entrainement(DataShared.dataEntrée, DataShared.dataPasApprentissage, DataShared.dataEntrée);
            //suite de donnée à predire

                                    
        }

        private void PredictionUn_click(object sender, RoutedEventArgs e)
        {
            PredictionUnPas prediction = new PredictionUnPas();
            prediction.Show();

        }

    }
    
}
