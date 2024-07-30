using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization;

namespace ServeBook_Backend.Models
{
    public class User
    {
        [Key]
        /* ------- */
        public int id_user {get; set;}

        /* ------ */
        [Required(ErrorMessage = "The name of the user is required.")]
        [MinLength(1, ErrorMessage = "name must be at least {1} characters.")]
        [MaxLength(100, ErrorMessage = "name must be at most {1} characters.")]
        public string name {get; set;}

        /* ------ */
        [Required(ErrorMessage = "The email of the user is required.")]
        [MinLength(1, ErrorMessage = "email must be at least {1} characters.")]
        [MaxLength(150, ErrorMessage = "email must be at most {1} characters.")]
        public string email {get; set;}

        /* ------ */
        [Required(ErrorMessage = "The password of the user is required.")]
        [MinLength(1, ErrorMessage = "password must be at least {1} characters.")]
        [MaxLength(100, ErrorMessage = "password must be at most {1} characters.")]
        public string password {get; set;}  

        /* ------ */
        [Required(ErrorMessage = "The rol is required.")]
        [MinLength(1, ErrorMessage = "rol must be at least {1} characters.")]
        [MaxLength(100, ErrorMessage = "rol must be at most {1} characters.")]
        public string rol {get; set;}
        [JsonIgnore]
        public List<Loan> Loans { get; set; }   
    }
}