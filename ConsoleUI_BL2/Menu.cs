using System;
using System.Collections.Generic;
using System.Text;
using static System.Environment;
using IBL;
using IBL.BO;


namespace ConsoleUI_BL
{

    /// <summary>
    /// the class has four different fields .
    /// each field is an object of a different class.
    /// each class takes care of the needed actions it has to provide.
    /// those objects are initialized in the constructor.
    /// in the constuctor also the function Navigate() is called -
    /// which naviates the first choice of the customer.
    /// </summary>
    public class Menu
    {
        BL bl;
        readonly AddOption add;
        readonly UpDateOption upDate;
        readonly DisplayOption display;
        readonly ShowListOption show;
        public Menu()
        {
            bl = new IBL.BL();
            add = new();
            upDate = new();
            display = new();
            show = new();
            Navigate();
        }
        /// <summary>
        /// naviget the first chice - the kind of action the customer wants to do.
        /// for each option there's a special object which navigates the customer's inner choice.
        /// </summary>
        private void Navigate()
        {
            int options;
            while (true)
            {
                Console.WriteLine("Please enter : \n1- For add\n2- For update\n3- For display\n4- For showing the lists\n5- For exit");
                while (!int.TryParse(Console.ReadLine(), out options))
                {
                    Console.WriteLine("Please enter only a digit! Try again!");
                }
                try
                {
                    switch (options)
                    {
                        case (int)Options.Add:
                            {
                                add.Options(ref bl);
                                break;
                            }

                        case (int)Options.UpDate:
                            {
                                upDate.Options(ref bl);
                                break;
                            }
                        case (int)Options.Display:
                            {
                                display.Options(ref bl);
                                break;
                            }
                        case (int)Options.ShowLists:
                            {
                                show.Options(ref bl);
                                break;
                            }
                        case (int)Options.Exit:
                            {
                                Exit(0);
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("ERROR! \nAn unknown option, Please try again.");
                               options = Program.InputIntValue();
                                break;
                            }
                    }
                }
               
                catch (Exception exe)
                {
                    Console.WriteLine(exe.Message + "\nTry again from the beginning!");
                }


            }
        }

    }

}






