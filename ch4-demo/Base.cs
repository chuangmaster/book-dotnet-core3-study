interface IFoo { }
interface IBar { }
interface IBaz { }

interface IFoobar<T1, T2>
{

}


public class Base : IDisposable
{
    public Base() => System.Console.WriteLine($"An istance of {GetType().Name} is created.");
    public void Dispose() => System.Console.WriteLine($"An istance of {GetType().Name} is disposed.");
}

public class Foo : Base, IFoo, IDisposable { }
public class Bar : Base, IBar, IDisposable { }
public class Baz : Base, IBaz, IDisposable { }

public class FooBar<T1, T2> : IFoobar<T1, T2>
{
    public T1 Foo { get; }
    public T2 Bar { get; }
    public FooBar(T1 foo, T2 bar)
    {
        Foo = foo;
        Bar = bar;
    }
}