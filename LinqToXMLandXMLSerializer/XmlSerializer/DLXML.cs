using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DS;

namespace XmlSeralizerSample
{
    class DLXML : IDL
    {
        #region Student
        
        string studentFilePath = @"StudentsList.xml";
        string courseFilePath = @"CoursesList.xml";
        string studentInCourseFilePath = @"StudentInCourseList.xml";

        public DLXML()
        {
            //if (!File.Exists(studentFilePath))
            //    DL.XMLTools.SaveListToXMLSerializer<DO.Student>(DS.DataSource.studentList, studentFilePath);
            
            //if (!File.Exists(courseFilePath))
            //    DL.XMLTools.SaveListToXMLSerializer<DO.Course>(DS.DataSource.courseList, courseFilePath);

            //if (!File.Exists(studentInCourseFilePath))
            //    DL.XMLTools.SaveListToXMLSerializer<DO.StudentInCourse>(DS.DataSource.studentInCourseList, studentInCourseFilePath);

        }
        public void AddStudent(DO.Student stu)
        {
            List<DO.Student> studentList = DL.XMLTools.LoadListFromXMLSerializer<DO.Student>(studentFilePath);

            DO.Student stu1 = studentList.FirstOrDefault(t => t.StudentId == stu.StudentId);
            
            if (stu1 != null)
            {
                //throw new SomeException("DL: Student with the same id already exists...");
            }
            
            studentList.Add(stu);
            
            DL.XMLTools.SaveListToXMLSerializer<DO.Student>(studentList, studentFilePath);
        }
        public void DeleteStudent(int id)
        {
            List<DO.Student> studentList = DL.XMLTools.LoadListFromXMLSerializer<DO.Student>(studentFilePath);

            DO.Student stu1 = GetStudent(id);
            
            if (stu1 == null)
            {
                //throw new SomeException("DL: Student with the same id not found...");
            }

            List<DO.StudentInCourse> studentInCourseList = DL.XMLTools.LoadListFromXMLSerializer<DO.StudentInCourse>(studentInCourseFilePath);

            if (studentInCourseList.Exists(s => s.StudentId == id))
            {
                //    throw new SomeException("DL: cannot delete student, courses for this student still exist !!!");
            }

            studentList.Remove(stu1);

            DL.XMLTools.SaveListToXMLSerializer<DO.Student>(studentList, studentFilePath);            
        }
        public void UpdateStudent(DO.Student stu)
        {
            List<DO.Student> studentList = DL.XMLTools.LoadListFromXMLSerializer<DO.Student>(studentFilePath);

            int index = studentList.FindIndex(t => t.StudentId == stu.StudentId);
            
            if (index == -1)
                throw new Exception("DAL: Student with the same id not found...");

            studentList[index] = stu;

            DL.XMLTools.SaveListToXMLSerializer<DO.Student>(studentList, studentFilePath);
        }
        public DO.Student GetStudent(int id)
        {
            List<DO.Student> studentList = DL.XMLTools.LoadListFromXMLSerializer<DO.Student>(studentFilePath);
            int index = studentList.FindIndex(t => t.StudentId == id);
            if (index == -1)
                throw new Exception("DAL: Student with the same id not found...");
            return studentList.FirstOrDefault(t => t.StudentId == id);
        }
        public IEnumerable<DO.Student> GetAllStudents(Func<DO.Student, bool> predicat = null)
        {
            List<DO.Student> studentList = DL.XMLTools.LoadListFromXMLSerializer<DO.Student>(studentFilePath);
            var v = from item in studentList
                    select item; //item.Clone();

            if (predicat == null)
                return v.AsEnumerable().OrderByDescending(s => s.StudentId);

            return v.Where(predicat).OrderByDescending(s => s.StudentId);
        }


        #endregion
    }
}
