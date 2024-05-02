    using Microsoft.EntityFrameworkCore;

    namespace A4.Models
    {
        public class A4DbContext : DbContext
        {
            public A4DbContext(DbContextOptions<A4DbContext> options) : base(options)
            {
            }

            // DbSet properties for your entity classes
            public DbSet<Product> Products { get; set; }
            public DbSet<User> Users { get; set; }

            public DbSet<PO> POs { get; set; }
            
            // Add more DbSet properties as needed for other entities
        }
    }
