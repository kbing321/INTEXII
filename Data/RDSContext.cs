using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using INTEXII.Models;
using Microsoft.AspNetCore.Identity;

namespace INTEXII.Data
{
    public class RDSContext : DbContext
    {
        public RDSContext() { }

        public RDSContext(DbContextOptions<RDSContext> options) : base(options) { }

        public virtual DbSet<Mummies> Mummies { get; set; }

        public virtual DbSet<IdentityUser> IUsers { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        public virtual DbSet<IdentityUserClaim<string>> UserClaims { get; set; }

        public virtual DbSet<finalburialrecords2> finalburialrecords2 { get; set; }

    }
}
