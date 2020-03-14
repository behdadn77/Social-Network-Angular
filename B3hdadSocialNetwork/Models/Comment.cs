using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace B3hdadSocialNetwork.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int Post_Id { get; set; }
        [ForeignKey("Post_Id")]
        public Post Post { get; set; }
        public string User_Id { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
    }
}
