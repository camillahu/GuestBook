using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook
{ 
    internal class Party
    {
        public string FirstName { get;}
        public string LastName { get;}
        public DateTime PartyDate { get; }
        public int NumOfGuests { get;}

        public int Id { get; }

        private List<Guest> Guests {get;} = [];

        public Party(string firstName, string lastName, DateTime date, int numOfGuests, int id)
        {
            FirstName = firstName;
            LastName = lastName;
            PartyDate = date;
            NumOfGuests = numOfGuests;
            Id = id;
        }

        public Party(string firstName, string lastName, DateTime date, int numOfGuests)
        {
            FirstName = firstName;
            LastName = lastName;
            PartyDate = date;
            NumOfGuests = numOfGuests;
        }

        public void ClearGuests()
        {
            Guests.Clear();
        }

        public void AddGuest(Guest newGuest)
        {
            Guests.Add(newGuest);
        }

        //public int GetPartySize()
        //{
        //    return Guests.Count;
        //}

        public void PrintGuests()
        {
            foreach (Guest g in Guests)
            {
                if(!g.IsBookingName) g.PrintInfo();
            }
        }

        //public void AddGuest(Guest newGuest)
        //{
        //    Guests.Add(newGuest);
        //}

        //public void PrintAllGuests()
        //{
        //    "".PrintStringToConsole();
        //    //$"Reservation name: {ReservationName}".PrintStringToConsole();
        //    foreach (Guest guest in Guests)
        //    {
        //        $"{guest.FirstName} {guest.LastName}".PrintStringToConsole();
        //    }
        //}
    }
}
