﻿using System;
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
            return await _context.Roots
                .Include(r => r.Comments)
                .Select(r => new Root()
                {
                    Id = r.Id,
                    DateTime = r.DateTime,
                    Message = r.Message,
                    Title = r.Title,
                    TotalVote = r.TotalVote,
                    UserName = r.UserName,

                    Comments = r.Comments.Select(a => new Comment()
                    {
                        Id = a.Id,
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
        }

        public async Task<List<Comment>> GetCommentListAsync()
        {
            return await _context.Comments
                .Select(r => new Comment()
                {
                    Id = r.Id,
                    DateTime = r.DateTime,
                    Message = r.Message,
                    TotalVote = r.TotalVote,
                    UserName = r.UserName,

                    Votes = r.Votes.Select(a => new Vote()
                    {
                        Id = a.Id,
                        UserName = a.UserName,
                        Value = a.Value

                    }).ToList()

                }).ToListAsync();
        }

        public async Task<List<Vote>> GetVoteListAsync()
        {
            return await _context.Votes
                .Select(r => new Vote()
                {
                    Id = r.Id,
                    UserName = r.UserName,
                    Value = r.Value

                }).ToListAsync();
        }

        //------------------------------------Methods For Getting Data by Id--------------------------------

        public async Task<Root> GetRootByIdAsync(int id)
        {
            return await _context.Roots
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

                    Comments = r.Comments.Select(a => new Comment()
                    {
                        Id = a.Id,
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

                })
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Comment> GetCommentByIdAsync(int id)
        {
            return await _context.Comments
                .AsNoTracking()
                .Select(r => new Comment()
                {
                    Id = r.Id,
                    DateTime = r.DateTime,
                    Message = r.Message,
                    TotalVote = r.TotalVote,
                    UserName = r.UserName,

                    Votes = r.Votes.Select(a => new Vote()
                    {
                        Id = a.Id,
                        UserName = a.UserName,
                        Value = a.Value

                    }).ToList()

                }).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Vote> GetVoteByIdAsync(int id)
        {
            return await _context.Votes
                .AsNoTracking()
                .Select(r => new Vote()
                {
                    Id = r.Id,
                    UserName = r.UserName,
                    Value = r.Value

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

            return new Vote()
            {
                Id = vote.Id,
                UserName = vote.UserName,
                Value = vote.Value
            };
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
            _context.Votes.Remove(await GetVoteByIdAsync(id));
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
        }
    }
}
