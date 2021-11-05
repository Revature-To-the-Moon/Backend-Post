using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BL;
using Microsoft.AspNetCore.Mvc;
using Models;
using Moq;
using Newtonsoft.Json.Linq;
using WebAPI.Controllers;
using Xunit;

namespace Tests
    {
    public class ControllerTest
        {
        [Fact]
        public async Task GetPostShouldReturnListofRoot()
            {
            List<Root> mockRoot = new List<Root>()
                    {
                    new Root()
                        {
                        Id = 1,
                        Title = "Test1",
                        Message = "my 1st post",
                        TotalVote = 0,
                        DateTime = DateTime.Now,
                        Comments = new List<Comment>()
                            {
                            new Comment()
                                {
                                Id =10,
                                Message = "my 1st Comment",
                                DateTime = DateTime.Now,
                                UserName = "McTesterson",
                                Votes = null,
                                Comments = null
                                }
                            }

                        },
                        new Root()
                        {
                        Id = 2,
                        Title = "Testy",
                        Message = "my 2st post",
                        TotalVote = 0,
                        DateTime = DateTime.Now,
                        Comments = new List<Comment>()
                            {
                            new Comment()
                                {
                                Id =30,
                                Message = "my 2st Comment",
                                DateTime = DateTime.Now,
                                UserName = "Testovich",
                                Votes = null,
                                Comments = null
                                }
                            }
                        }
                    };
            var mockBL = new Mock<IBL>();
            mockBL.Setup(x => x.GetRootListAsync()).ReturnsAsync(mockRoot);


            PostController service = new PostController(mockBL.Object);
            var result = await service.Get() as ObjectResult;
            var actualResult = result.Value;
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
            Assert.Equal(2, mockRoot.Count);
            }
        [Fact]
        public async Task GetPostbyIdShouldReturnRoot()
            {
            Root mockRoot = new Root()
                        {
                        Id = 1,
                        Title = "Test1",
                        Message = "my 1st post",
                        TotalVote = 1,
                        DateTime = DateTime.Now,
                        Comments = new List<Comment>()
                            {
                            new Comment()
                                {
                                Id =10,
                                Message = "my 1st Comment",
                                DateTime = DateTime.Now,
                                UserName = "McTesterson",
                                Votes = new List<Vote>()
                                    {
                                    new Vote ()
                                         {
                                        Id = 5,
                                        UserName = "Bob",
                                        Value = 1
                                        }
                                    }
                                }

                            }
                        };
            var mockBL = new Mock<IBL>();
            mockBL.Setup(x => x.GetRootByIdAsync(1)).ReturnsAsync(mockRoot);


            PostController service = new PostController(mockBL.Object);
            var result = await service.Get(1) as ObjectResult;
            var actualResult = result.Value;
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
            Assert.Equal(1, mockRoot.Id);
            Assert.Equal("Test1", mockRoot.Title);
            Assert.Single(mockRoot.Comments);
            Assert.Equal(mockRoot, actualResult);
           
            }
        [Fact]
        public async Task AddRootShouldReturnRoot()
            {
            Root mockRoot = new Root()
                {
                Id = 1,
                Title = "Test1",
                Message = "my 1st post",
                TotalVote = 1,
                DateTime = DateTime.Now,
                Comments = new List<Comment>()
                            {
                            new Comment()
                                {
                                Id =10,
                                Message = "my 1st Comment",
                                DateTime = DateTime.Now,
                                UserName = "McTesterson",
                                Votes = new List<Vote>()
                                    {
                                    new Vote ()
                                         {
                                        Id = 5,
                                        UserName = "Bob",
                                        Value = 1
                                        }
                                    }
                                }

                            }
                };
            var mockBL = new Mock<IBL>();
            mockBL.Setup(x => x.AddRootAsync(mockRoot)).ReturnsAsync(mockRoot);


            PostController service = new PostController(mockBL.Object);
            var result = await service.Post(mockRoot) as ObjectResult;
            var actualResult = result.Value;
            Assert.IsType<CreatedResult>(result);
            Assert.Equal(HttpStatusCode.Created, (HttpStatusCode)result.StatusCode);
            Assert.Equal(mockRoot, actualResult);
           

            }
        [Fact]
        public async Task UpdateRootShouldReturnRoot()
            {
            Root mockRoot = new Root()
                {
                Id = 1,
                Title = "Test1",
                Message = "my 1st post",
                TotalVote = 1,
                DateTime = DateTime.Now,
                Comments = new List<Comment>()
                            {
                            new Comment()
                                {
                                Id =10,
                                Message = "my 1st Comment",
                                DateTime = DateTime.Now,
                                UserName = "McTesterson",
                                Votes = new List<Vote>()
                                    {
                                    new Vote ()
                                         {
                                        Id = 5,
                                        UserName = "Bob",
                                        Value = 1
                                        }
                                    }
                                }

                            }
                };
            var mockBL = new Mock<IBL>();
            mockBL.Setup(x => x.UpdateRootAsync(mockRoot)).ReturnsAsync(mockRoot);


            PostController service = new PostController(mockBL.Object);
            var result = await service.Put(mockRoot) as ObjectResult;
            var actualResult = result.Value;
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
            Assert.Equal(mockRoot, actualResult);
            }
        [Fact]
        public async Task DelteRootShouldReturnOKandDelteRoot()
            {
            Root mockRoot = new Root()
                {
                Id = 1,
                Title = "Test1",
                Message = "my 1st post",
                TotalVote = 1,
                DateTime = DateTime.Now,
                Comments = new List<Comment>()
                            {
                            new Comment()
                                {
                                Id =10,
                                Message = "my 1st Comment",
                                DateTime = DateTime.Now,
                                UserName = "McTesterson",
                                Votes = new List<Vote>()
                                    {
                                    new Vote ()
                                         {
                                        Id = 5,
                                        UserName = "Bob",
                                        Value = 1
                                        }
                                    }
                                }

                            }
                };
            var mockBL = new Mock<IBL>();
            mockBL.Setup(x => x.DeleteRootAsync(1));


            PostController service = new PostController(mockBL.Object);
            var result = await service.Delete(1) as ObjectResult;
            var actualResult = result.Value;
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
            Assert.NotNull(result);
            }

        //*************************Vote Controller Tests
        [Fact]
        public async Task GetVoteShouldReturnListofVote()
            {
            List<Vote> mockVote = new List<Vote>()
                    {
                    new Vote()
                        {
                        Id = 1,
                        UserName = "Test1",
                        Value = 0
                        

                        },
                        new Vote()
                        {
                        Id = 2,
                        UserName = "Testy",
                        Value = -1
                        }
                    };
            var mockBL = new Mock<IBL>();
            mockBL.Setup(x => x.GetVoteListAsync()).ReturnsAsync(mockVote);


            VoteController service = new VoteController(mockBL.Object);
            var result = await service.Get() as ObjectResult;
            var actualResult = result.Value;
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
            Assert.Equal(2, mockVote.Count);
            }
        [Fact]
        public async Task GetVotebyIdShouldReturnVote()
            {
            Vote mockVote = new Vote()
                        {
                        Id = 1,
                        UserName = "Test1",
                        Value = 0
                        };
            var mockBL = new Mock<IBL>();
            mockBL.Setup(x => x.GetVoteByIdAsync(1)).ReturnsAsync(mockVote);


            VoteController service = new VoteController(mockBL.Object);
            var result = await service.Get(1) as ObjectResult;
            var actualResult = result.Value;
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
            Assert.Equal(1, mockVote.Id);
            Assert.Equal(mockVote, actualResult);

            }
        [Fact]
        public async Task AddVoteShouldReturnVote()
            {
            Vote mockVote = new Vote()
                {
                Id = 1,
                UserName = "Test1",
                Value = 0
                };
            var mockBL = new Mock<IBL>();
            mockBL.Setup(x => x.AddVoteAsync(mockVote)).ReturnsAsync(mockVote);


            VoteController service = new VoteController(mockBL.Object);
            var result = await service.Post(mockVote) as ObjectResult;
            var actualResult = result.Value;
            Assert.IsType<CreatedResult>(result);
            Assert.Equal(HttpStatusCode.Created, (HttpStatusCode)result.StatusCode);
            Assert.Equal(mockVote, actualResult);

            }

        [Fact]
        public async Task UpdateVoteShouldReturnVote()
            {
            Vote mockVote = new Vote()
                {
                Id = 1,
                UserName = "Test1",
                Value = 0
                };
            var mockBL = new Mock<IBL>();
            mockBL.Setup(x => x.UpdateVoteAsync(mockVote)).ReturnsAsync(mockVote);


            VoteController service = new VoteController(mockBL.Object);
            var result = await service.Put(mockVote) as ObjectResult;
            var actualResult = result.Value;
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
            Assert.Equal(mockVote, actualResult);
            }

        [Fact]
        public async Task DelteVoteShouldReturnOKandDelteVote()
            {
            Vote mockVote = new Vote()
                {
                Id = 1,
                UserName = "Test1",
                Value = 0
                };
            var mockBL = new Mock<IBL>();
            mockBL.Setup(x => x.DeleteVoteAsync(1));


            VoteController service = new VoteController(mockBL.Object);
            var result = await service.Delete(1) as ObjectResult;
            var actualResult = result.Value;
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
            Assert.NotNull(result);
            }


            //***************[Comment Controller Tests]********************
            [Fact]
            public async Task AddCommentShouldReturnComment()
            {
            Comment mockComment = new Comment()
            {
                Id = 1,
                Message = "A generic Message",
                TotalVote = 1,
                Comments = null,
                DateTime = DateTime.Now,
                UserName = "TestUser",
                Votes = null
            };
            var mockBL = new Mock<IBL>();
            mockBL.Setup(x => x.AddCommentAsync(mockComment)).ReturnsAsync(mockComment);


            CommentController service = new CommentController(mockBL.Object);
            var result = await service.Post(mockComment) as ObjectResult;
            var actualResult = result.Value;
            Assert.IsType<CreatedResult>(result);
            Assert.Equal(HttpStatusCode.Created, (HttpStatusCode)result.StatusCode);
            Assert.Equal(mockComment, actualResult);
            }

            [Fact]
            public async Task UpdateCommentShouldReturnComment()
            {
            Comment mockComment = new Comment()
            {
                Id = 1,
                Message = "A generic Message",
                TotalVote = 1,
                Comments = null,
                DateTime = DateTime.Now,
                UserName = "TestUser",
                Votes = null
            };
            var mockBL = new Mock<IBL>();
            mockBL.Setup(x => x.UpdateCommentAsync(mockComment)).ReturnsAsync(mockComment);


            CommentController service = new CommentController(mockBL.Object);
            var result = await service.Put(mockComment) as ObjectResult;
            var actualResult = result.Value;
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
            Assert.Equal(mockComment, actualResult);
            }
            
            [Fact]
            public async Task DeleteCommentShouldReturnOkAndDeleteComment()
            {
            Comment mockComment = new Comment()
            {
                Id = 1,
                Message = "A generic Message",
                TotalVote = 1,
                Comments = null,
                DateTime = DateTime.Now,
                UserName = "TestUser",
                Votes = null
            };
            var mockBL = new Mock<IBL>();
            mockBL.Setup(x => x.DeleteCommentAsync(mockComment.Id));

            CommentController service = new CommentController(mockBL.Object);
            
            var result = await service.Delete(mockComment.Id) as ObjectResult;
            var actualResult = result.Value;
            
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
            Assert.NotNull(result);
            }

        }
    }
    
