using System;

namespace IBL
{
    interface IBL: IBaseStationBL, ICustomerBL, IDroneBL, IParcelBL
    {

    }
    interface IBaseStationBL
    {
        void Add(BO.BaseStation baseStation);
        //void Update(int id, BO.BaseStation baseStation);
        //BO.BaseStation Get(int id);
    }

    interface ICustomerBL
    {
        //void Add(BO.BaseStation baseStation);
        //void Update(int id, BO.BaseStation baseStation);
        //BO.BaseStation Get(int id);
    }

    interface IDroneBL
    {
        //void Add(BO.BaseStation baseStation);
        //void Update(int id, BO.BaseStation baseStation);
        //BO.BaseStation Get(int id);
    }
    interface IParcelBL
    {
        //void Add(BO.BaseStation baseStation);
        //void Update(int id, BO.BaseStation baseStation);
        //BO.BaseStation Get(int id);
    }


}
