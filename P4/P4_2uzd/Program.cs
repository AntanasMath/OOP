using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace P4_2uzd
{
    class Mokinys
    {
        private string pavarde,
                       vardas,
                       grupe;
        private int pazymiuSK;
        private int[] pazymys;

        public Mokinys(string pav, string vard, string grupe, int pazSK, int[] paz)
        {
            this.pavarde = pav;
            this.vardas = vard;
            this.grupe = grupe;
            this.pazymiuSK = pazSK;
            this.pazymys = paz;
        }

        public Mokinys()
        {
            this.pavarde = "-";
            this.vardas = "-";
            this.pazymiuSK = 0;
        }

        public int ImtiPazymiuSkaiciu()
        {
            return pazymiuSK;
        }

        public int[] ImtiPazymius()
        {
            return pazymys;
        }

        public string ImtiGrupe()
        {
            return grupe;
        }

        public override string ToString()
        {
            string eilute;
            eilute = string.Format("|{0, 15}  {1, 10} {2, 10} {3, 3}  ",
            pavarde, vardas, grupe, pazymiuSK);
            for (int i = 0; i < pazymiuSK; i++)
            {
                eilute += string.Format("{0, 3}", pazymys[i]);
            }
            return eilute;
        }

    }
    class Fakultetas
    {
        private string fakultetas;
        private int n;
        private Mokinys[] mokiniai;

        public Fakultetas()
        {
            n = 0;
            mokiniai = new Mokinys[100];
        }

        public Fakultetas(string pav)
        {
            n = 0;
            mokiniai = new Mokinys[100];
            this.fakultetas = pav;
        }

        public string ImtiPavadinima()
        {
            return fakultetas;
        }

        public Mokinys Imti(int i)
        {
            return mokiniai[i];
        }

        public int Imti()
        {
            return n;
        }
        public void Deti(Mokinys mok)
        {
            mokiniai[n++] = mok;
        }

        public int ImtiPazymiuSkaiciu(int j)
        {
            return mokiniai[j].ImtiPazymiuSkaiciu();
        }

        public int[] ImtiPazymius(int j)
        {
            return mokiniai[j].ImtiPazymius();
        }

        public void Keisti(int i, int j)//swap()
        {
            Mokinys laikinas = new Mokinys();
            laikinas = mokiniai[j];
            mokiniai[j] = mokiniai[i];
            mokiniai[i] = laikinas;
        }

        public string ImtiGrupe(int i)
        {
            return mokiniai[i].ImtiGrupe();
        }

    }
    internal class Program
    {
        const string CFd = "..\\..\\TextFile1.txt";
        static void Main(string[] args)
        {
            Fakultetas fakas = new Fakultetas();

            Skaitymas(ref fakas, CFd);
            Console.WriteLine(fakas.ImtiPavadinima());

            Console.WriteLine("+----------------Pradiniai duomenys-----------------------");
            Console.WriteLine("|      Pavarde        vardas    grupe    paz.sk. pazymiai");
            Console.WriteLine("+---------------------------------------------------------");
            Spausdinimas(fakas);
            Console.WriteLine("+---------------------------------------------------------");

            Rikiavimas(fakas);

            Console.WriteLine("+----------------Surikiuoti duomenys-----------------------");
            Console.WriteLine("|      Pavarde        vardas    grupe    paz.sk. pazymiai");
            Console.WriteLine("+---------------------------------------------------------");
            Spausdinimas(fakas);
            Console.WriteLine("+---------------------------------------------------------");
            Console.ReadLine();
        }
        static void Skaitymas(ref Fakultetas F, string failas)
        {
            string p,
                   v,
                   gr;
            int pazSK;
            string fakPav;
            using (StreamReader skaitymas = new StreamReader(failas))
            {
                string line;
                fakPav = skaitymas.ReadLine();
                F = new Fakultetas(fakPav);
                while ((line = skaitymas.ReadLine()) != null)
                {
                    string[] dalys;
                    dalys = line.Split(' ');
                    p = dalys[0];
                    v = dalys[1];
                    gr = dalys[2];
                    pazSK = int.Parse(dalys[3]);
                    int[] paz = new int[pazSK];
                    for (int i = 0; i < pazSK; i++)
                    {
                        paz[i] = int.Parse(dalys[4 + i]);
                    }
                    Mokinys mok = new Mokinys(p, v, gr, pazSK, paz);
                    F.Deti(mok);
                }
            }

        }
        static void Spausdinimas(Fakultetas F)
        {
            for (int i = 0; i < F.Imti(); i++)
            {
                Console.WriteLine(F.Imti(i).ToString());
            }
        }
        static void Rikiavimas(Fakultetas F)
        {
            for (int i = 0; i < F.Imti(); i++)
            {
                for (int j = i + 1; j < F.Imti(); j++)
                {
                    int gr = string.Compare(F.ImtiGrupe(i), F.ImtiGrupe(j));

                    if (Vidurkis(F, i) < Vidurkis(F, j))
                    {
                        F.Keisti(i, j);
                    }
                    else if (Vidurkis(F, i) == Vidurkis(F, j) && gr > 0)
                    {
                        F.Keisti(i, j);
                    }
                }
            }
        }
        static double Vidurkis(Fakultetas F, int a)
        {
            int suma = 0;
            int pazSK = F.ImtiPazymiuSkaiciu(a);
            int[] paz = F.ImtiPazymius(a);
            for (int i = 0; i < pazSK; i++)
            {
                suma += paz[i];
            }

            return suma / pazSK;
        }
    }
}