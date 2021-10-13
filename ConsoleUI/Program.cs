using System;

namespace ConsoleUI
{
    public enum Options
    {
        Add = 1, UpDate, Display, ShowingLists, Exit
    }
    class Program
    {
        static void Main()
        {
            int option; bool valid = false;
            option = int.Parse(Console.ReadLine());
            while (!valid)
                switch (option)
                {
                    case (int)Options.Add:
                        {
                            valid = true;
                            break;
                        }

                    case (int)Options.UpDate:
                        {
                            valid = true;
                            break;
                        }


                    case (int)Options.Display:
                        {
                            valid = true;
                            break;
                        }


                    case (int)Options.ShowingLists:
                        {
                            valid = true;
                            break;
                        }

                    case (int)Options.Exit:
                        {
                            valid = true;
                            break;
                        }
                    default:
                        {

                            Console.WriteLine("ERROR! \nan unknown option, please try again.");
                            option = int.Parse(Console.ReadLine());
                            break;
                        }
                }
        }
    }
}

