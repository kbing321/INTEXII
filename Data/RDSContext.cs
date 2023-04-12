using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using INTEXII.Models;

namespace INTEXII.Data

{
    public class RDSContext : DbContext
    {
        public RDSContext() { }

        public RDSContext(DbContextOptions<RDSContext> options) : base(options) { }

        public virtual DbSet<Mummies> Mummies { get; set; }

    }
}