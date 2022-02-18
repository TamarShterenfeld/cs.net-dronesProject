using System;
using System.Collections.Generic;
using System.Text;
using IDal.DO;
using static IDal.IDal;
using static DalObject.DataSource;
using System.Linq;
using System.Reflection;
using Singleton;

namespace DalObject
{
    /// <summary>
    ///the class DalObject contains all the needed methods 
    ///which are connected to the data (in DataSource class) of the program.
    /// </summary>
    sealed partial class DalObject : Singleton<DalObject>,IDal.IDal
    {
        // a constructor
        public DalObject()
        {
            Initialize();
        }         
  
    }

    
}



