# AssyCheck

Excercises System.Buffers and other libraries to ensure they have correct binding redirects.

Original idea & implementation courtesy of [Marc Gravell](https://github.com/mgravell/Pipelines.Sockets.Unofficial/blob/5ae5fe451c4f71a618c4324f62c3beb2dc0b39a5/src/Pipelines.Sockets.Unofficial/Helpers.cs#L142-L172)

## Problem statement

Dotnet framework 4.7.2 applications can sometimes fail with binding redirects to System.Buffers, System.Memory, System.Runtime.CompilerServices.Unsafe (full list TBD). 

Exception looks like this:

```
System.IO.FileNotFoundException: Could not load file or assembly 'System.Buffers, Version=4.0.2.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51' or one of its dependencies. The system cannot find the file specified.
File name: 'System.Buffers, Version=4.0.2.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
```

These are caused by assemblies that are part of netstandard 2.0, and are built in to .NET Core, but are installed to net472 applications with nuget. AssyCheck fixes 
this problem and helps detect it early.


## Usage

Install dependency:

```
$ dotnet add package AssyCheck
```

In your application assert that everything is correctly working:

```csharp

static void Main(string[] args)
{
    AssyChecker.AssertDependencies();
}
```

Note that just adding a dependency to AssyCheck to your "main" application usually fixes your problem, but the assertion can be done to detect any problem earlier anyway.

