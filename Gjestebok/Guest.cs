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
        public int PartyId { get; set; }

        public int Id { get; }
    
        public Guest(string firstName, string lastName, bool isBookingName, int partyId, int id)
        {
            FirstName = firstName;
            LastName = lastName;
            IsBookingName = isBookingName;
            PartyId = partyId;
            Id = id;
        }
        public Guest(string firstName, string lastName, bool isBookingName, int partyId)
        {
            FirstName = firstName;
            LastName = lastName;
            IsBookingName = isBookingName;
            PartyId = partyId;
        }

        public void PrintInfo()
        {
            $"Name: {LastName}, {FirstName}".PrintStringToConsole();
        }

    }
}
