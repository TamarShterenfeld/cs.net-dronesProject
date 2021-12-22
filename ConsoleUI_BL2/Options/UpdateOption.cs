using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleUI_BL.Program;
using IBL;
using BO;
using DO;

namespace ConsoleUI_BL
{/// <summary>
 /// Navigates the upDate choice appropriate to the customer's choice.
 /// </summary>
    public class UpDateOption : ISubNavigate
    {
        public void Options(ref BL bl)
        {
            int id;
            double timeCharge;
            string model = null, num = null, customerId;
            Console.WriteLine("Please enter : \n1- For updating a drone \n2- For updating a basetation \n3- For updating a customer \n4- For charging drone \n5- For stop drone charging \n6- For associating parcel \n7- For picking up parcel \n8- For supply parcel");
            if (int.TryParse(Console.ReadLine(), out int innerChoice))
            {
                try
                {
                    switch (innerChoice)
                    {
                        case (int)UpDateOptions.UpdateDrone:
                            {

                                Console.WriteLine("Please enter droneId and a new model for the drone");
                                id = InputIntValue();
                                model = InputOptionalStringValue();
                                bl.UpdateDrone(id, model);

                                break;
                            }
                        case (int)UpDateOptions.UpdateBaseStation:
                            {
                                Console.WriteLine("Please enter baseStationId and one or more of the following details: name, number of charge slots");
                                id = InputIntValue();
                                while (model == null && num == null)
                                {
                                    model = InputOptionalStringValue();
                                    num = InputOptionalIntValue();
                                }
                                bl.UpdateBaseStation(id, model, num);

                                break;
                            }
                        case (int)UpDateOptions.UpdateCustomer:
                            {
                                Console.WriteLine("Please enter customerId and one or more of the following details: name, phone");
                                customerId = InputStringId();
                                while (model == null && num == null)
                                {
                                    model = InputOptionalStringValue();
                                    num = InputPhone();
                                }
                                bl.UpdateCustomer(customerId, model, num);
                                break;
                            }
                        case (int)UpDateOptions.ChargeDrone:
                            {
                                Console.WriteLine("Please enter droneId ");
                                id = InputIntValue();
                                bl.SendDroneForCharge(id);
                                break;
                            }
                        case (int)UpDateOptions.StopDroneCharge:
                            {
                                Console.WriteLine("Please enter droneId and timeCharge");
                                id = InputIntValue();
                                timeCharge = InputDoubleValue();
                                bl.ReleaseDroneFromRecharge(id, timeCharge);
                                break;
                            }
                        case (int)UpDateOptions.AssociationParcel:
                            {
                                Console.WriteLine("Please enter the drone id");
                                id = InputIntValue();
                                bl.AssociateParcel(id);
                                break;
                            }
                        case (int)UpDateOptions.PickUpParcel:
                            {
                                Console.WriteLine("Please enter the drone id");
                                id = InputIntValue();
                                bl.PickUpParcel(id);
                                break;
                            }
                        case (int)UpDateOptions.SupplyParcel:
                            {
                                Console.WriteLine("Please enter the drone id");
                                id = InputIntValue();
                                bl.SupplyParcel(id);
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("ERROR! \nan unknown option");
                                break;
                            }
                    }
                }

                //whike making all the calaulations of UpDate options all these exceptions may be thrown:
                catch (IntIdException exe)
                {
                    Console.WriteLine("the IntId: " + exe.Id + " isn't valid in the DAL logic level");
                }
                catch (ChargeSlotsException exe)
                {
                    Console.WriteLine("the chargeSlots: " + exe.ChargeSlots + " isn't valid in the DAL logic level");
                }
                catch (LocationException exe)
                {
                    Console.WriteLine("the Location: " + exe.Location + " isn't valid in the DAL logic level"+ "\nCoordinante value must be in range of - 180º to 180º");
                }
                catch (StringException exe)
                {
                    Console.WriteLine("the String: " + exe.Str + " isn't valid in the DAL logic level");
                }
                catch (StringIdException exe)
                {
                    Console.WriteLine("the Customer Id: " + exe.Id + " isn't valid in the DAL logic level");
                }
                catch (BLPhoneException exe)
                {
                    Console.WriteLine("the Phone: " + exe.Phone + " wasn't succeeded in the BL logic level");
                }
                catch (ParcelActionsException exe)
                {
                    Console.WriteLine("the Action: " + exe.Action + " wasn't succeeded in the BL logic level");
                }
                catch (BLIntIdException exe)
                {
                    Console.WriteLine("The id: " + exe.Id + " isn't valid in the BL logic level ");
                }
                catch (PhoneException exe)
                {
                    Console.WriteLine("the Phone: " + exe.Phone + " wasn't succeeded in the DAL logic level");
                }
                
                catch (BatteryException exe)
                {
                    Console.WriteLine("the Battery: " + exe.Battery + " isn't valid in the BL logic level");
                }
                catch (DroneStatusException exe)
                {
                    Console.WriteLine("the Status: " + exe.Status + " isn't valid  in the BL logic level");
                }
                catch (ParcelStatusException exe)
                {
                    Console.WriteLine("the Status: " + exe.ParcelStatus + " isn't valid  in the BL logic level");
                }

                catch (BLChargeSlotsException exe)
                {
                    Console.WriteLine("The ChargeSlots: " + exe.ChargeSlots + " isn't available in the BL logic level ");
                }
                catch (BLLocationException exe)
                {
                    Console.WriteLine("The Location: " + exe.Location + " isn't valid in the BL logic level" + "\nCoordinante value must be in range of - 180º to 180º");
                }
                catch (BLStringIdException exe)
                {
                    Console.WriteLine("The Customer Id: " + exe.Id + "isn't valid in the BL logic level ");
                }
                catch (AmountOfParcelsException exe)
                {
                    Console.WriteLine("The AmountOfParcels: " + exe.Amount + "isn't valid in the BL logic level ");
                }
                catch (BLStringException exe)
                {
                    Console.WriteLine("The String: " + exe.Str + "isn't valid in the BL logic level ");
                }

            }
            else Console.WriteLine("The update option must hold a numeric value!");
        }
    }

}
