using System.ComponentModel.DataAnnotations;


namespace login_registration.Models
{
    public class Message
    {
        [Display(Name = "Post a message")]
        public string message  { get; set; }
        public int user_id { get;set; }

    }
}