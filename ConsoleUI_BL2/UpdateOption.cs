using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleUI_BL.Program;
using IBL;
using IDal.DO;
using IBL.BO;
using DAL.DO;

namespace ConsoleUI_BL
{/// <summary>
 /// Navigates the upDate choice appropriate to the customer's choice.
 /// </summary>
    public class UpDateOption : ISubNavigate
    {

        /// <inheritdoc />
        public void Options(ref BL bl)
        {
            int id = 0;
            double timeCharge = 0;
            string model = null, num = null, customerId;
            Console.WriteLine("Please enter : \n1- For updating a drone \n2- For updating a basetation \n3- For updating a customer \n4- For charging drone \n5- For stop drone charging \n6- For associating parcel \n7- For picking up parcel \n8- For supply parcel \n");
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
                catch (DateTimeException exe)
                {
                    Console.WriteLine("the DateTime: "+ exe.MyDateTime+" isn't valid!");
                }

                catch (ActionException exe)
                {
                    Console.WriteLine("the Action: " + exe.Action + " wasn't succeeded!");
                }
                catch(BatteryException exe)
                {
                    Console.WriteLine("the Battery: "+ exe.Battery+ " isn't valid!");
                }
                catch(DroneStatusException exe)
                {
                    Console.WriteLine("the Status: " + exe.Status + " isn't valid!");
                }
                catch(ParcelStatusException exe)
                {
                    Console.WriteLine("the Status: "+ exe.ParcelStatus+" isn't valid");
                }

                catch(ChargeSlotsException exe)
                {
                    Console.WriteLine("The ChargeSlots: "+exe.ChargeSlots+" isn't available!");
                }
                catch(IntIdException exe)
                {
                    Console.WriteLine("The id: "+ exe.Id+" isn't valid!");
                }
                catch (LocationException exe)
                {
                    Console.WriteLine("The Location: " + exe.Location + " isn't valid!");
                }
                catch (StringException exe)
                {
                    Console.WriteLine("The string : " + exe.Str + " isn't valid!");
                }
            }
            else Console.WriteLine("The update option must hold a numeric value!");
        }
    }

}
