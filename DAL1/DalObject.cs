using System;
using System.Collections.Generic;
using System.Text;
using IDal.DO;
using static IDal.IDal;
using static DalObject.DataSource;
using static IDal.DO.OverloadException;
using System.Linq;
using System.Reflection;


namespace DalObject
{
    /// <summary>
    ///the class DalObject contains all the needed methods 
    ///which are connected to the data (in DataSource class) of the program.
    /// </summary>
    public partial class DalObject : IDal.IDal
    {
        // constructor
        public DalObject()
        {
            Initialize();
        }
  
        public static int IncreaseParcelIndex()
        {
            return ++Config.ParcelId;
        }

       
        
    }

    
}



