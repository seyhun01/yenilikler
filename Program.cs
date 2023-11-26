using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Fruit apple = new Apple();
            Fruit pineapple = new Pineapple();
            Fruit orenge = new Orange();
            Fruit[] fruits = { apple, pineapple, orenge };
            foreach (Fruit  Basket in fruits)
            {
                Basket.ValueMin();
                Basket.Taste();


            }

        }
    }
    abstract class Fruit
    {
        public int Price;
        public string Sort;
        public abstract void Taste();
        public abstract void ValueMin();
    }
    class Apple : Fruit
    {
        public int VitaminA = 23;
        public int VitaminB = 34;
        public override void Taste()
        {
            Console.WriteLine("Taste as apple");
        }
        public override void ValueMin()
        {
            Console.WriteLine(VitaminB+" " +VitaminA);
        }
    }
    class Pineapple : Fruit
    {
        public int VitaminD = 20;
        public int VitaminE = 73;

        public override void Taste()
        {
            Console.WriteLine("Taste as pineapple");
        }
        public override void ValueMin()
        {
            Console.WriteLine(VitaminE+" " +VitaminD);
        }
    }
    class Orange : Fruit
    {
        public int VitaminC = 27;
        public override void Taste()
        {
            Console.WriteLine("Taste as orange");
        }
        public override  void ValueMin()
        {
            Console.WriteLine(VitaminC);

        }
    }
}
