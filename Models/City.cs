using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace MiniProject_248207.Models
{
    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }

        public static List<City> GetCities()
        {
            List<City> cities = new List<City>();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=MiniProjectDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetAllCities";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cities.Add(new City
                    {
                        CityId = Convert.ToInt32(reader["CityId"]),
                        CityName = reader["CityName"].ToString()
                    });
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
            return cities;
        }
    }
}
