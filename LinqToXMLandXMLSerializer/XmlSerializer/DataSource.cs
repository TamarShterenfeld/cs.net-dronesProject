using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS
{
    static class DataSource1
    {
        public static List<DO.Student> studentList;
        public static List<DO.Course> courseList;
        public static List<DO.StudentInCourse> studentInCourseList;

        static DataSource1()
        {
            initLists();

        }
        private static void initLists()
        {
            courseList  = new List<DO.Course>();

            string[] courseNameArray = new string[] { "c#", "java", "מבנה המחשב", "c++", "אינפי", "בדידה", "מבוא לתכנות" };
            for (int i = 0; i < courseNameArray.Length; i++)
                courseList.Add(new DO.Course { CourseId = i, CourseName = courseNameArray[i] });

            string[] studentNameArray = new string[] { "אברהם", "יצחק", "יעקוב", "שרה", "רחל" };

            int id = -1;

            studentList = new List<DO.Student>
            {
                new DO.Student {StudentId = ++id ,
                    IsMarried = true ,
                    PersonDate = DateTime.ParseExact("24.03.85", "dd.MM.yy", null),
                    PersonName = studentNameArray[id],
                    StudentGender =  DO.Gender.male
                },
                new DO.Student {StudentId = ++id ,
                    IsMarried = false ,
                    PersonDate = DateTime.ParseExact("20.04.95", "dd.MM.yy", null),
                    PersonName = studentNameArray[id],
                    StudentGender = DO.Gender.male
                },
                new DO.Student {StudentId = ++id ,
                    IsMarried = true ,
                    PersonDate = DateTime.ParseExact("02.12.77", "dd.MM.yy", null),
                    PersonName = studentNameArray[id],
                    StudentGender = DO.Gender.male
                },
                new DO.Student {StudentId = ++id ,
                    IsMarried = false ,
                    PersonDate = DateTime.ParseExact("03.11.87", "dd.MM.yy", null),
                    PersonName = studentNameArray[id],
                    StudentGender = DO.Gender.female
                },
                new DO.Student {StudentId = ++id ,
                    IsMarried = true ,
                    PersonDate = DateTime.ParseExact("04.03.67", "dd.MM.yy", null),
                    PersonName = studentNameArray[id],
                    StudentGender = DO.Gender.female
                },
            };

            studentInCourseList = new List<DO.StudentInCourse>();
            
            var rand = new Random();
            for (int i = 0; i < studentList.Count; i++)
                studentInCourseList.Add(new DO.StudentInCourse() { StudentId = i, CourseId = rand.Next(0, courseNameArray.Length), Grade = rand.Next(0,100) });
        }
    }
}
