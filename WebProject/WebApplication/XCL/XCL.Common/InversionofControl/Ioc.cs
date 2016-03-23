using Microsoft.Practices.Unity;

namespace XCL.Common.InversionofControl
{
    public class Ioc
    {
        public static IUnityContainer _container;
        public static object _createInstanceLocker = new object();

        public static IUnityContainer Instance
        {
            get
            {
                if (_container == null)
                {
                    lock (_createInstanceLocker)
                    {
                        if (_container == null)
                            _container = new UnityContainer();
                    }
                }

                return _container;
            }
        }

        public static void Reset()
        {
            if (_container != null)
            {
                lock (_createInstanceLocker)
                {
                    if (_container != null)
                    {
                        _container.Dispose();
                        _container = null;
                    }
                }
            }
        }

        public static T Get<T>()
        {
            return Instance.Resolve<T>();
        }

        public static void Add<T>(T obj)
        {
            Instance.RegisterInstance(obj);
        }
    }
}
