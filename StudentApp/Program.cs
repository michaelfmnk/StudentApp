using System;

namespace StudentApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var a = new Student("a", "b", "c", 1000);
            var b = new Student("a", "b", "c", 1000);
            var res = a != b;
            Console.WriteLine(res);
        }
    }
}