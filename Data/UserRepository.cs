using INTEXII.Models;
using Microsoft.Data.SqlClient;

namespace INTEXII.Data
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        //public List<string> GetAllUsernames()
        //{
        //    var usernames = new List<string>();

        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        connection.Open();

        //        var query = "SELECT * FROM dbo.AspNetUsers";

        //        using (var command = new SqlCommand(query, connection))
        //        {
        //            using (var reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    var id = reader.GetString(0);
        //                    var username = reader.GetString(1);
        //                    usernames.Add(id);
        //                    usernames.Add(username);
        //                }
        //            }
        //        }

        //    }
        //    return usernames;
        //}

        public List<UserViewModel> GetAllUsers()
        {
            var users = new List<UserViewModel>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "SELECT Id, UserName FROM dbo.AspNetUsers";

                using (var command = new SqlCommand(query, connection))
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
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                //var query = "UPDATE dbo.AspNetUsers SET UserName = @NewUser WHERE Id = @UserId";
                var query = "UPDATE dbo.AspNetUsers SET UserName = @NewUser, NormalizedUserName = @NewUser, Email = @NewUser, NormalizedEmail = @NewUser WHERE Id = @UserId";


                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NewUser", newUser);
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteUser(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "DELETE FROM dbo.AspNetUsers WHERE Id = @UserId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.ExecuteNonQuery();
                }
            }
        }


    }

}