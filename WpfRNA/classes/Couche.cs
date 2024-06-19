using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfRNA
{
    class Couche
    {
        public Neurone[] Neurones { get; set; }

        public Couche(int nbrNeurones, int entreeParNeurone)
        {
            Neurones = new Neurone[nbrNeurones];
            for (int i = 0; i < nbrNeurones; i++)
            {
                Neurones[i] = new Neurone(entreeParNeurone);
            }
        }
    }
}
