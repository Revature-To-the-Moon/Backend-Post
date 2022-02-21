using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int ParentId { get; set; } //Id of post this comment is a reply to
        public int RootId { get; set; } //Id of original post
        public string Message { get; set; }
        public int TotalVote { get; set; }
        public DateTime DateTime { get; set; }
        public string UserName { get; set; }
        public List<Vote> Votes { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
