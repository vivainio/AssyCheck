using System;


namespace AssyCheck.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            AssyChecker.AssertDependencies();
            Console.WriteLine("Ok");
        }
    }
}
