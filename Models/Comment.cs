using System.ComponentModel.DataAnnotations;


namespace login_registration.Models
{
    public class Comment
    {
        [Display(Name = "Post a comment")]
        public string comment { get; set; }

        public int user_id { get; set; }

        public int message_id { get; set; }
    }
}