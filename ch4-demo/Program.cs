using Microsoft.Extensions.DependencyInjection;

var porvider = new ServiceCollection()
                .AddTransient<IFoo, Foo>()
                .AddScoped<IFoo, Foo>()
                .AddSingleton<IFoo, Foo>()
                .BuildServiceProvider();