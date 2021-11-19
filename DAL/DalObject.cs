using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;
using static IDAL.IDal;
using static DalObject.DataSource;
using static IDAL.DO.OverloadException;
using System.Linq;

namespace DalObject
{
    /// <summary>
    ///the class DalObject contains all the needed methods 
    ///which are connected to the data (in DataSource class) of the program.
    /// </summary>
    public partial class DalObject : IDAL.IDal
    {
        // constructor
        public DalObject()
        {
            Initialize();
        }
        
    }
    
}



