using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfRNA
{
    class DataShared
    {
        public static int[] neuroneParCouche { get; set; }
        public static double[] dataEntrée { get; set; }
        public static double[] dataSortieDesirée { get; set; }
        public static double dataPasApprentissage { get; set; }

        public static void tabNeuroneCouche(int taille)
        {
            neuroneParCouche = new int[taille];
        }

        public static void tabDataEntrée(int taille)
        {
            dataEntrée = new double[taille];
        }

        public static void tabSortieDesirée(int taille)
        {
            dataSortieDesirée = new double[taille];
        }

        public static void setValue(double[] value,double[] tab)
        {
            if (value.Length != tab.Length)
            {
                throw new ArgumentException($"taille du tableau {tab.Length} n' s'accorde pas avec les valeurs enteée");
            }
            Array.Copy(value, tab, value.Length);
        }
        public static void setValueint(int[] value, int[] tab)
        {
            if (value.Length != tab.Length)
            {
                throw new ArgumentException($"taille du tableau {tab.Length} n' s'accorde pas avec les valeurs enteée");
            }
            Array.Copy(value, tab, value.Length);
        }
    }
}
