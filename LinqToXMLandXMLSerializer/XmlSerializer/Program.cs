using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XmlSeralizerSample
{
    class Program
    {
        static void Main(string[] args)
        {
            IDL dl = new DLXML(); //need factory!!

            dl.GetAllStudents();

            //dl.DeleteStudent(1);
            
            //etc...

            Console.ReadLine();
        }
    }
}
