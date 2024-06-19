using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfRNA
{
    class Neurone
    {
        public double[] Poids { get; set; }
        public double Sortie { get; set; }
        public double Delta { get; set; }

        public Neurone(int numInputs)
        {
            Poids = new double[numInputs];
            Random rand = new Random();
            for (int i = 0; i < numInputs; i++)
            {
                double randomValeur = rand.NextDouble();
               //randomValeur = 0.1 + randomValeur * 2;

                Poids[i] = randomValeur;
            }
        }
    }
}
