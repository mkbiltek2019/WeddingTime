using SimpleInjector;
using System;

namespace AIT.UtilitiesComponents.Services
{
    public sealed class UnityService
    {
        // .NET guarantees thread safety for static initialization
        private static readonly UnityService _instance = new UnityService();
        private static readonly Container _container = new Container();

        public UnityService()
        {
            var container = new Container();
        }

        // to make it thread-safe without using locks
        public static UnityService Get()
        {
            return _instance;
        }

        public Container Container()
        {
            return _container;
        }

        public UnityService Register<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            Container().Register<TService, TImplementation>();
            return _instance;
        }

        public UnityService Register<TService, TImplementation>(Lifestyle lifestyle)
            where TService : class
            where TImplementation : class, TService
        {
            Container().Register<TService, TImplementation>(lifestyle);
            return _instance;
        }

        public UnityService Register<TConcrete>() 
            where TConcrete : class
        {
            Container().Register<TConcrete>();
            return _instance;
        }

        public UnityService Register(Type serviceType, Type implementationType, Lifestyle lifestyle)
        {
            Container().Register(serviceType, implementationType, lifestyle);
            return _instance;
        }

        public UnityService Register(Type serviceType, Type implementationType)
        {
            Container().Register(serviceType, implementationType);
            return _instance;
        }

        public UnityService Register<TService>(Func<TService> instanceCreator, Lifestyle lifestyle) 
            where TService : class
        {
            Container().Register(instanceCreator, lifestyle);
            return _instance;
        }

        public UnityService Register<TConcrete>(Lifestyle lifestyle) 
            where TConcrete : class
        {
            Container().Register<TConcrete>(lifestyle);
            return _instance;
        }        
    }
}
