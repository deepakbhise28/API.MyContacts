using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace API.Entities
{
    [DataContract]
    public class Contact
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Minimum 2 and maximum 50 letters are allowed.", MinimumLength = 2)]
        [RegularExpression("^[a-zA-Z]+((['][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Valid Charactors include (A-Z) (a-z) (')")]
        [DataMember(Name = "firstName", EmitDefaultValue = false)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Minimum 2 and maximum 50 letters are allowed.", MinimumLength = 2)]
        [RegularExpression("^[a-zA-Z]+((['][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Valid Charactors include (A-Z) (a-z) (')")]
        [DataMember(Name = "lastName", EmitDefaultValue = false)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [DataMember(Name = "email", EmitDefaultValue = false)]
        public string Email { get; set; }

        [Required]
        [DataMember(Name = "phoneNumber", EmitDefaultValue = false)]
        [StringLength(20, ErrorMessage = "Minimum 10 and maximum 20 letters are allowed.", MinimumLength = 10)]
        public string PhoneNumber { get; set; }

        [Required]
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public bool Status { get; set; }

        public string UserId { get; set; }

    }
}
