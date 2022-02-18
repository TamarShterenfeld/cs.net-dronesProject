using BO;
using Singleton;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using static PL.PO.POConverter;
using System.Windows;

namespace PL
{
    sealed partial class ListsModel
    {
        public event EventHandler RefreshStations;
        public void RefreshAll()
        {
            if (RefreshStations != null)
            {
                foreach (EventHandler item in RefreshStations.GetInvocationList())
                {
                    item.Invoke(null,EventArgs.Empty);
                }
            }
           
        }
    }



}
