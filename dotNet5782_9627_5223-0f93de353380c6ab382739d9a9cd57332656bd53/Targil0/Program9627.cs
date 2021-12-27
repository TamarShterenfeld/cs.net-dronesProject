using System;

namespace Targil0
{
    partial class Program
    {
        static void Main()
        {
            Welcome9627();
            Welcome5223();
            Console.ReadKey();
        }

        private  static void Welcome9627()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }

        static partial void Welcome5223();
    }
}
