using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace csharp_Main_Methods5
{
    class Program
    {


        static void Main(string[] args)
        {
            Circle circle = new Circle(3,4);
            circle.CirArea();
            Shape shape = new Shape();
            shape.RecArea();
            VisaCard visacard = new VisaCard();
            visacard.CheckBalance();
            visacard.ForeignCurrency();
            visacard.Cash();
            MasterCard mastercard =new MasterCard();
            mastercard.CheckBalance();
            mastercard.WithoutComission();
            mastercard.Cash();


        }



    }
}
        

        
    
    








        








    



    

    





