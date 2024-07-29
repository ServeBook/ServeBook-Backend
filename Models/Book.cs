using System.ComponentModel.DataAnnotations;

namespace ServeBook_Backend.Models
{
    public class Book
    {
        /* ------- */
        public int id_book {get; set;}

        /* ------ */
        [Required(ErrorMessage = "The title of the book is required.")]
        [MinLength(1, ErrorMessage = "title must be at least {1} characters.")]
        [MaxLength(50, ErrorMessage = "title must be at most {1} characters.")]
        public string title {get; set;}

        /* ------ */
        [Required(ErrorMessage = "The author of the book is required.")]
        [MinLength(1, ErrorMessage = "author must be at least {1} characters.")]
        [MaxLength(100, ErrorMessage = "author must be at most {1} characters.")]
        public string author {get; set;}

        /* ------ */
        public string? gender {get; set;}

        /* ------- */
        [Required(ErrorMessage = "The date of creation is required.")]
        [DataType(DataType.Date)]
        public DateTime datePublication {get; set;}

        /* ------- */
        public int copiesAvailable {get; set;}

        /* ------- */
        [Required(ErrorMessage = "The status of the book is required.")]
        public string status {get; set;}
        
    }
}