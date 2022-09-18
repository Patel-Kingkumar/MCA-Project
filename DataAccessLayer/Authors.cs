using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLayer
{
    public class Authors
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Books> Books { get; set; } = new HashSet<Books>();
    }
}
