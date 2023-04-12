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
        private readonly ApplicationDbContext _dbContext;
        private readonly string _connectionString;

        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ApplicationDbContext");
        }

        public IActionResult DataAdmin()
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

        [Authorize]
        //public IActionResult DataAdmin()
        //{
        //    return View("DataAdmin");
        //}
        [Authorize]
        public IActionResult ManageAdmin()
        {
            return View("ManageAdmin");
        }

        public IActionResult About()
        {
            return View("About");
        }

        public IActionResult Supervised()
        {
            return View("Supervised");
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
        [HttpPost]
        public IActionResult EditUser(string userId, string email)
        {
            var userRepository = new UserRepository(_connectionString);
            userRepository.UpdateUserEmail(userId, email);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteUser(int userId)
        {
            var userRepository = new UserRepository(_connectionString);
            userRepository.DeleteUser(userId);

            return RedirectToAction("DataAdmin");
        }



    }

}