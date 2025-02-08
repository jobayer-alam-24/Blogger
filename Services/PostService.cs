using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blogger.Data;
using Blogger.Enums;
using Blogger.Models;

namespace Blogger.Services
{
    public class PostService
    {
        private static List<Post> _posts = new List<Post>(){
                new Post(){
                    Id = 1,
                    Title = "First Post",
                    Content = "This is the first post lg",
                    Slug = "first-post",
                    CategoryId = 1,
                    UserId = 1,
                    Media = "/images/default-post.jpg",
                    Status = Status.Draft
                },
                new Post(){
                    Id = 2,
                    Title = "Second Post",
                    Content = "This is the second post kh",
                    Slug = "second-post",
                    CategoryId = 1,
                    UserId = 1,
                    Media = "/images/default-post.jpg",
                    Status = Status.Scheduled
                },
                new Post(){
                    Id = 3,
                    Title = "Third Post",
                    Content = "This is the third post gf",
                    Slug = "third-post",
                    CategoryId = 1,
                    UserId = 1,
                    Media = "/images/default-post.jpg",
                    Status = Status.Default
                },
                new Post(){
                    Id = 4,
                    Title = "Fourth Post",
                    Content = "This is the fourth post ty",
                    Slug = "fourth-post",
                    CategoryId = 1,
                    UserId = 1,
                    Media = "/images/default-post.jpg",
                    Status = Status.Default
                },
                new Post(){
                    Id = 5,
                    Title = "Fifth Post",
                    Content = "This is the fifth post uiy",
                    Slug = "fifth-post",
                    CategoryId = 1,
                    UserId = 1,
                    Media = "/images/default-post.jpg",
                    Status = Status.Default
                }
            };
        public static List<Post> GetAllPosts()
        {
            return _posts;
        }
    }
}