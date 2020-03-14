using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using B3hdadSocialNetwork.Areas.Identity.Data;
using B3hdadSocialNetwork.Model.ViewModels;
using B3hdadSocialNetwork.Models;
using B3hdadSocialNetwork.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace B3hdadSocialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeLinePostController : Controller
    {
        ApplicationContext db;
        UserManager<ApplicationUser> userManager;
        public TimeLinePostController(ApplicationContext _db, UserManager<ApplicationUser> _userManager)
        {
            db = _db;
            userManager = _userManager;
        }
        // GET: api/TimeLinePost/GetList/1
        [HttpGet("[action]/{page}")]
        public async Task<List<Result_spGetPostsWithPaginationViewModel>> GetList(int page = 0, int pageSize = 10)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var res = db.spGetPostsWithPaginations.FromSql(
                $@"spGetPostsWithPagination @pagenumber={page}, @pagesize={pageSize}")
                    .Select(x => new Result_spGetPostsWithPaginationViewModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Body = x.Body,
                        DateTime = x.DateTime,
                        LikeCount = x.LikeCount,
                        CommentCount = x.CommentCount,
                        UserFirstName = x.UserFirstName,
                        UserLastName = x.UserLastName,
                        ImageBase64 = x.Image != null ? Convert.ToBase64String(x.Image) : null,
                        User_Id = x.User_Id,
                        IsPostOwner = false
                    }).ToList();
            if (user!=null)
            {
                foreach (var item in res)
                {
                    item.IsPostOwner= user.Id == item.User_Id ? true : false;
                }
            }
            return res;
        }

        //GET: api/TimeLinePost/5
        //[HttpGet("{id}", Name = "Get")]
        //public Post Get(int id)
        //{
        //    return db.Posts.Single(x => x.Id == id);
        //}

        // POST: api/TimeLinePost
        [HttpPost]
        [Authorize]
        public async Task<bool> Post([FromForm]PostViewModel value)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            Post post = new Post()
            {
                Title = value.Title,
                Body = value.Body,
                DateTime = DateTime.Now,
                User_Id = user.Id
            };
            if (value.Image != null)
            {
                var filename = System.IO.Path.GetExtension(value.Image.FileName).ToLower();
                if (filename == ".jpg" || filename == ".png" || filename == ".jfif" || filename == ".gif")
                {
                    byte[] b = new byte[value.Image.Length];
                    value.Image.OpenReadStream().Read(b, 0, b.Length);
                    post.Image = b;
                    db.Posts.Add(post);
                    db.SaveChanges();
                }
                else
                {
                    return false;
                }

            }
            else
            {
                db.Posts.Add(post);
                db.SaveChanges();
            }
            return true;
        }

        // PUT: api/TimeLinePost/5
        [HttpPut("{id}")]
        public bool Put(int id, [FromBody] PostViewModel value)
        {
            var post = db.Posts.Single(x => x.Id == id);
            post.Title = value.Title;
            post.Body = value.Body;
            if (value.Image != null)
            {
                var filename = System.IO.Path.GetExtension(value.Image.FileName).ToLower();
                if (filename == ".jpg" || filename == ".png" || filename == ".jfif" || filename == ".gif")
                {
                    byte[] b = new byte[value.Image.Length];
                    value.Image.OpenReadStream().Read(b, 0, b.Length);
                    post.Image = b;
                    db.SaveChanges();
                }
                else
                {
                    return false;
                }
            }
            else
            {
                db.SaveChanges();
            }
            return true;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            var post = db.Posts.Single(x => x.Id == id);
            if (post.User_Id == (await userManager.GetUserAsync(HttpContext.User)).Id)
            {
                db.Remove(post);
                if (db.SaveChanges()!=0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
