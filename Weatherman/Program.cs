using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Weatherman
{
    class Program
    {
        static int zipCode;

        public double temp { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public double pressure { get; set; }
        public double sea_level { get; set; }
        public double grnd_level { get; set; }
        public int humidity { get; set; }
        public double temp_kf { get; set; }

        static void UserName()
        {
            //Ask the user for there name
            Console.WriteLine("What is your name?");
            string name = Console.ReadLine();
        }

        static void GenerateZipCode()
        {
            Console.WriteLine("Enter a zip code find out the current weather.");
            zipCode = int.Parse(Console.ReadLine());
          
            while (zipCode.ToString().Length != 5)
            {
                Console.WriteLine("Error. Zip code is not 5 digits. Please enter a valid number.");
                zipCode = int.Parse(Console.ReadLine());

                if (zipCode.ToString().Length == 5)
                {
                    zipCode = int.Parse(Console.ReadLine());

                }
            }
            
        }
                       
        static void goToWebsite()
        {
            //update URL with zipcode
            //my API key is 3a882c75f7a3a3545163ca7f7fbba7b3
            var url = "http://api.openweathermap.org/data/2.5/weather?zip=" + zipCode + ",us&units=metric&APPID=3a882c75f7a3a3545163ca7f7fbba7b3";

            var request = WebRequest.Create(url);

            var response = request.GetResponse();

            var rawResponse = String.Empty;
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                rawResponse = reader.ReadToEnd();
                Console.WriteLine(rawResponse);
            }
            var weatherResults = JsonConvert.DeserializeObject<RootObject>(rawResponse);

            Console.WriteLine();
        }


        static void Main(string[] args)
        {
            UserName();
            GenerateZipCode();
            goToWebsite();

            




        }
    }
}
