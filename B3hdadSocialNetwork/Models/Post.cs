using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace B3hdadSocialNetwork.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public byte[] Image { get; set; }
        public string User_Id { get; set; }
        public DateTime DateTime { get; set; }
        public ICollection<Comment>Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
    }
}
