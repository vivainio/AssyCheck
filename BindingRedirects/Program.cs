using System;
using AssyCheck;
namespace BindingRedirects
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Checking dependencies");
            AssyChecker.AssertDependencies();
            Console.WriteLine("Dependencies ok");
        }
    }
}
