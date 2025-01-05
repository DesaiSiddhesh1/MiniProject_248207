using Microsoft.Data.SqlClient;
using System.Data;

namespace MiniProject_248207.Models
{
    public class UserDisplay
    {
        public string FullName { get; set; }

        public string Gender { get; set; }

        public string EmailId { get; set; }

        public string PhoneNumber { get; set; }

        public string CityName { get; set; }

        public static List<UserDisplay> GetAllUser()
        {
            List<UserDisplay> usersdisplay = new List<UserDisplay>();

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=MiniProjectDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetAllUsers";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    usersdisplay.Add(new UserDisplay
                    {
                        CityName = reader["CityName"].ToString(),
                        FullName = reader["FullName"].ToString(),
                        Gender = reader["Gender"].ToString(),
                        EmailId = reader["EmailId"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString()
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
            return usersdisplay;
        }
    
    }
}
