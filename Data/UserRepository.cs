using INTEXII.Models;
using Npgsql;
using System.Data.SqlClient;

namespace INTEXII.Data
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<UserViewModel> GetAllUsers()
        {
            var users = new List<UserViewModel>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                var query = "SELECT Id, UserName FROM public.AspNetUsers";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new UserViewModel
                            {
                                Id = reader.GetString(0),
                                Email = reader.GetString(1)
                            };
                            users.Add(user);
                        }
                    }
                }
            }
            return users;
        }


        public void UpdateUserEmail(string userId, string newUser)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                var query = "UPDATE public.AspNetUsers SET UserName = @NewUser, NormalizedUserName = @NewUser, Email = @NewUser, NormalizedEmail = @NewUser WHERE Id = @UserId";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NewUser", newUser);
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteUserByEmail(string userId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                var query = "DELETE FROM public.AspNetUsers WHERE Id = @UserId";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
