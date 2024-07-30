using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServeBook_Backend.Dtos
{
    public class LoanCreateDto
    {
        [Key]
        /* ------- */
        public int id_book {get; set;}
        
        /* ------ */
        public int userId {get; set;}

        /* ------ */
        public int bookId {get; set;}

        /* ------- */
        [Required(ErrorMessage = "The date of return of the loan is required.")]
        [DataType(DataType.Date)]
        public DateOnly dateReturn {get; set;}
    }
}