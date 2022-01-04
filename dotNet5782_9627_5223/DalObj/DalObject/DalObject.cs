using System;
using System.Collections.Generic;
using System.Text;
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
    sealed public partial class DalObject : Singleton<DalObject>, DalApi.IDal
    {
        // a constructor
        DalObject()
        {
            Initialize();
        }
    }
}



