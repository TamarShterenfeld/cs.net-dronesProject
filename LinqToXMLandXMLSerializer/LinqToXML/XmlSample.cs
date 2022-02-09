using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace LinqToXML
{
    class XmlSample
    {
        XElement StudentRoot;
        string StudentPath = @"StudentXml.xml";

        public XmlSample()
        {
            if (!File.Exists(StudentPath))
                CreateFiles();
            else
                LoadData();
        }

        private void CreateFiles()
        {
            StudentRoot = new XElement("Students");
            StudentRoot.Save(StudentPath);
        }

        private void LoadData()
        {
            try
            {
                StudentRoot = XElement.Load(StudentPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }

        public void SaveStudentList(List<Student> StudentList)
        {
            StudentRoot = new XElement("Students");

            foreach (Student item in StudentList)
            {
                //XElement id = new XElement("id", item.Id);
                //XElement firstName = new XElement("firstName", item.FirstName);
                //XElement lastName = new XElement("lastName", item.LastName);

                //XElement name = new XElement("name", firstName, lastName);
                //XElement Student = new XElement("Student", id, name);

                //StudentRoot.Add(Student);

                AddStudent(item);
            }
           
            StudentRoot.Save(StudentPath);
        }

        public void SaveStudentListLinq(List<Student> StudentList)
        {
            //XElement StudentRoot;

            var v =   from p in StudentList
            select new XElement("Student",
                                        new XElement("id", p.Id),
                                       new XElement("name",
                                            new XElement("firstName", p.FirstName),
                                            new XElement("lastName", p.LastName)
                                            )
                                        );

            StudentRoot = new XElement("Students",v);
                                                                    
            StudentRoot.Save(StudentPath);
        }

        public List<Student> GetStudentList()
        {
            LoadData();
            List<Student> Students;
            try
            {
                Students = (from p in StudentRoot.Elements()
                            select new Student()
                            {
                                Id = Convert.ToInt32(p.Element("id").Value),
                                FirstName = p.Element("name").Element("firstName").Value,
                                LastName = p.Element("name").Element("lastName").Value
                            }).ToList();
            }
            catch
            {
                Students = null;
            }
            return Students;
        }

        public Student GetStudent(int id)
        {
            LoadData();
            Student Student;
            try
            {
                Student = (from p in StudentRoot.Elements()
                           where Convert.ToInt32(p.Element("id").Value) == id
                           select new Student()
                           {
                               Id = Convert.ToInt32(p.Element("id").Value),
                               FirstName = p.Element("name").Element("firstName").Value,
                               LastName = p.Element("name").Element("lastName").Value
                           }).FirstOrDefault();
            }
            catch
            {
                Student = null;
            }
            return Student;
        }

        public string GetStudentName(int id)
        {
            LoadData();
            string StudentName;
            try
            {
                StudentName = (from p in StudentRoot.Elements()
                               where Convert.ToInt32(p.Element("id").Value) == id
                               select p.Element("name").Element("firstName").Value
                               + " " +
                                  p.Element("name").Element("lastName").Value
                                  ).FirstOrDefault();
            }
            catch
            {
                StudentName = null;
            }
            return StudentName;
        }

        public void AddStudent(Student Student)
        {
            XElement id = new XElement("id", Student.Id);
            XElement firstName = new XElement("firstName", Student.FirstName);
            XElement lastName = new XElement("lastName", Student.LastName);
            XElement name = new XElement("name", firstName, lastName);

            XElement st = new XElement("Student", id, name);

            StudentRoot.Add(st);
            StudentRoot.Save(StudentPath);
        }

        public bool RemoveStudent(int id)
        {
            XElement StudentElement;
            try
            {
                StudentElement = (from p in StudentRoot.Elements()
                                  where Convert.ToInt32(p.Element("id").Value) == id
                                  select p).FirstOrDefault();
                StudentElement.Remove();
                StudentRoot.Save(StudentPath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void UpdateStudent(Student Student)
        {
            XElement StudentElement = (from p in StudentRoot.Elements()
                                       where Convert.ToInt32(p.Element("id").Value) == Student.Id
                                       select p).FirstOrDefault();

            StudentElement.Element("name").Element("firstName").Value = Student.FirstName;
            StudentElement.Element("name").Element("lastName").Value = Student.LastName;

            StudentRoot.Save(StudentPath);
        }





    }
}
