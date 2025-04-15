using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MathNet.Numerics;

namespace pokus
{
    internal class PravdepodbnostSpravneDiagnostiky : I_Observer
    {
        private int N;
        private List<List<int>> CopyMatice;
        private double PM;
        public PravdepodbnostSpravneDiagnostiky(double PM)
        {

            this.PM = PM;
        }
        public void Observe(List<List<int>> KopieMatice)
        {
            this.CopyMatice = KopieMatice;
            this.N = CopyMatice.Count;

        }

        private int Rekurze(int CharakterCislo, int[] FixniIndex = null)
        {
            if (FixniIndex == null) //impliciti nastaveni delky arraye podle skalaru CharakterCislo z uzivatelskeho vstupu
            {
                // defaultni hodnoty(PocetModulu+1) arraye  musi byt jine nez mozne hodnoty, jinak budou sum cykly nespravne 
                //preskakovat, plus jedna protoze CharakterCislo nemuze byt vetsi nez pocet modulu
                FixniIndex = Enumerable.Repeat(this.N + 1, CharakterCislo).ToArray();
            }
            if (CharakterCislo == 1)
            {
                int sum = 0;
                for (int i = 0; i < this.N; i++)// suma aktualniho "nejmensiho" kola ze vsech k sum
                {
                    if (FixniIndex.Contains(i))
                    { continue; }
                    FixniIndex[FixniIndex.Length - 1] = i;
                    int produkt = 1;
                    for (int j = 0; j < this.N; j++)//produkt, prochazeni vsech sloupcu v matici
                    {
                        bool LogSoucin = false;

                        for (int l = 0; l < FixniIndex.Length; l++)// prochazeni fixnimi indexy z predeslych sum, resp radky 
                                                                   // a provadeni logickeho soucinu
                        {
                            bool LocalBool = false;
                            if (this.CopyMatice[FixniIndex[l]][j] == 1)
                            {
                                LocalBool = true;
                            }

                            LogSoucin = LogSoucin || LocalBool;
                        }
                        if (!LogSoucin)
                        {
                            produkt = 0;
                            break;
                        }
                    }
                    sum += produkt;
                }

                FixniIndex[FixniIndex.Length - CharakterCislo] = this.N + 1;// uvolneni indexu
                return sum;
            }
            else
            {

                int suma = 0;
                for (int i = 0; i < this.N; i++) //suma nadrazenych sum cyklu
                {
                    if (FixniIndex.Contains(i))
                    { continue; }
                    FixniIndex[FixniIndex.Length - CharakterCislo] = i;
                    suma += Rekurze(CharakterCislo - 1, FixniIndex);
                }
                FixniIndex[FixniIndex.Length - CharakterCislo] = this.N + 1;// uvolneni indexove hodnoty pro nadrazene sum cykly
                return suma;
            }
        }
        private int Factorial(int n)
        {
            if (n == 0)
            {
                return 1;
            }
            else
            {
                return n * Factorial(n - 1);
            }
        }
        private double CharakteristickeCislo(int Verze)
        {
            return (1.0 / Factorial(Verze)) * Rekurze(Verze); //otazka zda je nutne pretypovani na double
        }
        public double PravdepodobnostSP() // "chyba" - pokud bude PM = 0, C# ma definovano 0^0=1 takze system nespadne
        {
            double vysledek = 0;
            for (int k = 1; k <= this.N; k++)
            {
                vysledek += Math.Pow((1 - this.PM), k) * (Math.Pow(this.PM, this.N - k)) * CharakteristickeCislo(k);
            }
            return vysledek;
        }
    }
}