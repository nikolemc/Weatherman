using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        const string connectionString =
               @"Server=localhost\SQLEXPRESS;Database=Weather;Trusted_Connection=True;";


        static string UserName()
        {
            //Ask the user for there name
            Console.WriteLine("What is your name?");
            string userName = Console.ReadLine();
            return userName;
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
                       
        static RootObject goToWebsite()
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
               // Console.WriteLine(rawResponse);
            }
            var weatherResults = JsonConvert.DeserializeObject<RootObject>(rawResponse);

           // Console.WriteLine();
            return weatherResults;
        }

        static public void AddWeatherDataToDataBase(RootObject WeatherData, string userName)
        {
            
            using (var Dataconnection = new SqlConnection(connectionString))
            {
                var text = @"INSERT INTO WeatherData (UserName, Temperature, CurrentConditions)" +
                           "Values (@UserName, @Temperature, @CurrentConditions)";

                var cmd = new SqlCommand(text, Dataconnection);

                cmd.Parameters.AddWithValue("@userName", userName);
                cmd.Parameters.AddWithValue("@Temperature", WeatherData.main.temp);
                cmd.Parameters.AddWithValue("@CurrentConditions", WeatherData.weather.First().description);
                Dataconnection.Open();
                cmd.ExecuteNonQuery();
                Dataconnection.Close();

            }
            
        }

      

        static public List<PastWeatherData> GetCurrentWeatherData()
        {
            var CurrentWeatherResults = new List<PastWeatherData>();

            using (var connection = new SqlConnection(connectionString))
            {
                var sqlCommand = new SqlCommand(@"SELECT [Temperature],[CurrentConditions]FROM [dbo].[WeatherData]s", connection);
                connection.Open();
                var reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    var weatherResults = new PastWeatherData
                    {
                        Temperature = (int)reader["Temperature"],
                        CurrentConditions = reader["CurrentConditions"].ToString()
                    };
                    CurrentWeatherResults.Add(weatherResults);

                }

                connection.Close();
                Console.WriteLine($"The current temperature is {CurrentWeatherResults[0].Temperature}° and the current condition is {CurrentWeatherResults[0].CurrentConditions} ");
                return CurrentWeatherResults;

            }
               
        }


        static void Main(string[] args)
        {
            var name =  UserName();
            GenerateZipCode();
            var data = goToWebsite();
            AddWeatherDataToDataBase(data, name);
            GetCurrentWeatherData();     
        

        }
    }
}
