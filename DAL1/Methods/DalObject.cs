﻿using System;
using System.Collections.Generic;
using System.Text;
using IDal.DO;
using static IDal.IDal;
using static DalObject.DataSource;
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
        // a constructor
        public DalObject()
        {
            Initialize();
        }         
  
    }

    
}



