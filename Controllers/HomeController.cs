using INTEXII.Data;
using INTEXII.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace INTEXII.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        //private readonly RDSContext _dbContext;
        private readonly string _connectionString;

        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ApplicationDbContext");
        }

        [Authorize]
        public IActionResult ManageAdmin()
        {
            //var userRepository = new UserRepository(_connectionString);
            //var usernames = userRepository.GetAllUsernames();

            //return View(usernames);
            var userRepository = new UserRepository(_connectionString);
            var users = userRepository.GetAllUsers();
            return View(users);
        }


        public IActionResult Index()
        {
            return View();
        }

        //[Authorize]
        //public IActionResult DataAdmin()
        //{
        //    return View("DataAdmin");
        //}
        [Authorize]
        public IActionResult DataAdmin()
        {
            return View("DataAdmin");
        }

        public IActionResult About()
        {
            return View("About");
        }

        public IActionResult Prediction()
        {
            return View("Predict");
        }

        public IActionResult Unsupervised()
        {
            return View("Unsupervised");
        }
        public IActionResult Privacy()
        {
            return View("Privacy");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //[HttpPost]
        //public IActionResult EditUser(string userId, string email)
        //{
        //    // Get the user from the UserRepository
        //    var user = UserRepository.GetUserById(userId);

        //    // Update the user's email address
        //    user.Email = email;
        //    _userRepository.UpdateUser(user);

        //    // Redirect the user back to the page where they can view the updated user information
        //    return RedirectToAction("ViewUser", new { userId });
        //}
        [Authorize]
        [HttpPost]
        public IActionResult EditUser(string userId, string email)
        {
            var userRepository = new UserRepository(_connectionString);
            userRepository.UpdateUserEmail(userId, email);

            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpPost]
        public IActionResult DeleteUser(string userId)
        {
            var userRepository = new UserRepository(_connectionString);
            userRepository.DeleteUserByEmail(userId);
            //userRepository.UpdateUserEmail(userId, email);

            return RedirectToAction("ManageAdmin");
        }



        [HttpPost]
        public IActionResult Predict(Prediction prediction)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7088/score");

                //http post
                var postTask = client.PostAsJsonAsync<Prediction>("prediction", prediction);
                postTask.Wait();

                var result = postTask.Result;
            }
            return View(prediction);
        }

    }

}