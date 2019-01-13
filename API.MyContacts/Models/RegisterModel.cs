using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace API.MyContacts.Models
{

    [DataContract]
    public class RegisterModel
    {

        [Required]
        [DataMember(Name = "userName")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [DataMember(Name = "email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [DataMember(Name = "password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [DataMember(Name = "confirmPassword")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [DataMember(Name = "hometown")]
        public string Hometown { get; set; }
    }
}