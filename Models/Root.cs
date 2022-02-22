using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Root
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public int TotalVote { get; set; } //Total number of votes or total sum of vote scores?
        public DateTime DateTime { get; set; }
        public string UserName { get; set; }
        public List<Comment> Comments { get; set; }

        //Determine if post is group post or normal 
        //If GroupPostId is > 0 it belongs to a group and the value specifies the group Id 
        //[No Group has 0 or null Id]
        public int GroupPostId { get; set; } //Community/Group daily stories

        public Root (int GroupPostId)
        {
            this.GroupPostId = 0;
        } 
    }
}