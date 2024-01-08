using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

public class Pratice
{
    /// <summary>
    /// 使用 DI 基本用法
    /// </summary>
    public static void Basic()
    {
        var provider = new ServiceCollection()
                .AddTransient<IFoo, Foo>()
                .AddScoped<IBar>(_ => new Bar())
                .AddSingleton<IBaz, Baz>()
                .AddTransient(typeof(IFoobar<,>), typeof(FooBar<,>))
                .BuildServiceProvider();
        Debug.Assert(provider.GetService<IFoo>() is Foo);
        Debug.Assert(provider.GetService<IBar>() is Bar);
        Debug.Assert(provider.GetService<IBaz>() is Baz);
    }

    /// <summary>
    /// Service Collection
    /// </summary>
    public static void ServiceCollection()
    {
        // Service collection
        var services = new ServiceCollection()
                        .AddTransient<Base, Foo>()
                        .AddTransient<Base, Bar>()
                        .AddTransient<Base, Baz>()
                        .BuildServiceProvider()
                        .GetServices<Base>();
        Debug.Assert(services.OfType<Foo>().Any());
        Debug.Assert(services.OfType<Bar>().Any());
        Debug.Assert(services.OfType<Baz>().Any());
    }

    /// <summary>
    /// Life Cycle basic
    /// </summary>
    public static void LifeCycle()
    {
        var root = new ServiceCollection()
                .AddTransient<IFoo, Foo>()        // 用完即丟，每次都是新的
                .AddScoped<IBar>(_ => new Bar ())  // 使用一個 Provider 就會產生一個新的
                .AddSingleton<IBaz, Baz>()        // 一個 Root 建立一次
                .BuildServiceProvider();
        var provider1 = root.CreateScope().ServiceProvider;
        var provider2 = root.CreateScope().ServiceProvider;
        GetServices<IFoo>(provider1);
        GetServices<IBar>(provider1);
        GetServices<IBaz>(provider1);
        System.Console.WriteLine();
        GetServices<IFoo>(provider2);
        GetServices<IBar>(provider2);
        GetServices<IBaz>(provider2);

    }

    static void GetServices<T>(IServiceProvider provider)
    {
        provider.GetService<T>();
        provider.GetService<T>();
    }


    /// <summary>
    /// Life Cycle basic2
    /// </summary>
    public static void LifeCycle_Dispose()
    {
        using (var root = new ServiceCollection()
                .AddTransient<IFoo, Foo>()        // 用完即丟，每次都是新的
                .AddScoped<IBar>(_ => new Bar ())  // 使用一個 Provider 就會產生一個新的
                .AddSingleton<IBaz, Baz>()        // 一個 Root 建立一次
                .BuildServiceProvider())
        {
            using (var scope = root.CreateScope())
            {
                System.Console.WriteLine("In using block (scope)");
                var provider = scope.ServiceProvider;
                provider.GetService<IFoo>();
                provider.GetService<IBar>();
                provider.GetService<IBaz>();
                System.Console.WriteLine("Child container is disposed.");
            }
            System.Console.WriteLine("Root container is disposed.");
        }
    }
}