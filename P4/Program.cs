using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace P4
{
    class Butas
    {
        private int nr,
                    kambariai;
        private double plotas,
                       kaina;
        private string telNr;

        public Butas()
        {
            this.nr = 0;
            this.kambariai = 0;
            this.plotas = 0;
            this.kaina = 0;
            this.telNr = "-";
        }

        public Butas(int n, double p, int ksk, double kaina, string tel)
        {
            this.nr = n;
            this.kambariai = ksk;
            this.plotas = p;
            this.kaina = kaina;
            this.telNr = tel;
        }

        public double ImtiKaina()
        {
            return kaina;
        }
        public int ImtiKambariuSkaiciu()
        {
            return kambariai;
        }

        public override string ToString()
        {
            string eilute;
            eilute = string.Format("|{0, 4}  {1, 6} {2, 8} {3, 15} {4, 16}|",
            nr, plotas, kambariai, kaina, telNr);
            return eilute;
        }

    }
    class Daugiaaukstis
    {
        private int n;
        private Butas[] Butai;

        public Daugiaaukstis()
        {
            n = 0;
            Butai = new Butas[100];
        }

        public Butas Imti(int i)
        {
            return Butai[i];
        }

        public double ImtiKaina(int j)
        {
            return Butai[j].ImtiKaina();
        }
        public int ImtiKambariuSkaiciu(int j)
        {
            return Butai[j].ImtiKambariuSkaiciu();
        }

        public int Imti()
        {
            return n;
        }
        public void Deti(Butas but)
        {
            Butai[n++] = but;
        }


    }
    internal class Program
    {
        const int Ca = 9;
        const int Cb = 3;
        const string CFd = "..\\..\\Butai.txt";
        const string CFrez = "..\\..\\Rezultatai.txt";
        static void Main(string[] args)
        {
            if (File.Exists(CFrez))
            {
                File.Delete(CFrez);
            }

            Daugiaaukstis[] aukstas = new Daugiaaukstis[Ca];
            Daugiaaukstis TinkamiButai = new Daugiaaukstis();

            Console.WriteLine("+-----------------Pradiniai duomenys-------------------+");
            Console.WriteLine("| Nr.   Plotas  kambariu sk.      kaina         numeris|");
            Console.WriteLine("+------------------------------------------------------+");
            Skaitymas(CFd, ref aukstas);
            Spausdinimas(aukstas);
            Console.WriteLine("+------------------------------------------------------+\n");

            int riba = 50000;
            int kambSK = 3;

            Console.WriteLine("Keliu kambariu buto ieskote?");
            kambSK = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Kokia maksimali buto kaina?");
            riba = int.Parse(Console.ReadLine());

            Console.WriteLine("+-------{0} kamb. butai nesiekiantys {1} eur.----------+", kambSK, riba);
            Console.WriteLine("| Nr.   Plotas  kambariu sk.      kaina         numeris|");
            Console.WriteLine("+------------------------------------------------------+");
            Tinkamumas(aukstas, TinkamiButai, riba, kambSK);
            TinkamuSpausdinimas(TinkamiButai);
            Console.WriteLine("+------------------------------------------------------+\n");

            Console.WriteLine("Programa baige darba!\n");
            Console.ReadLine();

        }
        static void Skaitymas(string failas, ref Daugiaaukstis[] aukstas)
        {
            int nr, ksk;
            double p, kaina;
            string tel;

            string line;
            using (StreamReader reader = new StreamReader(failas))
            {
                for (int i = 0; i < Ca; i++)
                {
                    aukstas[i] = new Daugiaaukstis();
                    for (int j = 0; j < Cb; j++)
                    {
                        line = reader.ReadLine();
                        string[] parts = line.Split(';');
                        nr = int.Parse(parts[0]);
                        p = double.Parse(parts[1]);
                        ksk = int.Parse(parts[2]);
                        kaina = double.Parse(parts[3]);
                        tel = parts[4];
                        Butas but = new Butas(nr, p, ksk, kaina, tel);
                        aukstas[i].Deti(but);
                    }
                }
            }
        }
        static void Spausdinimas(Daugiaaukstis[] aukstas)
        {
            for (int i = 0; i < Ca; i++)
            {
                for (int j = 0; j < Cb; j++)
                {
                    Console.WriteLine(aukstas[i].Imti(j));
                }
            }
        }
        static void Tinkamumas(Daugiaaukstis[] A, Daugiaaukstis T, int riba, int kSK)
        {
            for (int i = 0; i < Ca; i++)
            {
                for (int j = 0; j < Cb; j++)
                {
                    if (A[i].ImtiKaina(j) <= riba && A[i].ImtiKambariuSkaiciu(j) == kSK)
                    {
                        T.Deti(A[i].Imti(j));
                    }
                }
            }
        }
        static void TinkamuSpausdinimas(Daugiaaukstis T)
        {
            for (int i = 0; i < T.Imti(); i++)
            {
                Console.WriteLine(T.Imti(i).ToString());
            }
        }
    }
}
