using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServeBook_Backend.Models
{
    public class Loan
    {
        [Key]
        /* ------- */
        public int id_loan {get; set;}
        
        /* ------ */
        public int userId {get; set;}
        public User User {get; set;}

        /* ------ */
        public int bookId {get; set;}
        /*==========================================================================*/
        public  Book Book { get; set; }
        /*===========================================================================*/

        /* ------- */
        [Required(ErrorMessage = "The date of creation of the loan is required.")]
        [DataType(DataType.DateTime)]
        public DateTime dateLoan {get; set;}

        /* ------- */
        [Required(ErrorMessage = "The date of return of the loan is required.")]
        [DataType(DataType.Date)]
        public DateOnly dateReturn {get; set;}

        /* ------- */
        [Required(ErrorMessage = "The status of the loan is required.")]
        public string status {get; set;}

        /*=======================================================*/
        





    }
}