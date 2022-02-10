using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalXml
{
    sealed partial class DalXml
    {
        bool ConvertStringToBool(string s)
        {
            return(s == "true" ? true : false);
        }
    }
}
