using Blogger.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blogger.Models
{
    public class BaseEntity
    {
        [Key]
        [BindNever]
        public int Id { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        [MinLength(10, ErrorMessage = "Content must be above 10 characters!")]
        [DisplayName("Add Content")]
        public string Content { get; set; }
        [DisplayName("URL Slug")]
        public string Slug { get; set; }

        [Required(ErrorMessage = "Status Is Required!")]
        [DisplayName("Provide Status")]
        public Status Status { get; set; }
    }
}
