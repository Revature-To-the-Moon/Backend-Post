using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using Models;
using Microsoft.EntityFrameworkCore;

namespace DL
{
    public class DBRepo : IRepo
    {
       readonly private PostDB _context;

        public DBRepo(PostDB context)
        {
            _context = context;
        }


        //------------------------------------Methods For Getting List--------------------------------------

        public async Task<List<Root>> GetRootListAsync()
        {
            List<Root> roots = await _context.Roots
                .Include(r => r.Comments)
                .Select(r => new Root()
                {
                    Id = r.Id,
                    DateTime = r.DateTime,
                    Message = r.Message,
                    Title = r.Title,
                    TotalVote = r.TotalVote,
                    UserName = r.UserName,

                    Comments = r.Comments.Where(r => r.ParentId == -1).Select(a => new Comment()
                    {
                        Id = a.Id,
                        ParentId = a.ParentId,
                        RootId = a.RootId,
                        DateTime = a.DateTime,
                        Message = a.Message,
                        TotalVote = a.TotalVote,
                        UserName = a.UserName,

                        Votes = a.Votes.Select(b => new Vote()
                        {
                            Id = b.Id,
                            UserName = b.UserName,
                            Value = b.Value

                        }).ToList()

                    }).ToList(),

                }).ToListAsync();

            foreach (Root r in roots)
            {
                foreach(Comment com in r.Comments)
                {
                    com.Comments = await GetCommentChildAsync(com);
                }
            }

            return roots;
        }

        public async Task<List<Comment>> GetCommentListAsync()
        {
            List<Comment> comment = await _context.Comments
                .Where(r => r.ParentId == -1)
                .Select(r => new Comment()
                {
                    Id = r.Id,
                    ParentId = r.ParentId,
                    RootId = r.RootId,
                    DateTime = r.DateTime,
                    Message = r.Message,
                    TotalVote = r.TotalVote,
                    UserName = r.UserName,

                    Votes = r.Votes.Select(a => new Vote()
                    {
                        Id = a.Id,
                        UserName = a.UserName,
                        Value = a.Value,
                        CommentId = a.CommentId

                    }).ToList()
                }).ToListAsync();

            foreach(Comment com in comment)
            {
                com.Comments = await GetCommentChildAsync(com);
            }

            return comment;
        }

        public async Task<List<Comment>> GetCommentChildAsync(Comment comment)
        {
            List<Comment> comments = await _context.Comments
                .Where(r => r.ParentId == comment.Id)
                .Select(r => new Comment()
                {
                    Id = r.Id,
                    ParentId = r.ParentId,
                    RootId = r.RootId,
                    DateTime = r.DateTime,
                    Message = r.Message,
                    TotalVote = r.TotalVote,
                    UserName = r.UserName,

                    Votes = r.Votes.Select(a => new Vote()
                    {
                        Id = a.Id,
                        UserName = a.UserName,
                        Value = a.Value,
                        CommentId = a.CommentId

                    }).ToList()
                }).ToListAsync();

            foreach (Comment com in comments)
            {
                com.Comments = await GetCommentChildAsync(com);
            }

            return comments;
        }

        public async Task<List<Vote>> GetVoteListAsync()
        {
            return await _context.Votes
                .Select(r => new Vote()
                {
                    Id = r.Id,
                    UserName = r.UserName,
                    Value = r.Value,
                    CommentId = r.CommentId

                }).ToListAsync();
        }

        //------------------------------------Methods For Getting Data by Id--------------------------------

        public async Task<Root> GetRootByIdAsync(int id)
        {
            Root aRoot = await _context.Roots
                .Include(r => r.Comments)
                .AsNoTracking()
                .Select(r => new Root()
                {
                    Id = r.Id,
                    DateTime = r.DateTime,
                    Message = r.Message,
                    Title = r.Title,
                    TotalVote = r.TotalVote,
                    UserName = r.UserName,

                    Comments = r.Comments.Where(r => r.ParentId == -1).Select(a => new Comment()
                    {
                        Id = a.Id,
                        ParentId = a.ParentId,
                        RootId = a.RootId,
                        DateTime = a.DateTime,
                        Message = a.Message,
                        TotalVote = a.TotalVote,
                        UserName = a.UserName,

                        Votes = a.Votes.Select(b => new Vote()
                        {
                            Id = b.Id,
                            UserName = b.UserName,
                            Value = b.Value,
                            CommentId = b.CommentId

                        }).ToList()

                    }).ToList(),

                })
                .FirstOrDefaultAsync(r => r.Id == id);

            foreach (Comment com in aRoot.Comments)
            {
                com.Comments = await GetCommentChildAsync(com);
            }

            return aRoot;
        }

