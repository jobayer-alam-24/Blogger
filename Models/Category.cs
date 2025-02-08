using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Blogger.Models
{
    public class Category : BaseEntity
    {
        
        [Range(1, int.MaxValue, ErrorMessage = "Parent ID MUST BE Positive!")]
        [Display(Name = "Parent Category")]
        public int? ParentId {get; set;}
        [Required(ErrorMessage = "You must have to provide Name!")]
        [StringLength(100, ErrorMessage = "Name can not be longer than 100 characters!")]
        public string Name {get; set;}

        public List<Post> Posts { get; set; } = new List<Post>();
    }
}