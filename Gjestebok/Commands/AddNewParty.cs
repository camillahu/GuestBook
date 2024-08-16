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
            AddBookingGuest(newParty, true, partyId );
            AddGuest(true, partyId);
            dbConnection.AddGuestToDb();

        }

        public Party AddParty()
        {
            "Type in the name of the person responsible for the party.".PrintStringToConsole();
            string firstName = "First Name:".RequestString();
            string lastName = "Last Name:".RequestString();
            DateTime date = DateTime.Now; //skal egt gjøre sånn at man kan sette inn dato selv, men gjør det bare sånn enn så lenge
            int numOfGuests = "NumberOfGuests:".RequestInt();

            Party newParty = new Party(firstName, lastName, date, numOfGuests);
            return newParty;
        }

        public void AddBookingGuest(Party newParty, bool isBookingName, int partyId)
        {
            //start her 
        }

        Guest AddGuest(isBookingName = false, partyId)
        {
            "Type in the info of a guest:".PrintStringToConsole();
            string firstName = "First Name:".RequestString();
            string lastName = "Last Name:".RequestString();
            bool IsBookingName = isBookingName;
            Guest newGuest = new Guest(name);
            return newGuest;
        }

        public bool AddingGuestsChoice()
        {
            string answer = "Type 1 to add new guest, type anything else to quit making party: ".RequestString();

            return answer == "1";
        }
    }
}
