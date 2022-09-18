using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer
{
    public class Books
    {     
        public int Id { get; set; }
        public string Title { get; set; }
        public string BookName { get; set; }
        public int AuthorId { get; set; }       
        public string Publisher { get; set; }
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }
        public int Quantity { get; set; }
        public string BookImage { get; set; }
        public int LanguageId { get; set; }
        public string Description { get; set; }
        public int PageNo { get; set; }
        public Authors Author { get; set; }
        public Languages Language { get; set; }
        public ICollection<Borrowings> BookBorrowing { get; set; } = new HashSet<Borrowings>();
        //public ICollection<Borrowings> Borrowings { get; set; } = new HashSet<Borrowings>();
    }
 }
