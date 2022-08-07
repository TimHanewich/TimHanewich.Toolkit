using System;
using TimHanewich.Toolkit;
using System.Threading.Tasks;
using TimHanewich.Toolkit.CommandLine;
using Newtonsoft.Json;

namespace FunctionalTesting
{
    class Program
    {
        static void Main(string[] args)
        {

            CommandLineArguments cliargs = CommandLineArguments.Parse(args);
            Console.WriteLine(JsonConvert.SerializeObject(cliargs, Formatting.Indented));

            string v = cliargs.Value("agee");
            if (v == null)
            {
                Console.WriteLine("IT WAS NULL");
            }
            Console.WriteLine(v);
        }
    }
}
