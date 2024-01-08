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
}