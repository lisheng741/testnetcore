using Castle.DynamicProxy;
using Simple.Common.Components.Castle;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Extensions.DependencyInjection;

public static class CastleServiceCollectionExtensions
{
    //public static IServiceCollection AddServices(this IServiceCollection services)
    //{
    //    services.Add(typeof(CastleServiceCollectionExtensions), typeof(CastleServiceCollectionExtensions),)
    //}


    //public static IServiceCollection Add(
    //    this IServiceCollection services,
    //    Type serviceType,
    //    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] Type implementationType,
    //    ServiceLifetime lifetime,
    //    params Type[] interceptorTypes)
    //{
    //    Func<IServiceProvider, object> factory = (provider) =>
    //    {
    //        var target = provider.GetService(implementationType);

    //        List<IInterceptor> interceptors = interceptorTypes.ToList().ConvertAll<IInterceptor>(interceptorType =>
    //        {
    //            return provider.GetService(interceptorType) as IInterceptor;
    //        });

    //        var proxy = CastleHelper.Generator.CreateInterfaceProxyWithoutTarget(serviceType,);

    //        return proxy;
    //    };
    //    var serviceDescriptor = new ServiceDescriptor(serviceType, factory, lifetime);
    //    services.Add(serviceDescriptor);

    //    return services;
    //}
}