        public async Task<Comment> GetCommentByIdAsync(int id)
        {
            Comment aComment = await _context.Comments
                .AsNoTracking()
                .Select(r => new Comment()
                {
                    Id = r.Id,
                    ParentId = r.ParentId,
                    RootId = r.RootId,
                    DateTime = r.DateTime,
                    Message = r.Message,
                    TotalVote = r.TotalVote,
                    UserName = r.UserName,

                    Votes = r.Votes.Select(a => new Vote()
                    {
                        Id = a.Id,
                        UserName = a.UserName,
                        Value = a.Value,
                        CommentId = a.CommentId

                    }).ToList()

                }).FirstOrDefaultAsync(r => r.Id == id);
            aComment.Comments = await GetCommentChildAsync(aComment);
            return aComment;
        }

        public async Task<Vote> GetVoteByIdAsync(int id)
        {
            return await _context.Votes
                .AsNoTracking()
                .Select(r => new Vote()
                {
                    Id = r.Id,
                    UserName = r.UserName,
                    Value = r.Value,
                    CommentId = r.CommentId

                }).FirstOrDefaultAsync(r => r.Id == id);
        }

        //------------------------------------Methods for Adding To DB--------------------------------------

        public async Task<Root> AddRootAsync(Root root)
        {
            await _context.AddAsync(root);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
            return root;
        }

        public async Task<Comment> AddCommentAsync(Comment comment)
        {
            await _context.AddAsync(comment);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
            return comment;
        }

        public async Task<Vote> AddVoteAsync(Vote vote)
        {
            await _context.AddAsync(vote);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            bool positive = true;
            if (vote.Value == -1)
            {
                positive = false;
            }
            Comment com = await GetCommentByIdAsync(vote.CommentId);
            Root root = await GetRootByIdAsync(com.RootId);

            if (positive)
            {
                root.TotalVote++;
            }
            else
            {
                root.TotalVote--;
            }
            _context.Roots.Update(root);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            await UpdateTotalvote(com, positive);

            return vote;
        }
        

        //------------------------------------Methods for Updating DB--------------------------------------

        public async Task<Root> UpdateRootAsync(Root root)
        {
            _context.Roots.Update(root);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            return new Root()
            {
                Id = root.Id,
                DateTime = root.DateTime,
                Message = root.Message,
                Title = root.Title,
                TotalVote = root.TotalVote,
                UserName = root.UserName
            };
        }

        public async Task<Comment> UpdateCommentAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            return new Comment()
            {
                Id = comment.Id,
                ParentId = comment.ParentId,
                RootId = comment.RootId,
                DateTime = comment.DateTime,
                Message = comment.Message,
                TotalVote = comment.TotalVote,
                UserName = comment.UserName,
                Votes = comment.Votes,
            };
        }

        public async Task<Vote> UpdateVoteAsync(Vote vote)
        {
            _context.Votes.Update(vote);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            bool positive = true;
            if (vote.Value == -1)
            {
                positive = false;
            }
            Comment com = await GetCommentByIdAsync(vote.CommentId);
            Root root = await GetRootByIdAsync(com.RootId);
            
            if (positive)
            {
                root.TotalVote = root.TotalVote+2;
            }
            else
            {
                root.TotalVote = root.TotalVote-2;
            }

            _context.Roots.Update(root);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            await UpdateTotalvote(com, positive);
            await UpdateTotalvote(com, positive);


            return new Vote()
            {
                Id = vote.Id,
                UserName = vote.UserName,
                Value = vote.Value,
                CommentId = vote.CommentId
            };
        }

        /// <summary>
        /// recursion for changing totalvotes when vote is added
        /// if positive is true will add 1 to the total value of a comment
        /// and then call the method again with the parent comment if there is one
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="positive"></param>
        /// <returns></returns>
        public async Task UpdateTotalvote(Comment comment, bool positive)
        {
            if (positive)
            {
                comment.TotalVote++;
            }
            else
            {
                comment.TotalVote--;
            }

            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            System.Diagnostics.Debug.WriteLine(_context.ChangeTracker.DebugView.LongView);
            
            

            if (comment.ParentId != -1)
            {
                Comment temp = await GetCommentByIdAsync(comment.ParentId);
                await UpdateTotalvote(temp, positive);
            }

        }

        //------------------------------------Methods for Deleting From DB---------------------------------

        public async Task DeleteRootAsync(int id)
        {
            _context.Roots.Remove(await GetRootByIdAsync(id));
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
        }

        public async Task DeleteCommentAsync(int id)
        {
            _context.Comments.Remove(await GetCommentByIdAsync(id));
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
        }

        public async Task DeleteVoteAsync(int id)
        {
            Vote vote = await GetVoteByIdAsync(id);
            _context.Votes.Remove(vote);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            // switched if statement because we are deleting and need to remove the value
            bool positive = true;
            if (vote.Value == 1)
            {
                positive = false;
            }
            Comment com = await GetCommentByIdAsync(vote.CommentId);
            Root root = await GetRootByIdAsync(com.RootId);
            
            if (positive)
            {
                root.TotalVote++;
            }
            else
            {
                root.TotalVote--;
            }

            _context.Roots.Update(root);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            await UpdateTotalvote(com, positive);
        }



        
    }

}



