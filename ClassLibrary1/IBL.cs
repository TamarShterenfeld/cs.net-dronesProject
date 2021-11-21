using System;
using System.Collections.Generic;
using System.Linq;
using IBL.BO;


namespace IBL
{
    interface IBL: IBaseStationBL, ICustomerBL, IDroneBL, IParcelBL
    {

    }
    interface IBaseStationBL
    {
        void Add(BO.BaseStation baseStation);
        BO.BaseStation GetBLBaseStation(int id);
        //IEnumerable <DroneInCharging> GetDronesInMe(int stationId); 
        //void Update(int id, BO.BaseStation baseStation);
        //BO.BaseStation Get(int id);
    }

    interface ICustomerBL
    {
        void Add(BO.Customer baseStation);
        //void Update(int id, BO.BaseStation baseStation);
        //BO.BaseStation Get(int id);
    }

    interface IDroneBL
    {
        void Add(BO.Drone baseStation , int baseStationId);
        //void Update(int id, BO.BaseStation baseStation);
        //BO.BaseStation Get(int id);
    }
    interface IParcelBL
    {
        void Add(BO.Parcel baseStation);
        //void Update(int id, BO.BaseStation baseStation);
        //BO.BaseStation Get(int id);
    }


}
