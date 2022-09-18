using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management.ViewModels
{
    public class BookImageViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string BookName { get; set; }
        public int AuthorId { get; set; }
        public string Publisher { get; set; }
        public int Quantity { get; set; }
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }
        public IFormFile BookNameImage { get; set; }
        public int PageNo { get; set; }
        public int LanguageId { get; set; }
        public string Description { get; set; }
        public IList<Authors> Author { get; set; }
        public Languages Language { get; set; }
    }
}
