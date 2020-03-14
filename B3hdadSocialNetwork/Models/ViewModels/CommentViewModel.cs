using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace B3hdadSocialNetwork.Models.ViewModels
{
    public class CommentViewModel
    {
        public int Post_Id { get; set; }
        public string User_Id { get; set; }
        public string Text { get; set; }
    }
}
