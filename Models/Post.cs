using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Blogger.Enums;
using Blogger.Helpers.CustomAttributes;
using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace Blogger.Models
{
    public class Post : BaseEntity
    {

        [StringLength(50, ErrorMessage = "Title must be between 5 and 50 characters.", MinimumLength = 5)]
        [Required(ErrorMessage = "Title is required.")]
        [Display(Name = "Enter The Blog Title")]
        public string Title { get; set; }

        [BindNever]
        public DateTime CreatedAt { get; set; }
        [BindNever]
        public DateTime UpdatedAt { get; set; }
        [BindNever]
        public DateTime PublishedAt { get; set; }
        [BindNever]

        //custom attrib - datenotinfuture, 
        //[DisplayName("Schedule Date")]
        //[DateNotInFuture]
        //public DateTime ScheduleDate {get; set;}
        //custom attrib - poster DOB 10 - 15 = accept 
        //[DisplayName("Enter Your Date Of Birth")]
        //public DateTime DateOfBirth {get; set;}
        //Navigation Key
        [Display(Name = "Select Category")]
        [Required(ErrorMessage = "Category ID is Required")]
        public int CategoryId { get; set; }
        //Navigation Property
        public Category Category { get; set; }
        [BindNever]
        public int UserId { get; set; }

        [MaxLength(50, ErrorMessage = "URL must be below 50 characters.")]
        public string Media { get; set; } 

        public Post()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            PublishedAt = DateTime.UtcNow;
            Status = Status.Default;
            UserId = 1;
            Media = "/images/default-post.jpg";
        }
    }
}

