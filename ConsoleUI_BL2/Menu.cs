using System;
using System.Collections.Generic;
using System.Text;
using static System.Environment;
using IBL;
using IBL.BO;


namespace ConsoleUI_BL
{


    public class Menu
    {
        BL bl = new BL();
        AddOption add;
        UpDateOption upDate;
        DisplayOption display;
        ShowListOption show;
        public Menu()
        {
            bl = new BL();
            add = new AddOption();
            upDate = new UpDateOption();
            display = new DisplayOption();
            show = new ShowListOption();
            Navigate();
        }
        /// <summary>
        /// naviget the first chice - the kind of action the customer want to do.
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
                                add.options(ref bl);
                                break;
                            }

                        case (int)Options.UpDate:
                            {
                                upDate.options(ref bl);
                                break;
                            }
                        case (int)Options.Display:
                            {
                                display.options(ref bl);
                                break;
                            }
                        case (int)Options.ShowingLists:
                            {
                                show.options(ref bl);
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
                                Program.inputIntValue(out options);
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






