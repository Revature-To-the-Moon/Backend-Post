﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Microsoft.EntityFrameworkCore;

namespace DL
{
    public class DBRepo : IRepo
    {
        private PostDB _context;

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
                        Votes = a.Votes,

                        Comments = a.Comments.Select(b => new Comment()
                        {
                            Id = b.Id,
                            DateTime = b.DateTime,
                            Message = b.Message,
                            TotalVote = b.TotalVote,
                            UserName = b.UserName,
                            Votes = b.Votes,
                        }).ToList(),

                    }).ToList(),

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
                        Votes = a.Votes,

                        Comments = a.Comments.Select(b => new Comment()
                        {
                            Id = b.Id,
                            DateTime = b.DateTime,
                            Message = b.Message,
                            TotalVote = b.TotalVote,
                            UserName = b.UserName,
                            Votes = b.Votes,
                        }).ToList(),

                    }).ToList(),

                })
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        //------------------------------------Methods for Adding To DB--------------------------------------

        public async Task<Root> AddRootAsync(Root root)
        {
            await _context.AddAsync(root);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
            return root;
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

        //------------------------------------Methods for Deleting From DB---------------------------------

        public async Task DeleteRootAsync(int id)
        {
            _context.Roots.Remove(await GetRootByIdAsync(id));
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
        }
    }
}
