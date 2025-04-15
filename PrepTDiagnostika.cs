using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace pokus
{
    internal class PrepTDiagnostika: I_Observer
    {
        private int N;
        private List<List<int>> matice;
        public void Observe(List<List<int>> KopieMatice)
        {
         this.matice = KopieMatice;
         this.N = KopieMatice.Count;
        }
        public int CountT()
        {
                  int[] array = new int[this.N];
                   for (int i = 0; i < this.N; i++)
                   {
                       int count = 0;
                       for (int j = 0; j < this.N; j++)
                       {
                        if (j ==i)
                           {
                              continue;
                           }


                        else if (matice[j][i] == 1)
                           {
                               count++;
                           }
                       }
                       array[i] = count;
                   }
                   Array.Sort(array);
                   if (array[0] > this.CountTMax())
                   {
                       return this.CountTMax();
                   }
                   else
                   {
                       return array[0];
                   }


        }     
        public int CountTMax()
        {
            return (this.N - 1)/2;
        }
        public int CountL()
        {
            return ((this.N * (this.N - 1)) / 2);
        }

    }
    
    
}
