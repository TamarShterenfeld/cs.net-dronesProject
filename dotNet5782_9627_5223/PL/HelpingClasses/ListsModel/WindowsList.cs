using BO;
using Singleton;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using static PL.PO.POConverter;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace PL
{
    sealed partial class ListsModel 
    {
        public static List<Window> OpenWindows = new();
       
    }
}
