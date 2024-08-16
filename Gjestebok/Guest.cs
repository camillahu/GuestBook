using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook
{
    internal class Guest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsBookingName { get; set; }
        public int PartiId { get; set; }

        public Guest(string name)
        {
            Name = name;
        }

        public string GetName()
        {
            return Name;
        }
    }
}
