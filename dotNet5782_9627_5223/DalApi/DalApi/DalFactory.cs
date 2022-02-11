﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using DO;

namespace DalApi
{
    public static class DalFactory
    {
        public static IDal GetDal()
        {
            string dalType = DalConfig.DalName;
            string dalPkg = DalConfig.DalPackages[dalType];
            if (dalPkg == null) throw new DalConfigException($"Package {dalType} is not found in packages list in dal-config.xml");
            string[] Files = Directory.GetFiles(Environment.CurrentDirectory);
            try
            {
                Assembly.LoadFrom($"{dalPkg}.dll");
            }
            catch (Exception e) { throw new DalConfigException($"Failed to load the {dalPkg}.dll file"); }
            Type type = Type.GetType($"{dalPkg}.{dalPkg}, {dalPkg}");
            if (type == null) throw new DalConfigException($"Class {dalPkg} was not found in the {dalPkg}.dll");
            IDal dal = (IDal)type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static| BindingFlags.FlattenHierarchy).GetValue(null);
            if (dal == null) throw new DalConfigException($"Class {dalPkg} is not a singleton or wrong propertry name for Instance");
            return dal;
        }
    }
}

