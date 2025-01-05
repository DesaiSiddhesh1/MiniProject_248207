using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MiniProject_248207.Models
{
    public class Users
    {
        public string LoginName { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        public char Gender { get; set; }
        [Required]
        [EmailAddress]
        public string EmailId { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        public bool RememberMe { get; set; }
        public static void RegisterUser(Users user)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=MiniProjectDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "InsertUser";

                cmd.Parameters.AddWithValue("@LoginName", user.LoginName);
                cmd.Parameters.AddWithValue("@FullName", user.FullName);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@Gender", user.Gender);
                cmd.Parameters.AddWithValue("@EmailId", user.EmailId);
                cmd.Parameters.AddWithValue("@CityId", user.CityId);
                cmd.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);

                cmd.ExecuteNonQuery();


            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }
        public static Users Authenticate(string loginName,string password)
        {
            Users authenticateUser = null;
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=MiniProjectDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            try
            {
                
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AuthenticateUser";

                cmd.Parameters.AddWithValue("@LoginName", loginName);
                cmd.Parameters.AddWithValue("@Password", password);
                

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    authenticateUser = new Users
                    {
                        FullName = reader["FullName"].ToString()
                    };
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
            return authenticateUser;
        }
    }
}
