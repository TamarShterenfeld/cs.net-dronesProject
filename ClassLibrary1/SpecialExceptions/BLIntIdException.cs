using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        [Serializable]
        public class BLIntIdException : Exception
        {
            public int Id { get; set; }
            public BLIntIdException() : base() { }
            public BLIntIdException(string message) : base(message) { }
            public BLIntIdException(string message, Exception inner) : base(message, inner) { }
            protected BLIntIdException(SerializationInfo info, StreamingContext context) : base(info, context) { }
            public BLIntIdException(int id) { Id = id; }
            override public string ToString()
            {
                return "Int Id Exceptioin : DAL logic level throws an int id exception " + Message;
            }

        }
    }
}