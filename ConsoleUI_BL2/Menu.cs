using System;
using System.Collections.Generic;
using System.Text;
using static System.Environment;
using IBL;
using IBL.BO;
using static ConsoleUI_BL.Program;



namespace ConsoleUI_BL
{
    public class Menu 
    {
        BL bl;
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
                                inputIntValue(ref options);
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
    /// <summary>
    /// Navigates the add choice appropriate to the user's choice.
    /// </summary>
    public class AddOption : ISubNavigate
    {
        int innerChoice = 0;
        int id=0 , droneId = 0, baseStationId = 0;
        string name = " " , customerId = " ", phone = " ", model = " ", senderId = " ", targetId = " ";
        double battery = 0, longitude = 0, latitude = 0;
        int chrgeSlots = 0;
        Location location = null;
        //all the enum type litteral are entered as string type
        //and then checked if they contain an enum name.
        string maxWeight = "", weight = "", priority = "";     
        public void options(ref IBL.BL bl)
        {

            Console.WriteLine("Please enter : \n1- For Base Station \n2- For Drone\n3- For Customer\n4- For Parcel ");
            if (int.TryParse(Console.ReadLine(), out innerChoice))
            {
                switch (innerChoice)
                {
                    case (int)AddOptions.BaseStation:
                        {
                            CheckBaseStationDetails(ref id, ref name, ref location, ref chrgeSlots);
                            BaseStation baseStation = new BaseStation(id, name, location, chrgeSlots, null);
                            bl.Add(baseStation);
                            break;
                        }

                    case (int)AddOptions.Drone:
                        {
                            CheckDroneDetails(ref id, ref model, ref maxWeight, ref baseStationId);
                            Drone drone = new Drone();
                            bl.AddDrone(id, maxWeight, baseStationId);
                            break;
                        }
                    case (int)AddOptions.Customer:
                        {
                            CheckCustomerDetails(ref customerId, ref name,ref phone, ref location);
                            Customer customer = new Customer(customerId, name,  phone,  location,null,null);
                            bl.Add(customer);
                            break;
                        }
                    case (int)AddOptions.Parcel:
                        {
                            CheckParcelDetails(ref senderId, ref targetId, ref weight, ref priority);
                            Parcel parcel = new Parcel()
                            AddParcel(id, senderId, targetId, droneId, weight, priority);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("ERROR! \nan unknown option");
                            break;
                        }
                }
            }
            else Console.WriteLine("The add option must hold a numeric value!");
        }
    }

    /// <summary>
    /// Navigates the upDate choice appropriate to the customer's choice.
    /// </summary>
    public class UpDateOption : ISubNavigate
    {
        int innerChoice;
        int parcelId = 0, droneId = 0, baseStationId = 0;
        string senderId = "", targetId = "";
        public void options(ref IBL.BL bl)
        {
            Console.WriteLine("Please enter : \n1- For associating parcel\n2- For picking up parcel\n3- For supply parcel\n4- For charging drone\n5- For stop drone charging ");
            if (int.TryParse(Console.ReadLine(), out innerChoice))
            {
                switch (innerChoice)
                {
                    case (int)UpDateOptions.AssociatingParcel:
                        {
                            Console.WriteLine("Please enter the parcel's id and the drone's id");
                            InputAssociatedParcelDetails(ref parcelId, ref droneId);
                            AssociateParcel(parcelId, droneId);
                            break;
                        }
                    case (int)UpDateOptions.PickingUpParcel:
                        {
                            Console.WriteLine("Please enter the parcel's id and the sender's id");
                            InputPickUpParcelDetails(ref parcelId, ref senderId);
                            PickUpParcel(parcelId, senderId);
                            break;
                        }
                    case (int)UpDateOptions.SupplyingParcel:
                        {
                            Console.WriteLine("Please enter the parcel's id and the target's id");
                            InputArrivalDetails(ref parcelId, ref targetId);
                            SupplyParcel(parcelId, targetId);
                            break;
                        }
                    case (int)UpDateOptions.ChargingDrone:
                        {
                            Console.WriteLine("Please enter droneId and baseStationId");
                            ChargingDrone(droneId, baseStationId);
                            break;
                        }
                    case (int)UpDateOptions.StopDroneCharging:
                        {
                            Console.WriteLine("Please enter droneId and baseStationId");
                            StopDroneCharging(droneId);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("ERROR! \nan unknown option");
                            break;
                        }
                }
            }
            else Console.WriteLine("The update option must hold a numeric value!");
        }
    }



    /// <summary>
    /// Navigates the display choice appropriate to the customer's choice.
    /// </summary>
    public class DisplayOption : ISubNavigate
    {
        int innerChoice;
        int parcelId = 0, droneId = 0, baseStationId = 0;
        string customerId = "";
        public void options(ref IBL.BL bl)
        {
            Console.WriteLine("Please enter: \n1- For a base station\n2- For a drone\n3- For a customer\n4- For a parcel");
            if (int.TryParse(Console.ReadLine(), out innerChoice))
            {
                switch (innerChoice)
                {
                    case (int)DisplayOptions.BaseStation:
                        {
                            Console.WriteLine("Please enter baseStationId");
                            inputIntValue(ref baseStationId);
                            DisplayBaseStation(ref baseStationId);
                            break;
                        }
                    case (int)DisplayOptions.Drone:
                        {
                            Console.WriteLine("Please enter droneId");
                            inputIntValue(ref droneId);
                            DisplayDrone(ref droneId);
                            break;
                        }
                    case (int)DisplayOptions.Customer:
                        {
                            Console.WriteLine("Please enter customerId");
                            inputStringId(ref customerId);
                            DisplayCustomer(ref customerId);
                            break;
                        }
                    case (int)DisplayOptions.Parcel:
                        {
                            Console.WriteLine("Please enter parcelId");
                            inputIntValue(ref parcelId);
                            DisplayParcel(ref parcelId);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("ERROR! \nan unknown option.");
                            break;
                        }
                }
            }
            else Console.WriteLine("The display option must hold a numeric value!");
        }
    }


    /// <summary>
    /// Navigates the showList choice appropriate to the customer's choice.
    /// </summary>
    public class ShowListOption : ISubNavigate
    {
        int innerChoice;
        public void options(ref IBL.BL bl) 
        { 
            Console.WriteLine("Please enter: \n1- For base stations list\n2- For drones list\n3- For customers list\n4- For parcels list\n5- For not associated parcels list\n6 - For base stations with available charge slots");
            if (int.TryParse(Console.ReadLine(), out innerChoice))
            {
                switch (innerChoice)
                {
                    case (int)ShowingListsOptions.BaseStations:
                        {
                            //ShowBaseStationsList();
                            break;
                        }
                    case (int)ShowingListsOptions.Drones:
                        {
                            //ShowDronesList();
                            break;
                        }
                    case (int)ShowingListsOptions.Customers:
                        {
                            //ShowCustomersList();
                            break;
                        }
                    case (int)ShowingListsOptions.Parcels:
                        {
                            //ShowParcelsList();
                            break;
                        }
                    case (int)ShowingListsOptions.NotAssociatedParcels:
                        {
                            //ShowNotAssociatedParcelsList();
                            break;
                        }
                    case (int)ShowingListsOptions.AvailableChargeSlots:
                        {
                            //AvailableChargeSlots();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("ERROR! \nan unknown option.");
                            break;
                        }
                }
            }
            else Console.WriteLine("The show list option must hold a numeric value!");
        }
    }
}


