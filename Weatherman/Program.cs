using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weatherman
{
    class Program
    {
        static int temp;
        static int zipcode;

        static void UserName()
        {
            //Ask the user for there name
            Console.WriteLine("What is your name?");
            string name = Console.ReadLine();
        }

        static void GenerateZipCode()
        {
            Console.WriteLine("Enter a zip code find out the current weather.");
            var temp = int.Parse(Console.ReadLine());

            while (temp.ToString().Length != 5)
            {
                Console.WriteLine("Error. Zip code is not 5 digits. Please enter a valid number.");
                temp = int.Parse(Console.ReadLine());

                if (temp.ToString().Length == 5)
                {
                    temp = int.Parse(Console.ReadLine());

                }
            }

        }

        static void Main(string[] args)
        {
            UserName();
            GenerateZipCode();

          


        }
    }
}
