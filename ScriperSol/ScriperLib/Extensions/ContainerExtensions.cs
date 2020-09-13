using SimpleInjector;
using System;
using System.Collections.Generic;

namespace ScriperLib.Extensions
{
    public static class ContainerExtensions
    {
        public static void RegisterWithFactory<TService,TImplementaion>(this Container container) 
            where TService : class 
            where TImplementaion : class, TService
        {
            container.Register<TService, TImplementaion>();
            container.Register<Func<TService>>(() => container.GetInstance<TService>);
        }

        public static void RegisterWithFactoryCollection<TService>(this Container container, params Type[] types)
            where TService : class
        {
            container.Collection.Register<TService>(types);
            container.Register<Func<IEnumerable<TService>>>(() => container.GetAllInstances<TService>, Lifestyle.Transient);
        }
    }
}
