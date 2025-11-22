using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
//--------------------------------------------------------------------------------------------------------
/*U4 - 2.Mobiliojo ryšio kortelės
Norėdamas palyginti mobiliojo ryšio operatorių siūlomas išankstinio mokėjimo korteles Sirvydas
surinko šią informaciją į tekstinį failą. Faile eilutėmis yra kortelių duomenys: kortelės(tinklo)
pavadinimas, pradinė suma kortelėje, tarifas savame tinkle, tarifas į kitus tinklus, SMS žinučių tarifas
savame tinkle ir į kitus tinklus. Parašykite programą, kuri spausdintų kortelių duomenis lentele, surastų
kortelę, kurios SMS žinučių tarifai į kitus tinklus mažiausi. Papildykite programą veiksmais, kurie leistų
atrinkti korteles, kurios leidžia skambinti ir siųsti SMS žinutes savame tinkle nemokamai, ir šį sąrašą
surikiuoti pagal pradinę sumą mažėjimo tvarka ir kortelės pavadinimą abėcėliškai.*/
//--------------------------------------------------------------------------------------------------------
namespace MGTM_32_Antanas_Dickus_L3
{
    /// <summary>
    /// Klase aprašanti korteles duomenis
    /// </summary>
    class Kortele
    {
        private string pavadinimas;//korteles pavadinimas
        private int suma;//korteleje esanti pinigu suma
        private double tarifas_sav,// skambuciu tarifas savame tinkle
            tarifas_sve, //skambuciu tarifas svetimame tinkle
            sms_sav, //SMS tarifas savame tinkle
            sms_sve; //SMS tarifas svetimame tinkle


