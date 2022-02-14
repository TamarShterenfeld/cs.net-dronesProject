using DO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Runtime.CompilerServices;


namespace  DalXml
{
    public static class XMLTools
    {
        public static XElement CustomersRoot;
        public static XElement ConfigRoot;
        public static string CustomerPath = @"Customers.xml";
        public static string ConfigPath = @"Config.xml";
        private static readonly string dirPath = @"Xml\";

        //a constructor which construct also files
        //of XmlSeriakizer and files of XElement.
         static XMLTools()
        {
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            if (!File.Exists(CustomerPath))
                CreateFiles();
            else
                LoadData();
        }

        #region XmlSerializer
        #region SaveListToXmlSerializer
        public static void SaveListToXmlSerializer<T>(List<T> list, string filePath)
        {
            FileStream file = new FileStream(dirPath + filePath, FileMode.Create);
            XmlSerializer x = new XmlSerializer(list.GetType());
            x.Serialize(file, list);
            file.Close();
        }
        #endregion

        #region LoadListFromXmlSerializer
        public static List<T> LoadListFromXmlSerializer<T>(string filePath)
        {
            try
            {
                if (File.Exists(dirPath + filePath))
                {
                    List<T> list;
                    XmlSerializer x = new XmlSerializer(typeof(List<T>));
                    FileStream file = new FileStream(dirPath + filePath, FileMode.Open);
                    list = (List<T>)x.Deserialize(file);
                    file.Close();
                    return list;
                }
                else
                {
                    return new List<T>();
                }
            }
            catch (Exception ex)
            {
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
            }
        }
        #endregion

        #endregion

        #region LinqToXml
        static void CreateFiles()
        {
            CustomersRoot = new XElement("Customers");
            CustomersRoot.Save(dirPath + CustomerPath);
            ConfigRoot = XElement.Load(dirPath + ConfigPath);
        }

        public static void LoadData()
        {
            try
            {
                CustomersRoot = XElement.Load(dirPath + CustomerPath);
                ConfigRoot = XElement.Load(dirPath + ConfigPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }

        
        #endregion
    }
}
