using System;
using System.Threading.Tasks;

namespace App.Basic.IntegrationTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            Startup.MainAsync(args).GetAwaiter().GetResult();
            Console.WriteLine("----complete----");
            Console.ReadKey();
        }
    }

    class Startup
    {
        public static async Task MainAsync(string[] args)
        {
            try
            {


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
