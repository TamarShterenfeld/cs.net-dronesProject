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
        private static readonly string dirPath = @"xml\";
        static XMLTools()
        {
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
        }
        #region SaveLoadWithXElement
        public static void SaveListToXmlElement<T>(List<T> list, string filePath)
        {
            FileStream file = new FileStream(dirPath + filePath, FileMode.Create);
            XmlSerializer x = new XmlSerializer(list.GetType());
            x.Serialize(file, list);
            file.Close();
        }

        public static List<T> LoadListFromXmlElement<T>(string filePath)
        {
            try
            {
                if (File.Exists(dirPath + filePath))
                {
                    
                }
                else
                {
                    return new List<T>();
                }
            }
            catch (Exception ex)
            {
                throw ;
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
            }
        }
        #endregion

        #region SaveLoadWithXMLSerializer
        public static void SaveListToXmlSerializer<T>(IEnumerable<T> list, string filePath)
        {
            try
            {
                FileStream file = new FileStream(dirPath + filePath, FileMode.Create);
                XmlSerializer x = new XmlSerializer(list.GetType());

                x.Serialize(file, list);
                file.Close();
            }
            catch (Exception ex)
            {
               // throw new DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
            }
        }
        public static List<T> LoadListFromXmlSerializer<T>(string filePath)
        {
            //try
            //{
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
                    return new List<T>();
            //}
            //catch (Exception ex)
            //{
            //   //throw new DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
            //}
        }
        #endregion
    }
}
