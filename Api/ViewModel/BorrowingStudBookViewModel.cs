using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ViewModel
{
    public class BorrowingStudBookViewModel
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
        public string StudentName { get; set; }
        public string Books { get; set; }
        public Students Student { get; set; }
        public Books Book { get; set; }
    }
}
