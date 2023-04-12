using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using INTEXII.Data;
using System;
using System.Linq;
using INTEXII.Models;

namespace INTEXII.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any movies.
                //if (context.Mummies.Any())
                //{
                //    return;   // DB has been seeded
                //}

                context.Mummies.AddRange(
                    new Mummies
                    {
                        Username = "josh@josh.com",
                        RoleType = "Admin"
                    },

                    new Mummies
                    {
                        Username = "Angelina@josh.com",
                        RoleType = "User"
                    },

                    new Mummies
                    {
                        Username = "Kohlton@josh.com",
                        RoleType = "User"
                    },

                    new Mummies
                    {
                        Username = "Nathaniel@josh.com",
                        RoleType = "User"
                    }
                );
                //context.SaveChanges();
            }
        }
    }
}