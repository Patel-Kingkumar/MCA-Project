
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    public class Students
    {
        public int Id { get; set; }        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string StudentImage { get; set; }
        //public IList<SelectListItem> BookName { get; set; }
        public ICollection<Borrowings> BookBorrowing { get; set; } = new HashSet<Borrowings>();

    }
}