        /// <summary>
        /// klases "korteles" konstruktorius be parametru
        /// </summary>
        public Kortele()
        {
            this.pavadinimas = "Pavadinimas nepateiktas";
            this.suma = 0;
            this.tarifas_sav = 0;
            this.tarifas_sve = 0;
            this.sms_sav = 0;
            this.sms_sve = 0;
        }
        /// <summary>
        /// klases "korteles" konstruktorius su parametrais
        /// </summary>
        /// <param name="pavadinimas"></param>
        /// <param name="suma"></param>
        /// <param name="tarifas_sav"></param>
        /// <param name="tarifas_sve"></param>
        /// <param name="sms_sav"></param>
        /// <param name="sms_sve"></param>
        public Kortele(string pavadinimas, int suma,
            double tarifas_sav, double tarifas_sve,
            double sms_sav, double sms_sve)
        {
            this.pavadinimas = pavadinimas;
            this.suma = suma;
            this.tarifas_sav = tarifas_sav;
            this.tarifas_sve = tarifas_sve;
            this.sms_sav = sms_sav;
            this.sms_sve = sms_sve;
        }
        /// <summary>
        /// metodas, sukuriantis lentele
        /// korteles duomenims
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string eilute;
            eilute = string.Format("|{0, -17}|{1, -17}|{2, -17}|" +
                 "{3, -19}|{4, -18}|{5, -21}|",
                pavadinimas, suma,
                tarifas_sav, tarifas_sve,
                sms_sav, sms_sve);
            return eilute;
        }
        /// <summary>
        /// metodai, grazinantys korteles duomenis
        /// </summary>
        public double saviems_tarifas() { return tarifas_sav; }
        public double saviems_sms() { return sms_sav; }
        public double svetimiems() { return sms_sve + tarifas_sve; }
        public string Pavadinimas() { return pavadinimas; }
        public static bool operator <=(Kortele st1, Kortele st2)
        {
            int p = String.Compare(st1.pavadinimas,
                st2.pavadinimas,
            StringComparison.CurrentCulture);
            int s = st1.suma.CompareTo(st2.suma);
            return (s < 0 || (s == 0 && p > 0));
        }
        public static bool operator >=(Kortele st1, Kortele st2)
        {
            int p = String.Compare(st1.pavadinimas,
                st2.pavadinimas,
            StringComparison.CurrentCulture);
            int s = st2.suma.CompareTo(st1.suma);
            return (s > 0 || (s == 0 && p < 0));
        }
        public override bool Equals(object obj)
        {
            return obj is Kortele kortele &&
                   pavadinimas == kortele.pavadinimas &&
                   suma == kortele.suma &&
                   tarifas_sav == kortele.tarifas_sav &&
                   tarifas_sve == kortele.tarifas_sve &&
                   sms_sav == kortele.sms_sav &&
                   sms_sve == kortele.sms_sve;
        }
        public override int GetHashCode()
        {
            int hashCode = 612615005;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(pavadinimas);
            hashCode = hashCode * -1521134295 + suma.GetHashCode();
            hashCode = hashCode * -1521134295 + tarifas_sav.GetHashCode();
            hashCode = hashCode * -1521134295 + tarifas_sve.GetHashCode();
            hashCode = hashCode * -1521134295 + sms_sav.GetHashCode();
            hashCode = hashCode * -1521134295 + sms_sve.GetHashCode();
            return hashCode;
        }
    }
    /// <summary>
    /// konteineris, saugantis korteles duomenis
    /// </summary>
    class operatorius
    {
        const int Cn = 100; // studentų masyvo dydis
        private Kortele[] Korteles; // studentų objektų masyvas
        private int kiek; // studentų skaičius

        /// <summary>
        /// konstruktorius pe parametru
        /// </summary>
        public operatorius()
        {
            kiek = 0;
            Korteles = new Kortele[Cn];
        }
        /// <summary>
        /// metodas, grazinantis i-tąją kortele
        /// </summary>
        /// <param name="i"></param>
        /// <returns> kortele </returns>
        public Kortele ImtiKortele(int i)
        { return Korteles[i]; }
        /// <summary>
        /// metodas, grazinantis korteliu kieki
        /// </summary>
        /// <returns></returns>
        public int ImtiKiek()
        { return kiek; }
        /// <summary>
        /// metodas, idedantis nauja kortele
        /// i oepratoriaus n-tąją vieta
        /// </summary>
        /// <param name="kt"></param>
        public void DetiKortele(Kortele kt)
        { Korteles[kiek++] = kt; }
        /// <summary>
        /// metodas, pakeiciantis korteliu
        /// indeksus masyve
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        public void Rikiavimas()
        {
            for (int i = 0; i < kiek; i++)
            {
                Kortele min = Korteles[i];
                int im = i;
                for (int j = i + 1; j < kiek; j++)
                {
                    if (min <= Korteles[j])
                    {
                        min = Korteles[j];
                        im = j;
                        Korteles[im] = Korteles[i];
                        Korteles[i] = min;
                    }
                }
            }
        }
        public Kortele pigiausia()
        {
            Kortele Pigiausia = Korteles[0];
            for (int i = 1; i < kiek; i++)
            {
                if (Korteles[i].svetimiems() <
                    Pigiausia.svetimiems())
                {
                    Pigiausia = Korteles[i];
                }
            }
            return Pigiausia;
        }

    }
    internal class Program
    {//programoje naudojamos konstantos:
        const int Cn = 100;
        const string CFd1 = "..//..//Korteles.txt";
        const string CFr = "..//..//Rezultatai.txt";
        static void Main(string[] args)
        {
            /*
             * TestasMas - operatorius, saugantis pradinius duomenis
             * Nemokamos - operatorius, saugantis korteles,
             kurios leidzia nemokamai
             susisiekti su tame paciame
             tinkle esanciais numeriais
              */
            operatorius TestasMas = new operatorius();
            operatorius Nemokamos = new operatorius();

            string[] Pigiausios = new string[Cn];
            int pigios_kiekis = 0;
            Skaityti(CFd1, TestasMas);

            if (File.Exists(CFr)) File.Delete(CFr);

            Spausdinti(CFr, TestasMas,
                "Pradinis korteliu sarasas:",
                "Nera pradiniu duomenu");


            formuoti_pigiausias(TestasMas,
                ref Pigiausios, out pigios_kiekis);
            spaudinimas_pigiausiu(pigios_kiekis,
                Pigiausios, CFr);

            nemokamos_savame(TestasMas, Nemokamos);
            Nemokamos.Rikiavimas();

            Spausdinti(CFr, Nemokamos,
                "Korteles, leidziancios nemokamai" +
                " bendrauti savame tinkle:",
                "Nera korteliu, kurios leistu" +
                " nemokamai bendrauti savame tinkle");

        }
        /// <summary>
        /// metodas, nuskaitantis duomenis is pradinio failo
        /// </summary>
        /// <param name="fv"></param>
        /// <param name="kort"></param>
        static void Skaityti(string fv, operatorius kort)
        {
            using (StreamReader reader = new StreamReader(fv))
            {
                string line;
                while (reader.Peek() > -1)
                {
                    line = reader.ReadLine();
                    string[] parts = line.Split(' ');
                    string pav = parts[0];
                    int sum = Convert.ToInt32(parts[1]);
                    double tar_sav = double.Parse(parts[2]);
                    double tar_sve = double.Parse(parts[3]);
                    double sms_sav = double.Parse(parts[4]);
                    double sms_sve = double.Parse(parts[5]);
                    Kortele kortele = new Kortele(pav, sum,
                        tar_sav, tar_sve, sms_sav, sms_sve);
                    kort.DetiKortele(kortele);
                }
            }
        }
        /// <summary>
        /// metodas, spausdinantis lentele
        /// </summary>
        /// <param name="fv"></param>
        /// <param name="kort"></param>
        /// <param name="antraste"></param>
        /// <param name="antraste2"></param>
        static void Spausdinti(string fv,
            operatorius kort,
            string antraste,
            string antraste2)
        {
            const string virsus =
                "---------------------------" +
                "-------------------------------------" +
                "-------------------------------------" +
                "------------------\n" +
                "   Nr|Pavadinimas      |Suma           " +
                "  |Tarifas tinkle   |Tarifas ne tikle  " +
                " |SMS tarifas tinkle|SMS tarifas ne tinkle|\n" +
                "---------------------------" +
                "-------------------------------------" +
                "-------------------------------------" +
                "------------------";
            using (var fr = File.AppendText(fv))
            {
                if (kort.ImtiKiek() > 0)
                {
                    fr.WriteLine(antraste);
                    fr.WriteLine(virsus);
                    for (int i = 0; i < kort.ImtiKiek(); i++)
                    {
                        Kortele kt = kort.ImtiKortele(i);
                        fr.WriteLine("{0, 4:d} {1}", i + 1,
                            kort.ImtiKortele(i).ToString());
                    }
                    fr.WriteLine("---------------------------" +
                    "-------------------------------------" +
                    "-------------------------------------" +
                    "------------------");
                }
                else fr.WriteLine(antraste2);

            }
        }
        /// <summary>
        /// metodas, formauojantis pigiausia
        /// tarifa svetimiems turinciu korteliu sarasa
        /// </summary>
        /// <param name="op"></param>
        /// <param name="pigios"></param>
        /// <param name="n"></param>
        static void formuoti_pigiausias(operatorius op,
            ref string[] pigios, out int n)
        {
            n = 0;
            for (int i = 0; i < op.ImtiKiek(); i++)
            {
                if (op.ImtiKortele(i).svetimiems() ==
                    op.pigiausia().svetimiems())
                {
                    pigios[n] = op.ImtiKortele(i).Pavadinimas();
                    n++;
                }
            }
        }
        static void spaudinimas_pigiausiu(int n,
            string[] pigios, string fv)
        {
            using (var fr = File.AppendText(fv))
            {
                fr.Write("Pigiausias tarifa " +
                    "svetimiems tinklams turi:");
                for (int i = 0; i < n; i++)
                {
                    fr.Write(" {0} ", pigios[i]);
                }
                fr.Write('\n');
            }
        }
        /// <summary>
        /// metodas, sukuriantis nemokama tarifa
        /// savame tinkle turinciu korteliu sarasa
        /// </summary>
        /// <param name="op"></param>
        /// <param name="nem"></param>
        static void nemokamos_savame(operatorius op,
            operatorius nem)
        {
            for (int i = 0; i < op.ImtiKiek(); i++)
            {
                if (op.ImtiKortele(i).saviems_tarifas() == 0 &&
                    op.ImtiKortele(i).saviems_sms() == 0)
                {
                    nem.DetiKortele(op.ImtiKortele(i));
                }
            }
        }
    }
}