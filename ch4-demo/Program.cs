using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

var porvider = new ServiceCollection()
                .AddTransient<IFoo, Foo>()
                .AddScoped<IBar>(_ => new Bar())
                .AddSingleton<IBaz, Baz>()
                .BuildServiceProvider();
Debug.Assert(porvider.GetService<IFoo>() is Foo);
Debug.Assert(porvider.GetService<IBar>() is Bar);
Debug.Assert(porvider.GetService<IBaz>() is Baz);
