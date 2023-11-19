using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_Main_Methods5
{
    class MasterCard : BankCard
    {
        public void Cash()
        {
            Console.WriteLine($"Odenis 500 manat");
        }

        public void CheckBalance()
        {
            Console.WriteLine($"Balasiniz 300 manat");
        }
        public void WithoutComission()
        {
            Console.WriteLine("Bu kartla komissiyasiz nagd pul elde ede bilersiz");
        }
    }
}
