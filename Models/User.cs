using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Blogger.Models
{
    public class User
    {
        [Key]
        [BindNever]
        public int Id { get; set; }
        [Required(ErrorMessage = "First Name is Required")]
        [Display(Name = "First Name"), StringLength(50, ErrorMessage = "First name can not be above 50 characters")]
        public string FirstName {get; set;}
        [DisplayName("Middle Name"), StringLength(50, ErrorMessage = "Middle name can not be above 50 characters")]
        public string MiddleName {get; set;}
        [DisplayName("Last Name"), Required(ErrorMessage = "Last Name is Required"), StringLength(50, ErrorMessage = "Last name can not be above 50 characters")]
        public string LastName {get; set;}
        [DisplayName("Phone Number"), Required(ErrorMessage = "Phone Number is Required")]
        [Phone]
        public string Phone {get; set;}
        [Required(ErrorMessage = "Email is Required"), EmailAddress]
        public string Email {get; set;}
        [Required(ErrorMessage = "Password is Required"), MinLength(6)]
        public string Password {get; set;}
        public DateTime RejisteredAt {get; set;} = DateTime.Now;
        public DateTime LastLogin {get; set;} = DateTime.Now;
        [StringLength(500, ErrorMessage = "Intro is Too long.")]
        [DisplayName("Introduction")]
        public string Intro {get; set;}
        [DisplayName("Profile Picture")]
        public string Profile {get; set;}
        public User()
        {
            Profile = "/images/user.png";
            RejisteredAt = DateTime.Now;
            LastLogin = DateTime.Now;
        }
    }
}