using System;
using BearerTokenDecoder;
using System.Configuration;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string token =  ConfigurationManager.AppSettings["token"];
            JwtDecoder d = new JwtDecoder();
            Console.WriteLine(d.Decode(token).Result);

            string token2 = d.Encode(ConfigurationManager.AppSettings["Id"],
                ConfigurationManager.AppSettings["FirstName"],
                ConfigurationManager.AppSettings["LastName"],
                ConfigurationManager.AppSettings["LanguageIsoCode"],
                ConfigurationManager.AppSettings["Email"],
                ConfigurationManager.AppSettings["UserName"]
                );
            Console.WriteLine(d.Decode(token2).Result);
            Console.ReadLine();
        }
    }
}
