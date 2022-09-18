using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    public class Languages
    {
        public int Id { get; set; }
        public string Language { get; set; }
        public ICollection<Books> Books { get; set; } = new HashSet<Books>();
        
    }
}
