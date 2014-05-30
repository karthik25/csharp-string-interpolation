# C# String Interpolation

This repo is to experiment with ruby like string interpolation in C#. Consider the following run in the irb shell:

```shell
irb(main):001:0> salutation = "Hello"
=> "Hello"
irb(main):002:0> who = "World!"
=> "World!"
irb(main):003:0> puts "#{salutation}, #{who}"
Hello, World!
=> nil
irb(main):004:0>
```

This feature of ruby is called "string interpolation". Unfortunately C# doesn't have this feature built-in. This repo provides this ability! Here is a sample!! To use this you need an instance of a string and an instance of a type. Replacements in the string has to be specified in the format #{PROPERTY-NAME} where PROPERTY-NAME is a valid property in the instance passed. For example "src" uses the Id, Name and Point properties in the "MoreComplex" class. As shown in the following example, you can even use complex properties ("Point") - preferrably with ToString() overridden.

```csharp
using System;
using CSharpStringInterpolation.Lib;

public class Sample
{
    public static void Main()
    {
        const string src = "Id: #{Id}, Name: #{Name}, Point: #{Point}";
        var c = new MoreComplex { Id = 1, Name = "Karthik", Point = new Point { X = 1, Y = 2 } };
        var interpolated = c.InterpolateThis(src);
        Console.WriteLine(interpolated);
    }
}

public class MoreComplex
{
  public int Id { get; set; }
  public string Name { get; set; }
  public Point Point { get; set; }
}

public class Point
{
  public int X { get; set; }
  public int Y { get; set; }

  public override string ToString()
  {
     return string.Format("({0},{1})", X, Y);
  }
}
```

# What's Supported?

* Simple properties like integers, string etc.
* Complex properties that has overridden ToString()
* Array based properties
* Simple expressions like Property1 + Property2 etc etc.
