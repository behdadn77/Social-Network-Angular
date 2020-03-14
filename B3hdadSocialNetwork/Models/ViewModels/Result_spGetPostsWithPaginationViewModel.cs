using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace B3hdadSocialNetwork.Model.ViewModels
{
    public class Result_spGetPostsWithPaginationViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string ImageBase64 { get; set; }
        public DateTime DateTime { get; set; }
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string User_Id { get; set; }
        public bool IsPostOwner { get; set; }
    }
}
