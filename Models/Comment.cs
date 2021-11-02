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
        public string Message { get; set; }
        public int TotalVote { get; set; }
        public DateTime DateTime { get; set; }
        public string UserName { get; set; }
        public List<Vote> Votes { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
