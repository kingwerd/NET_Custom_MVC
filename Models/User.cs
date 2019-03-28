using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomMVC.Models {

    public class User {

        [Key]
        public int UserId {get;set;}

        [Required]
        [MinLength(2)]
        [Display(Name="First Name")]
        public string FirstName {get;set;}

        [Required]
        [MinLength(2)]
        [Display(Name="Last Name")]
        public string LastName {get;set;}

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name="Email Address")]
        public string Email {get;set;}

        [Required]
        [DataType(DataType.Password)]
        public string Password {get;set;}

        [NotMapped]
        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [Display(Name="Password Confirmation")]
        public string Confirm {get;set;}

        public int UserLevel {get;set;} = 1;

        public DateTime CreatedAt {get;set;}
        public DateTime UpdatedAt {get;set;}

        public List<Post> Posts {get;set;}
    }
}