using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Singleton
{
    /// <summary>
    /// the class SingletonException inherits the class "Exception"
    /// it adds another constructor besides the Exception's constructor.
    /// </summary>
    [Serializable]
    public class SingletonException:Exception
    {
        public SingletonException(string text) :base(text){ }
        public SingletonException(string text, Exception e) : base(text,e) { }
        public SingletonException(SerializationInfo info,StreamingContext context ) : base(info, context) { }
    }
}
