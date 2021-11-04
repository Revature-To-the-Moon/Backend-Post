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
    public class DLTests
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

        [Fact]
        public void AddingRootShouldAddRoot()
        {
            using (var context = new PostDB(options))
            {
                IRepo repo = new DBRepo(context);
                Root rootToAdd = new Root()
                {
                    Id = 1000,
                    Title = "add TheTitle",
                    Message = "add a testing message",
                    TotalVote = 1,
                    DateTime = new DateTime(2021, 1, 1, 1, 1, 1),
                    UserName = "addtestuser"
                };

                repo.AddRootAsync(rootToAdd);
            }

            using (var context = new PostDB(options))
            {
                Root root = context.Roots.FirstOrDefault(u => u.Id == 1000);

                Assert.NotNull(root);
                Assert.Equal("add TheTitle", root.Title);
                Assert.Equal("add a testing message", root.Message);
                Assert.Equal(1, root.TotalVote);
                Assert.Equal(new DateTime(2021, 1, 1, 1, 1, 1), root.DateTime);
                Assert.Equal("addtestuser", root.UserName);
            }
        }

        [Fact]
        public void AddingCommentShouldAddComment()
        {
            using (var context = new PostDB(options))
            {
                IRepo repo = new DBRepo(context);
                Comment commentToAdd = new Comment()
                {
                    Id = 1001,
                    Message = "add a testing message",
                    TotalVote = 1,
                    DateTime = new DateTime(2021, 1, 1, 1, 1, 1),
                    UserName = "addtestuser"
                };

                repo.AddCommentAsync(commentToAdd);
            }

            using (var context = new PostDB(options))
            {
                Comment comment = context.Comments.FirstOrDefault(u => u.Id == 1001);

                Assert.NotNull(comment);
                Assert.Equal("add a testing message", comment.Message);
                Assert.Equal(1, comment.TotalVote);
                Assert.Equal(new DateTime(2021, 1, 1, 1, 1, 1), comment.DateTime);
                Assert.Equal("addtestuser", comment.UserName);
            }
        }

        [Fact]
        public void AddingVoteShouldAddVote()
        {
            using (var context = new PostDB(options))
            {
                IRepo repo = new DBRepo(context);
                Vote voteToAdd = new Vote()
                {
                    Id = 1002,
                    Value = 1,
                    UserName = "addtestuser"
                };

                repo.AddVoteAsync(voteToAdd);
            }

            using (var context = new PostDB(options))
            {
                Vote vote = context.Votes.FirstOrDefault(u => u.Id == 1002);

                Assert.NotNull(vote);
                Assert.Equal(1, vote.Value);
                Assert.Equal("addtestuser", vote.UserName);
            }
        }

        [Fact]
        public async void GetAllRootsShouldGetAllRoots()
        {
            using (var context = new PostDB(options))
            {
                IRepo repo = new DBRepo(context);

                var roots = await repo.GetRootListAsync();

                Assert.NotNull(roots);
                Assert.Equal(3, roots.Count);
            }
        }

        [Fact]
        public async void GetAllCommetsShouldGetAllComments()
        {
            using (var context = new PostDB(options))
            {
                IRepo repo = new DBRepo(context);

                var comments = await repo.GetCommentListAsync();

                Assert.NotNull(comments);
                Assert.Equal(3, comments.Count);
            }
        }

        [Fact]
        public async void GetAllVoteShouldGetAllVotes()
        {
            using (var context = new PostDB(options))
            {
                IRepo repo = new DBRepo(context);

                var votes = await repo.GetVoteListAsync();

                Assert.NotNull(votes);
                Assert.Equal(3, votes.Count);
            }
        }

        [Fact]
        public async void UpdatingRootShouldUpdate()
        {
            using (var context = new PostDB(options))
            {

                IRepo repo = new DBRepo(context);
                Root rootToUpdate = await repo.GetRootByIdAsync(2);

                rootToUpdate.Title = "newUpdatedTitle";


                await repo.UpdateRootAsync(rootToUpdate);
            }

            using (var context = new PostDB(options))
            {

                Root root = context.Roots.FirstOrDefault(e => e.Id == 2);

                Assert.NotNull(root);
                Assert.Equal("newUpdatedTitle", root.Title);
            }
        }

        [Fact]
        public async void UpdatingCommentShouldUpdate()
        {
            using (var context = new PostDB(options))
            {

                IRepo repo = new DBRepo(context);
                Comment commentToUpdate = await repo.GetCommentByIdAsync(11);

                commentToUpdate.Message = "newUpdatedMessage";


                await repo.UpdateCommentAsync(commentToUpdate);
            }

            using (var context = new PostDB(options))
            {

                Comment comment = context.Comments.FirstOrDefault(e => e.Id == 11);

                Assert.NotNull(comment);
                Assert.Equal("newUpdatedMessage", comment.Message);
            }
        }

        [Fact]
        public async void UpdatingVoteShouldUpdate()
        {
            using (var context = new PostDB(options))
            {

                IRepo repo = new DBRepo(context);
                Vote voteToUpdate = await repo.GetVoteByIdAsync(100);

                voteToUpdate.Value = 42;


                await repo.UpdateVoteAsync(voteToUpdate);
            }

            using (var context = new PostDB(options))
            {
                Vote vote = context.Votes.FirstOrDefault(e => e.Id == 100);

                Assert.NotNull(vote);
                Assert.Equal(42, vote.Value);
            }
        }
    }
}

