using System;

namespace rainbow
{
    class Program
    {
        static void Main(string[] args)
        {
            Common.init();
            Console.WriteLine(Common.manager.GetModel().ToString());
        }
    }
}
