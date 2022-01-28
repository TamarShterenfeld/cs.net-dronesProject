using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using Singleton;

namespace DalXml
{
    /// <summary>
    ///the class DalXml contains all the needed methods 
    ///which are connected to the data (in xml files) of the program.
    /// </summary>
    sealed partial class DalXml : Singleton<DalXml>, DalApi.IDal
    {
        // a constructor
        DalXml()
        {
            //Initialize();
        }
    }
}