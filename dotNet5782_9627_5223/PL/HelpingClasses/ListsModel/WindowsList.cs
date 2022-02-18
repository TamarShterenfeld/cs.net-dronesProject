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
        public event EventHandler Refresh;

        public void RefreshAll()
        {
            if (Refresh != null)
            {
                foreach (EventHandler item in Refresh.GetInvocationList())
                {
                    item.Invoke(null,EventArgs.Empty);
                }
            }

        }
    }



}
