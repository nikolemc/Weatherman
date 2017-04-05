using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weatherman
{
    class PastWeatherData
    {
        public int Id { get; set; }
        public int Temperature { get; set; }
        public string CurrentConditions { get; set; }
        public string UserName { get; set; }
        public string Time { get; set; }


        public PastWeatherData()
        {

        }

        public PastWeatherData(SqlDataReader reader)
        {
            Id = (int)reader["Id"];
            Temperature = (int)reader["Temperature"];
            CurrentConditions = reader["CurrentConditions"].ToString();
            UserName = reader["UserName"].ToString();
            Time = reader["Time"].ToString();

        }
    }

   
}
