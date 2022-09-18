using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    public class Admins
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AdminImage { get; set; }
    }
}
