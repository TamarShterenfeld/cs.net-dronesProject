using System;
using static IDAL.DO.IDAL;
using IDAL.DO;
using static DalObject.DalObject;



namespace ConsoleUI
{
    public enum Options
    {
        Add = 1, UpDate, Display, ShowingLists, Exit
    }

    public enum AddOptions
    {
        BaseStation = 1, Drone, Customer, Package,
    }

    public enum UpDateOptions
    {
        AssociatingParcel = 1, PickingUpParcel, SupplyParcel, ChargingDrone, StopDroneCharging,
    }

    public enum DisplayOptions
    {
        BaseStation = 1, Drone, Customer, Package,
    }

    public enum ShowingListsOptions
    {
        BaseStations, Drones, Customers, Packages, NotAssociatedPackages, AvailableChrgeSlots,
    }
    public partial class Program
    {
        static void Main()
        {
            int option;
            int innerChoice;
            Parcel parcel = new Parcel();
            Drone drone = new Drone();
            DroneCharge droneCharge = new DroneCharge();
            BaseStation baseStation = new BaseStation();

            int id = 0, droneId = 0;
            string name = "", customerId = "", phone = "", model = "", senderId = "", targetId = "";
            double longitude = 0, latitude = 0, battery = 0;
            int chrgeSlots = 0;
            DroneStatuses status = 0;
            WeightCategories maxWeight = 0;
            WeightCategories Weight = 0;
            Priorities Priority = 0;
            DateTime Production = new DateTime(), Association = new DateTime(), PickingUp = new DateTime(), Arrival = new DateTime();


            Console.WriteLine("Please enter : \n1- For add\n2- For update\n3- For display\n4- For showing the lists\n5- For exit");

            while (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Please enter a digit! Try again!");
            }

            while (true)
            {
                switch (option)
                {
                    case (int)Options.Add:
                        {
                            if (int.TryParse(Console.ReadLine(), out innerChoice))
                            {
                                switch (innerChoice)
                                {
                                    case (int)AddOptions.BaseStation:
                                        {
                                            BaseStationDetails(ref id, ref name, ref longitude, ref latitude, ref chrgeSlots);
                                            CreatingBaseStation(id, name, longitude, latitude, chrgeSlots);
                                            break;
                                        }

                                    case (int)AddOptions.Drone:
                                        {
                                            //
                                            CreatingDrone(id, model, status, maxWeight, battery);
                                            break;
                                        }
                                    case (int)AddOptions.Customer:
                                        {
                                            CustomerDetails(ref customerId, ref name, ref phone, ref longitude, ref latitude);
                                            CreatingCustomer(ref customerId, ref name, ref phone, ref longitude, ref latitude);
                                            break;
                                        }
                                    case (int)AddOptions.Package:
                                        {
                                            //
                                            CreatingParcel(id, senderId, targetId, droneId, Weight, Priority, Production, Association, PickingUp, Arrival);
                                            break;
                                        }
                                    default:
                                        {
                                            Console.WriteLine("ERROR! \nan unknown option");
                                            break;
                                        }

                                }
                            }
                            break;
                        }

                    case (int)Options.UpDate:
                        {
                            InputDetailsOfDrone(ref drone);
                            InputDetailsOfParcel(ref parcel);
                            Console.WriteLine("Please enter : \n1- For associating package\n2- For picking up package\n3- For supply package\n4- For charging drone\n5- For stop drone charging ");
                            if (int.TryParse(Console.ReadLine(), out innerChoice))
                            {
                                switch (innerChoice)
                                {
                                    case (int)UpDateOptions.AssociatingParcel:
                                        {
                                            AssociatingParcel(ref parcel, ref drone);
                                            break;
                                        }
                                    case (int)UpDateOptions.PickingUpParcel:
                                        {
                                            PickingUpParcel(ref parcel);
                                            break;
                                        }
                                    case (int)UpDateOptions.SupplyParcel:
                                        {
                                            SupplyParcel(ref parcel);
                                            break;
                                        }
                                    case (int)UpDateOptions.ChargingDrone:
                                        {
                                            ChargingDrone(ref drone, ref baseStation, ref droneCharge);
                                            break;
                                        }
                                    case (int)UpDateOptions.StopDroneCharging:
                                        {
                                            StopDroneCharging(ref drone, ref baseStation);
                                            break;
                                        }
                                }
                            }
                            break;
                        }

                    case (int)Options.Display:
                        {
                            if (int.TryParse(Console.ReadLine(), out innerChoice))
                            {
                                switch (innerChoice)
                                {
                                    case (int)DisplayOptions.BaseStation:
                                        {
                                            if (int.TryParse(Console.ReadLine(), out id))
                                            {
                                                DisplayBaseStation(SearchBaseStation(id));
                                            }
                                            else Console.WriteLine("Id must contain  only digits");
                                            break;
                                        }

                                    case (int)DisplayOptions.Drone:
                                        {
                                            if (int.TryParse(Console.ReadLine(), out id))
                                            {
                                                DisplayDrone(SearchDrone(id));
                                            }
                                            else Console.WriteLine("Id must contain  only digits");
                                            break;
                                        }
                                    case (int)DisplayOptions.Customer:
                                        {
                                            break;
                                        }
                                    case (int)DisplayOptions.Package:
                                        {
                                            if (int.TryParse(Console.ReadLine(), out id))
                                            {
                                               // DisplayParcel(SearchParcel(id));
                                            }
                                            else Console.WriteLine("Id must contain  only digits");
                                            break;
                                        }
                                    default:
                                        {
                                            Console.WriteLine("ERROR! \nan unknown option.");
                                            break;
                                        }

                                }
                            }
                            break;
                        }


                    case (int)Options.ShowingLists:
                        { 
                            break;
                        }

                    case (int)Options.Exit:
                        {
                            System.Environment.Exit(0);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("ERROR! \nan unknown option, please try again.");
                            while (!int.TryParse(Console.ReadLine(), out option))
                            {
                                Console.WriteLine("Please enter a digit! Try again!");
                            }
                            break;
                        }
                }
            }
        }
    }
}

