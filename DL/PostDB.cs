using Microsoft.EntityFrameworkCore;
using Models;


namespace DL
{
    public class PostDB : DbContext
    {
        public PostDB() : base() { }

        public PostDB(DbContextOptions options) : base(options) { }

        public DbSet<Root> Roots { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Vote> Votes { get; set; }
    }
}
