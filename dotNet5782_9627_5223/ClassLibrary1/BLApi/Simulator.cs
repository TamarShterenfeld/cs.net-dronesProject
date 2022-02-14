using System;
using BO;
using System.Threading;
using DalApi;
using System.Linq;
using static System.Math;

namespace IBL
{
    internal class Simulator
    {
        //enum Maintenance { Starting, Going, Charging }
        //enum BatteryUsage { Available, Light, Medium, Heavy, Charging }
        //private const double VELOCITY = 1.0;
        //private const int DELAY = 500;
        //private const double TIME_STEP = DELAY / 1000.0;
        //private const double STEP = VELOCITY / TIME_STEP;

        public Simulator(BLApi.IBL Bl, int droneId, Action updateDrone, Func<bool> checkStop)
        {
            BLApi.IBL bl = Bl;
        //    var dal = bl.GetMyDal();
        //    var drone = bl.GetDroneForList(droneId);
        //    int? parcelId = null;
        //    int? baseStationId = null;
        //    BaseStation station = null;
        //    double distance = 0.0;
        //    int batteryUsage = 0;
        //    DO.Parcel? parcel = null;
        //    bool pickedUp = false;
        //    Customer customer = null;
        //    Maintenance maintenance = Maintenance.Starting;

        //    void initDelivery(int id)///////////////////////////////
        //    {
        //        parcel = dal.GetParcel(id);
        //        batteryUsage = (int)Enum.Parse(typeof(BatteryUsage), parcel?.Weight.ToString());
        //        pickedUp = parcel?.PickUpDate is not null;
        //        customer = bl. GetBLCustomer((pickedUp? parcel?.TargetId : parcel?.SenderId));
        //    }

        //    do
        //    {
        //        //(var next, var id) = drone.nextAction(bl);

        //        switch (drone)
        //        {
        //            case DroneForList { Status: DroneStatuses.Available }:
        //                if (!sleepDelayTime()) break;

        //                lock (bl)
        //                {
        //                    parcelId = bl.GetBOParcelsList().Select(p => p?.AssociationDate == null
        //                                                      && (WeightCategories)(p?.Weight) <= drone.MaxWeight
        //                                                      && drone.RequiredBattery(bl, (int)p?.Id) < drone.Battery)//מתודב שמחשבת כמה בטריה צריך כדי לבצע את המשלוח 
        //                                     .OrderByDescending(p => p?.Priority)
        //                                     .ThenByDescending(p => p?.Weight)
        //                                     .FirstOrDefault()?.Id;
        //                    switch (parcelId, drone.Battery)
        //                    {
        //                        case (null, 1.0):
        //                            break;

        //                        case (null, _):
        //                            baseStationId = bl.FindClosestBaseStation(drone, charge: true)?.Id;// מתודה שמוצאת תחנה קרובה ביותר ( כדי להטען ) כמובן
        //                            if (baseStationId != null)
        //                            {
        //                                drone.Status = DroneStatuses.Maintenance;
        //                                maintenance = Maintenance.Starting;
        //                                dal.BaseStationDroneIn((int)baseStationId);// מתודה שמחזירה באיזה תחנה הרחפן נמצא
        //                                dal.AddDroneCharge(droneId, (int)baseStationId);// הכנסת רחפן לטעינה
        //                            }
        //                            break;
        //                        case (_, _):
        //                            try
        //                            {
        //                                dal.ParcelSchedule((int)parcelId, droneId);//מתודת שיוך חבילה לרחפן
        //                                drone.DeliveryId = parcelId;
        //                                initDelivery((int)parcelId);
        //                                drone.Status = DroneStatuses.Delivery;
        //                            }
        //                            catch (DO.ExistIdException ex) { throw new BadStatusException("Internal error getting parcel", ex); }
        //                            break;
        //                    }
        //                }
        //                break;

        //            case DroneForList { Status: DroneStatuses.Maintenance }:
        //                switch (maintenance)
        //                {
        //                    case Maintenance.Starting:
        //                        lock (bl)
        //                        {
        //                            try { station = bl.GetBaseStation(baseStationId ?? dal.GetDroneChargeBaseStationId(drone.Id)); }
        //                            catch (DO.ExistIdException ex) { throw new BadStatusException("Internal error base station", ex); }
        //                            distance = drone.Distance(station);
        //                            maintenance = Maintenance.Going;
        //                        }
        //                        break;

        //                    case Maintenance.Going:
        //                        if (distance < 0.01)
        //                            lock (bl)
        //                            {
        //                                drone.Location = station.Location;
        //                                maintenance = Maintenance.Charging;
        //                            }
        //                        else
        //                        {
        //                            if (!sleepDelayTime()) break;
        //                            lock (bl)
        //                            {
        //                                double delta = distance < STEP ? distance : STEP;
        //                                distance -= delta;
        //                                drone.Battery = Max(0.0, drone.Battery - delta * bl.BatteryUsages[DRONE_FREE]);
        //                            }
        //                        }
        //                        break;

        //                    case Maintenance.Charging:
        //                        if (drone.Battery == 1.0)
        //                            lock (bl)
        //                            {
        //                                drone.Status = DroneStatuses.Available;
        //                                dal.DeleteDroneCharge(droneId);
        //                                dal.BaseStationDroneOut(station.Id);
        //                            }
        //                        else
        //                        {
        //                            if (!sleepDelayTime()) break;
        //                            lock (bl) drone.Battery = Min(1.0, drone.Battery + bl.BatteryUsages[DRONE_CHARGE] * TIME_STEP);
        //                        }
        //                        break;
        //                    default:
        //                        throw new BadStatusException("Internal error: wrong maintenance substate");
        //                }
        //                break;

        //            case DroneForList { Status: DroneStatuses.Delivery }:
        //                lock (bl)
        //                {
        //                    try { if (parcelId == null) initDelivery((int)drone.DeliveryId); }
        //                    catch (DO.ExistIdException ex) { throw new BadStatusException("Internal error getting parcel", ex); }
        //                    distance = drone.Distance(customer);
        //                }

        //                if (distance < 0.01 || drone.Battery == 0.0)
        //                    lock (bl)
        //                    {
        //                        drone.Location = customer.Location;
        //                        if (pickedUp)
        //                        {
        //                            dal.ParcelDelivery((int)parcel?.Id);
        //                            drone.Status = DroneStatuses.Available;

        //                        }
        //                        else
        //                        {
        //                            dal.ParcelPickup((int)parcel?.Id);
        //                            customer = bl.GetCustomer((int)parcel?.TargetId);
        //                            pickedUp = true;
        //                        }
        //                    }
        //                else
        //                {
        //                    if (!sleepDelayTime()) break;
        //                    lock (bl)
        //                    {
        //                        double delta = distance < STEP ? distance : STEP;
        //                        double proportion = delta / distance;
        //                        drone.Battery = Max(0.0, drone.Battery - delta * bl.BatteryUsages[pickedUp ? batteryUsage : DRONE_FREE]);
        //                        double lat = drone.Location.Latitude + (customer.Location.Latitude - drone.Location.Latitude) * proportion;
        //                        double lon = drone.Location.Longitude + (customer.Location.Longitude - drone.Location.Longitude) * proportion;
        //                        drone.Location = new() { Latitude = lat, Longitude = lon };
        //                    }
        //                }
        //                break;

        //            default:
        //                throw new BadStatusException("Internal error: not available after Delivery...");

        //        }
        //        updateDrone();
        //    } while (!checkStop());
        }

        //private static bool sleepDelayTime()
        //{
        //    try { Thread.Sleep(DELAY); } catch (ThreadInterruptedException) { return false; }
        //    return true;
        //}
        //const int timer = 1000;//a second is allocated for every pause stop.
        //const double speed = 5000;//speed of 1,000 km per a second.
        //public Simulator(IBL.BL bl, int droneId, Action refreshDisplay, Func<bool> checkStopping)
        //{

        //}

        //void func( object sender, EventArgs e)
        //{

        //}
    }
}

