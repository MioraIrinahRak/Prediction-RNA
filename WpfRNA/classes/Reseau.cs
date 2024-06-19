using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfRNA
{
    class Reseau
    {
        public Couche[] Couches { get; set; }

        public Reseau(int nbrCouches, int[] neuronesParCouche)
        {
            Couches = new Couche[nbrCouches];

            for (int i = 0; i < nbrCouches; i++)
            {
                int entréeNeurone = (i == 0) ? neuronesParCouche[i+1] : neuronesParCouche[i - 1];
                Couches[i] = new Couche(neuronesParCouche[i], entréeNeurone);

            }
        }

        // Fonction d'activation Sigmoid
        public double Sigmoid(double x)
        {
            return 1.0 / (1.0 + Math.Exp(-x));
        }

        // Dérivée de Sigmoid
        public double SigmoidDerivative(double x)
        {
            double sigmoid = Sigmoid(x);
            return sigmoid * (1 - sigmoid);
        }

        public void propagationAvant(double[] entrée)
        {
            double[] nouvelEntrée = entrée;

            //Etape 3
            for (int i = 0; i < Couches.Length; i++)
            {
                Couche couche = Couches[i];
                if (couche == null)
                {
                    throw new Exception($"Couches[{i}] n\'est pas initialise.");
                }

                double[] entréeSuivant = new double[couche.Neurones.Length];


                for (int j = 0; j < couche.Neurones.Length; j++)
                {
                    Neurone neurone = couche.Neurones[j];
                    double h = 0;

                    for (int k = 0; k < nouvelEntrée.Length; k++)
                    {
                        h += neurone.Poids[k] * nouvelEntrée[k];
                    }
                    neurone.Sortie = Sigmoid(h);
                    entréeSuivant[j] = neurone.Sortie;
                }
                nouvelEntrée = entréeSuivant;

            }
        }
        //etape 4 calcul de Delta pour la couche de sortie
        public void retopropa(double[] sortieDersirée, double pasApprentissage)
        {

            int lastCouche = Couches.Length;

            Console.WriteLine($"la dernière couche:{lastCouche}");

            Couche coucheSortie = Couches[lastCouche - 1];
            Couche couchePrecedent = Couches[lastCouche - 1];
            int nombreNeuroneSortie = coucheSortie.Neurones.Length;

            double[] sig = new double[sortieDersirée.Length];
            double[] sigmaTab = new double[sortieDersirée.Length];
            //verification si le nombre de sortie desirée correspond  au nombre de neurone dans la couche de sortie
            if (nombreNeuroneSortie == sortieDersirée.Length)
            {
                //calcul de total de g'(h)
                for (int j = 0; j < coucheSortie.Neurones.Length; j++)
                {
                    double h = 0;
                    for (int i = 0; i < couchePrecedent.Neurones.Length; i++)
                    {
                        Neurone neurone = couchePrecedent.Neurones[i];
                        h += neurone.Poids[j] * neurone.Sortie;
                    }
                    double sommeSigmoidDerivate = SigmoidDerivative(h);
                    sig[j] = sommeSigmoidDerivate;
                }
                //calcul de sigma-sortie
                for (int j = 0; j < coucheSortie.Neurones.Length; j++)
                {
                    double sigma = 0;

                    Neurone neurone = coucheSortie.Neurones[j];
                    sigma = sortieDersirée[j] - neurone.Sortie;

                    sigmaTab[j] = sigma;

                }

                double[] deltaTab = new double[coucheSortie.Neurones.Length];

                //calcul de delta
                for (int i = 0; i < coucheSortie.Neurones.Length; i++)
                {
                    Neurone neurone = coucheSortie.Neurones[i];
                    neurone.Delta = sig[i] * sigmaTab[i];
                    Console.WriteLine($"sigma du neurone de sortie= {neurone.Delta}");
                }

                //calcul des delta pour les >couches precedent< la couche de sortie

                //etape 5 : retropropagation TETO NISY >= AH

                for (int i = Couches.Length - 2; i > 0; i--)
                {
                    Couche couche = Couches[i];
                    Couche coucheSuivante = Couches[i + 1];
                    Couche coucheAvant = Couches[i - 1];
                    for (int j = 0; j < couche.Neurones.Length; j++)
                    {
                        Neurone neurone = couche.Neurones[j];
                        double part2 = 0;
                        double h = 0;
                        for (int k = 0; k < coucheSuivante.Neurones.Length; k++)
                        {
                            part2 += coucheSuivante.Neurones[k].Poids[j] * coucheSuivante.Neurones[k].Delta;

                        }

                        for (int l = 0; l < coucheAvant.Neurones.Length; l++)
                        {
                            h += coucheAvant.Neurones[l].Poids[j] * coucheAvant.Neurones[l].Sortie;
                        }

                        neurone.Delta = SigmoidDerivative(h) * part2;
                        Console.WriteLine($"sigma du neurone neurone n°{j}= {neurone.Delta}");
                    }
                }


                //etape 6 : mis à jour des poids
                for (int i = 1; i < Couches.Length; i++)
                {
                    Couche couche = Couches[i];
                    Couche coucheP = Couches[i - 1];
                    for (int j = 0; j < couche.Neurones.Length; j++)
                    {
                        Neurone neurone = couche.Neurones[j];
                        for (int k = 0; k < coucheP.Neurones.Length; k++)
                        {
                            Neurone neuroneP = coucheP.Neurones[k];

                            neurone.Poids[k] += pasApprentissage * (neurone.Delta * neuroneP.Sortie);


                        }
                    }
                }

            }
            else
            {
                Console.WriteLine("Le nombre de sortie n'est pas egale au nombre de sortie desirée");
            }


        }

        public void Entrainement(double[] entrée, double pasApprentissage, double[] sortieDesirée)
        {
            propagationAvant(entrée);
            retopropa(sortieDesirée, pasApprentissage);
        }

        public void prediction(double[] suiteDeValeurs)
        {
            double[] nouvelleSuite = suiteDeValeurs;
            int nbrCouche = Couches.Length;
            Couche couche = Couches[nbrCouche - 2];
            Couche coucheSortie = Couches[nbrCouche - 1];


            for (int i = 0; i < coucheSortie.Neurones.Length; i++)
            {
                Neurone neuroneSortie = coucheSortie.Neurones[i];
                double ValeurPredite = 0;
                for (int j = 0; j < couche.Neurones.Length; j++)
                {
                    Neurone neurone = couche.Neurones[j];
                    ValeurPredite += neurone.Poids[i] * neurone.Sortie;
                }
                Console.WriteLine($"la valeur predite est {ValeurPredite}");
            }
        }

        public double[] predictionUnPas(double[] suiteDeValeurs, double PasApprentissage, double[] sortieDerisée)
        {
            double[] nouvelleSuite = suiteDeValeurs;
            double[] valeurPreditetab = new double[suiteDeValeurs.Length];
            int nbrCouche = Couches.Length;


            for (int k = 0; k < suiteDeValeurs.Length - Couches[0].Neurones.Length; k++)
            {
                double[] entrée = new double[Couches[0].Neurones.Length];
                for (int m = 0; m < Couches[0].Neurones.Length; m++)
                {
                    entrée[m] = suiteDeValeurs[m + k];
                    Console.WriteLine(entrée[m]);
                }
                propagationAvant(entrée);

                //!!!
                Couche couche = Couches[nbrCouche - 2];
                Couche coucheSortie = Couches[nbrCouche - 1];

                for (int i = 0; i < coucheSortie.Neurones.Length; i++)
                {
                    Neurone neuroneSortie = coucheSortie.Neurones[i];
                    double ValeurPredite = 0;
                    for (int j = 0; j < couche.Neurones.Length; j++)
                    {
                        Neurone neurone = couche.Neurones[j];

                        //!!!
                        ValeurPredite += neuroneSortie.Poids[i] * neurone.Sortie;
                    }
                    Console.WriteLine($"la valeur predite de u{k} est  {ValeurPredite}");

                    valeurPreditetab[k] = k==0 ? suiteDeValeurs[0] :ValeurPredite;
                }
            }

            return valeurPreditetab;
        }

    }
}
