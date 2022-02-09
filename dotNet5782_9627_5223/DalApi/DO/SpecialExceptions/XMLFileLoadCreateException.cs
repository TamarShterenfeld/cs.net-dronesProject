using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DO
{
    /// <summary>
    /// a class that treats in exceptions of Battery type.
    /// </summary>
    [Serializable]
    public class XMLFileLoadCreateException : Exception
    {
        string myMessage;
        public string Mymessage { get; set; }
        string file;
        public string File { get; set; }
        public XMLFileLoadCreateException() : base() { }
        public XMLFileLoadCreateException(string message) : base(message) { }
        public XMLFileLoadCreateException(string message, Exception inner) : base(message, inner) { }
        protected XMLFileLoadCreateException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public XMLFileLoadCreateException(string m1, string m2, Exception ex) : base(m1) { }
        override public string ToString()
        {
            return "XMLFileLoadCreateException Exception is thrown from DO logic level from file: " + Message;
        }
    }

}
