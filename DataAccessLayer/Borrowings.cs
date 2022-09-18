using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class Borrowings
    {
        public int Id { get; set; }
        public int StudentId { get; set; }        
        public int BookId { get; set; }
        [DataType(DataType.Date)]
        public DateTime RetriveDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime SubmitDate { get; set; }
        public Students Student { get; set; }
        public Books Book { get; set; }
    }
}
