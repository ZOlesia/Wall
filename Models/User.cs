using System.ComponentModel.DataAnnotations;


namespace login_registration.Models
{
    public class User
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name - letters only, at least 2 characters")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First Name can only contain letters")]
        [MinLength(2)]
        public string first_name { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name - letters only, at least 2 characters")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last Name can only contain letters")]
        [MinLength(2)]
        public string last_name { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Valid Email format")]
        [RegularExpression(@"^[a-zA-Z0-9.+_-]+@[a-zA-Z0-9._-]+\.[a-zA-Z]+$")]
        [EmailAddress]
        public string email { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password - at least 8 characters")]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [Display(Name = "Confirm Password")]
        [MinLength(8)]
        [Compare("password", ErrorMessage = "Password don`t match")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password doesn`t match")]
        public string password_confirmation { get; set; }
    }
}


