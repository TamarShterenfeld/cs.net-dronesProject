using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
    }

    public class StudentInCourse
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }        
        public float Grade { get; set; }
    }

    public enum Gender { male, female }

    public class Student
    {
        public int StudentId { get; set; }
        public string PersonName { get; set; }
        public bool IsMarried { get; set; }
        public DateTime PersonDate { get; set; }
        public Gender StudentGender { get; set; }
    }
}
