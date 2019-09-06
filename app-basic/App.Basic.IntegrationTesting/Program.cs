using HashidsNet;
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
                //string base64Guid = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                //var salt = Guid.NewGuid().ToString();
                //var hashids = new Hashids(salt);
                //var hash = hashids.Decode(salt);

                var hashIds1 = new HashidsNet.Hashids("abc", 4, "BCDFHJKNPQRSTUVWXYZMEGA0123456789");
                var hashIds2 = new HashidsNet.Hashids("fgh", 4, "BCDFHJKNPQRSTUVWXYZMEGA0123456789");

                var str1 = hashIds1.EncodeLong(0);
                var str2 = hashIds1.EncodeLong(uint.MaxValue);
                Console.WriteLine(str1);
                Console.WriteLine(str2);



                //for (int i = 0; i < 50; i++)
                //{
                //    //Console.WriteLine(Guid.NewGuid().ToString("N"));
                //    //var salt = Guid.NewGuid().ToString();
                //    //var hashids = new Hashids(salt);
                //    //var hash = hashids.Encode(salt);
                //    //Console.WriteLine(hash);
                //}

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
