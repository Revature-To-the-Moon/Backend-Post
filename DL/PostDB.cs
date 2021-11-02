using Microsoft.EntityFrameworkCore;
using Models;


namespace DL
{
    public class PostDB : DbContext
    {
        public PostDB() : base() { }

        public PostDB(DbContextOptions options) : base(options) { }
    }
}
