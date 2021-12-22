using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI_BL
{
    /// <summary>
    /// an interface which introduces all the methods that every sub navigate class has to imlement.
    /// the access to data is implemented by the IBl object 
    /// which connect the function to the data in the lower logic levels.
    /// </summary>
    public interface ISubNavigate
    {
        /// <summary>
        /// a function that navigates the user into his innerChoice
        /// </summary>
        /// <param name="bl"></param>
        public void Options(ref BLApi.IBL bl);
    }
}
