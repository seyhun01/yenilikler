using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Class1
    {
        static void Main(string[] args)
        {
            Solution solution = new Solution();
            solution.ReserveSen();
            Solution2 solution2 = new Solution2();
            solution2.LastWord();
            Solution1 solution1 = new Solution1();
            solution1.LastWordSize();

        }
        #region task2
        class Solution
        {
            public string[] Sentence = { "world", "hello", "salam" };

            public void ReserveSen()
            {
                string[] sentences = new string[Sentence.Length];
                for (int i = 0; i < Sentence.Length; i++)
                {
                    sentences[i] = Sentence[Sentence.Length - 1 - i];


                }
                foreach (string item in sentences)
                {
                    Console.WriteLine(item);
                }


            }
        }


        #endregion
        #region Task3

        class Solution2
        {
            string[] Sentence = { "hello", "world", "space" };


            public void LastWord()
            {
                string[] Word = new string[1];
                for (int i = 0; i < Sentence.Length; i++)
                {
                    Word[0] = Sentence[Sentence.Length - 1];

                }
                Word[0].ToCharArray();
                char[] ch = Word[0].ToCharArray();
                foreach (char item in ch)
                {
                    Console.WriteLine(item);
                }



            }



        }
        #endregion
        #region Task1
        class Solution1
        {
            string[] Sentence = { "hello", "world", "space" };


            public void LastWordSize()
            {
                string[] Word = new string[1];
                for (int i = 0; i < Sentence.Length; i++)
                {
                    Word[0] = Sentence[Sentence.Length - 1];

                }
                Word[0].ToCharArray();
                char[] ch = Word[0].ToCharArray();


                Console.WriteLine($"The last word is:{Word[0]}- its length is {ch.Length}");

            }
        }
        #endregion
    }
}

