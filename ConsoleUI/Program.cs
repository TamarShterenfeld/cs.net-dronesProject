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
            int option; bool valid = false;
            int innerChoice;
            Parcel parcel = new Parcel();
            Drone drone = new Drone();
            DroneCharge droneCharge = new DroneCharge();
            BaseStation baseStation = new BaseStation();

            Console.WriteLine("Please enter : \n1- For add\n2- For update\n3- For display\n4- For showing the lists\n5- For exit");

            while (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Please enter a digit! Try again!");
            }

            while (!valid)
            {
                switch (option)
                {
                    case (int)Options.Add:
                        {
                            int addingOption;
                            addingOption = int.Parse(Console.ReadLine());

                            int id = 0, senderId = 0 , targetId = 0, droneId = 0;
                            string name = "", customerId = "" , phone = "", model=""; 
                            double longitude= 0 , latitude = 0, battery = 0;
                            int chrgeSlots = 0;
                            DroneStatuses status = 0;
                            WeightCategories maxWeight = 0;
                            WeightCategories Weight = 0;
                            Priorities Priority = 0;
                            DateTime Production = new DateTime() , Association = new DateTime(), PickingUp = new DateTime() , Arrival = new DateTime();

                            switch (addingOption)
                            {
                                case (int)AddOptions.BaseStation:
                                    {
                                        BaseStationDetails(ref id, ref name ,ref longitude, ref latitude,ref chrgeSlots);
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
                                        Console.WriteLine("ERROR! \nan unknown option, please try again.");
                                        break;
                                    }
                                    
                            }
                                        break;

                        }

                    case (int)Options.UpDate:
                        {
                            InputDetailsOfDrone(ref drone);
                            InputDetailsOfParcel(ref parcel);
                            valid = true;
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

