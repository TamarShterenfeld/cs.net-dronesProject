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
        public class BLStringIdException : Exception
        {
            public string Id { get; set; }
            public BLStringIdException() : base() { }

            public BLStringIdException(string message, Exception inner) : base(message, inner) { }
            protected BLStringIdException(SerializationInfo info, StreamingContext context) : base(info, context) { }
            public BLStringIdException(string str) { Id = str; }
            override public string ToString()
            {
                return "String id Exceptioin : DAL logic level throws a string id exception " + Message;
            }

        }
    }
}