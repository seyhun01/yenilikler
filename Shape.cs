using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_Main_Methods5
{
  
     class Shape:Rectangle
    {
        public int Pi = 3;
        public int Radius;

       

        public override void RecArea()
        {
            Console.WriteLine($"dordbucagin sahesi-{A*B}");
        }
       


    }
    class Circle : Shape
    {
        public Circle(int radius,int pi)
        {
            Radius = radius;
            Pi = pi;
        }
        public void CirArea()
        {
            Console.WriteLine($"Dairenin sahesi-{Radius*Radius*Pi}");
               
        }


    }
   


}
