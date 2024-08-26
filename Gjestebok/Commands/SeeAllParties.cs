using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook.Commands
{
    internal class SeeAllParties : ICommand
    {
        public int Id => 3;
        public string Text => "See all parties";
        public void Execute(DbConnection connection, List<Party> parties)
        {
            connection.GetAllParties(parties);

            foreach (Party p in parties)
            {
                "".PrintStringToConsole();
                $"Booking for: {p.LastName}, {p.FirstName}".PrintStringToConsole();
                $"Date: {p.PartyDate}".PrintStringToConsole();
                $"Size: {p.NumOfGuests}".PrintStringToConsole();
                "".PrintStringToConsole();
            }
        }
    }
}
