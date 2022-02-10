using DO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;


namespace Dal
{
    class XMLTools
    {
        public static XElement CustomersRoot;
        public static string CustomerPath = @"Customers.xml";
        private static readonly string dirPath = @"xml\";

        //a constructor which construct also files
        //of XmlSeriakizer and files of XElement.
        public XMLTools()
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
        void CreateFiles()
        {
            CustomersRoot = new XElement("Customers");
            CustomersRoot.Save(dirPath + CustomerPath);
        }

        public static void LoadData()
        {
            try
            {
                CustomersRoot = XElement.Load(dirPath + CustomerPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }

        public static void SaveStudentListLinq(List<Customer> customers)
        {
            //XElement StudentRoot;

            var v = from c in customers
                    select new XElement("Customer",
                                                new XElement("id", c.Id),
                                               new XElement("name",c.Name),
                                               new XElement("phone", c.Phone),
                                               new XElement("longitude", c.Longitude),
                                               new XElement("latitude", c.Latitude)
                                                );

            CustomersRoot = new XElement("Customers", v);

            CustomersRoot.Save(dirPath + CustomerPath);
        }
        #endregion
    }
}
