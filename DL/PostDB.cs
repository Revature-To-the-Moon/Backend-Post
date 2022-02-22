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


//After every change to models Last ran initMig3
//dotnet ef migrations add AddedFieldToRootGroupId -c PostDB --startup-project ../WebAPI
//dotnet ef database update --startup-project ../WebAPI