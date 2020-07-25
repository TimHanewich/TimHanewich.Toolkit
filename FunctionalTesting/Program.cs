using System;
using TimHanewich.Toolkit;
using System.Threading.Tasks;

namespace FunctionalTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            HanewichTimer ht = new HanewichTimer();
            ht.StartTimer();
            Task.Delay(5000).Wait();
            ht.StopTimer();
            Console.WriteLine(ht.GetElapsedTime().ToString());
        }
    }
}
