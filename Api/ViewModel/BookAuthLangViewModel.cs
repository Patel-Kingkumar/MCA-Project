using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ViewModel
{
    public class BookAuthLangViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string BookName { get; set; }
        //public int AuthorId { get; set; }
        public string Publisher { get; set; }
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }
        public string BookImage { get; set; }
        //public int LanguageId { get; set; }
        public string AuthorName { get; set; }
        public int Quantity { get; set; }
        public string Languages { get; set; }
        public int PageNo { get; set; }
        public string Description { get; set; }
        public Authors Author { get; set; }
        public Languages Language { get; set; }
        public IEnumerable<Books> MyBooks { get; set; }
    }
}
