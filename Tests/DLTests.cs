using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL;
using Xunit;
using Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Tests
{
    class DLTests
    {
        private readonly DbContextOptions<PostDB> options;

        public DLTests()
        {
            options = new DbContextOptionsBuilder<PostDB>().UseSqlite("Filename=Test.db").Options;

            Seed();
        }

        private void Seed()
        {
            using (var context = new PostDB(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Roots.AddRange(
                    new List<Root>()
                    {
                        new Root()
                        {
                            Id = 1,
                            Title = "TheTitle",
                            Message = "a testing message",
                            TotalVote = 1,
                            DateTime = new DateTime(2021, 1, 1, 1, 1, 1),
                            UserName = "testuser"
                        },
                        new Root()
                        {
                            Id = 2,
                            Title = "AnotherTitle",
                            Message = "am testing things",
                            TotalVote = -1,
                            DateTime = new DateTime(2021, 1, 1, 1, 1, 1),
                            UserName = "testuser"
                        },
                        new Root()
                        {
                            Id = 3,
                            Title = "The FINAL Title",
                            Message = "a testing last message",
                            TotalVote = 1,
                            DateTime = new DateTime(2021, 1, 1, 1, 1, 1),
                            UserName = "testuser"
                        }
                    });

                context.Comments.AddRange(
                    new List<Comment>()
                    {
                        new Comment()
                        {
                            Id = 10,
                            Message = "comment to somthing",
                            TotalVote = 123,
                            DateTime = new DateTime(2021, 1, 1, 1, 1, 1),
                            UserName = "SomeGuy"
                        },
                        new Comment()
                        {
                            Id = 11,
                            Message = "another comment to somthing",
                            TotalVote = 321,
                            DateTime = new DateTime(2021, 1, 1, 1, 1, 1),
                            UserName = "idkSomeGuy"
                        },
                        new Comment()
                        {
                            Id = 12,
                            Message = "and another one comment to somthing",
                            TotalVote = -123,
                            DateTime = new DateTime(2021, 1, 1, 1, 1, 1),
                            UserName = "wowSomeGuy"
                        }
                    });

                context.Votes.AddRange(
                    new List<Vote>()
                    {
                        new Vote()
                        {
                            Id = 100,
                            UserName = "guyorgirl1",
                            Value = 1
                        },
                        new Vote()
                        {
                            Id = 101,
                            UserName = "guyorgirl2",
                            Value = 1
                        },
                        new Vote()
                        {
                            Id = 102,
                            UserName = "guyorgirl3",
                            Value = -1
                        }
                    });
                context.SaveChanges();
            }
    }

}
}

