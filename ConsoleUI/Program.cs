using System;

namespace ConsoleUI
{
    public enum Options
    {
        Add = 1, UpDate, Display, ShowingLists, Exit
    }

    public enum AddingOptions
    {
        AddingBaseStation = 1, AddingDrone, AddingCustomer, AddingParcel
    }
    public partial class Program
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
                            int addingOption;
                            addingOption = int.Parse(Console.ReadLine());

                            int id = 0;
                            string name = ""; 
                            double longitude= 0 , latitude = 0;
                            int chrgeSlots = 0;

                            switch (addingOption)
                            {
                                case (int)AddingOptions.AddingBaseStation:
                                    {
                                        BaseStationDetails(ref id, ref name ,ref longitude, ref latitude,ref chrgeSlots);
                                        break;
                                    }

                                case (int)AddingOptions.AddingDrone:
                                    {
                                        break;
                                    }
                                case (int)AddingOptions.AddingCustomer:
                                    {
                                        break;
                                    }
                                case (int)AddingOptions.AddingParcel:
                                    {
                                        break;
                                    }
                                default:
                                    {
                                        Console.WriteLine("ERROR! \nan unknown option, please try again.");
                                        break;
                                    }
                                    
                            }
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

