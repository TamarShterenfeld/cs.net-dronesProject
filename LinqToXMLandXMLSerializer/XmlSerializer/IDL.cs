using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlSeralizerSample
{
    interface IDL
    {
        #region Student
        void AddStudent(DO.Student stu);
        void DeleteStudent(int id);
        void UpdateStudent(DO.Student stu);
        DO.Student GetStudent(int id);
        IEnumerable<DO.Student> GetAllStudents(Func<DO.Student, bool> predicat = null);
        
        #endregion
    }
}
