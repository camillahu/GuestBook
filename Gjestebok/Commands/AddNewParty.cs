using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GuestBook.Commands
{
    internal class AddNewParty : ICommand
    {
        public int Id => 1;
        public string Text => "Add new party";

        public void Execute(DbConnection dbConnection)
        {
            //hvordan få sendt nytt party til lista i program? 

            Party newParty = AddParty();
            int partyId = dbConnection.AddPartyToDb(newParty);
            Guest bookingGuest = GetBookingGuest(newParty, partyId);
            dbConnection.AddGuestToDb(bookingGuest);
            while (AddingGuestsChoice())
            {
                dbConnection.AddGuestToDb(GetGuest(partyId));
            }
        }

        public Party AddParty()
        {
            "Type in the name of the person responsible for the party.".PrintStringToConsole();
            string firstName = "First Name:".RequestString();
            string lastName = "Last Name:".RequestString();
            DateTime date = DateTime.Now.Date; //skal egt gjøre sånn at man kan sette inn dato selv, men gjør det bare sånn enn så lenge
            
            Party newParty = new Party(firstName, lastName, date, 1);
            
            return newParty; // id blir 100-et eller annet? 
        }

        public Guest GetBookingGuest(Party newParty, int partyId)
        {
            string firstName = newParty.FirstName;
            string lastName = newParty.LastName;
            Guest bookingGuest = new Guest(firstName, lastName, true, partyId);

            return bookingGuest; 
        }

        Guest GetGuest(int partyId)
        {
            "Type in the info of a guest:".PrintStringToConsole();
            string firstName = "First Name:".RequestString();
            string lastName = "Last Name:".RequestString();
            Guest newGuest = new Guest(firstName, lastName, false, partyId );
            return newGuest;
        }

        public bool AddingGuestsChoice()
        {
            string answer = "Type 1 to add new guest, type anything else to quit making party: ".RequestString();
            
            return answer == "1"; 
        }
    }
}
