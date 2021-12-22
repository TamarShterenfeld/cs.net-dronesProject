using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    //an interface that forces the inheriters to creat a Location object.
    public interface ILocatable
    {
        Location Location { get; set; }
    }
}
