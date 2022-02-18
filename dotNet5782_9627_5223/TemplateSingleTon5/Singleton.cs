using System;
using System.Reflection;

namespace Singleton
{
    public abstract class Singleton<T> where T: Singleton<T>
    {
        // make the static and protected constructors
        static Singleton() { }
        protected Singleton() { }

        class Nested
        {
            // _instance is initialized to null
            internal static volatile T _instance = null;
            internal static readonly object  _lock  = new object();//for multithreading
            static Nested() { }
        }
        // separate property ensures Lazy class instatiation, no setter, getter only.
        public static T Instance
        {
            get
            {
                // if it is already not null - no need for lock()
                if (Nested._instance == null)
                {
                    lock (Nested._lock)// for multithreading
                    {
                        if (Nested._instance == null)// double check
                        {
                            // get the type of the generic class, and check whether it is sealed(Reflection)
                            Type t = typeof(T);
                            if (t == null || !t.IsSealed)
                            {
                                throw new SingletonException(string.Format($"{t.Name} must be a sealed class"));
                            }
                            // get the instance constructor of the deriving class
                            // ensure it to be non-public and parameterLess
                            // (still using Reflection)
                            ConstructorInfo constr = null;
                            try
                            {
                                constr = t.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
                            }
                            catch (Exception ex)
                            {
                                throw new SingletonException($"A private/protected constructor is missing for {t.Name}", ex);
                            }
                            // also exclode internal or default constructor (still Reflection)
                            if (constr == null || constr.IsAssembly)
                            {
                                throw new SingletonException($"A private/protected constructor is missing for {t.Name}");
                            }
                            // create new instance and invoke its constructor by Reflection
                            Nested._instance = (T)constr.Invoke(null);
                        }
                    }
                }
                return Nested._instance;
            }
        }
    }
}
