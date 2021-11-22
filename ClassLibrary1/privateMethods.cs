using System;
using System.Collections.Generic;
using System.Text;
using static DalObject.DataSource;
using IDal.DO;
using System.Linq;

namespace IBL
{
    public partial class BL : IBL
    {
        /// <summary>
        /// The function checks if the variable is int type.
        /// </summary>
        /// <param name="id">a int type variable</param>
        private static void InputIntValue(ref int id)
        {
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Id can contain only digits, Please try again!");
            }
        }

        public static int GetParcelIndex() 
        {
            return DalObject.DalObject.IncreaseParcelIndex();
        }

    }
}
