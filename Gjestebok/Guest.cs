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
        public int IsBookingName { get; set; } 
        public int PartyId { get; set; } 
        //#region test
        

        //#endregion 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstName">edwfwefewf</param>
        /// <param name="lastName"></param>
        /// <param name="isBookingName">dette er en bool egt</param>
        /// <param name="partyId"></param>
        public Guest(string firstName, string lastName, int isBookingName, int partyId)
        {
            FirstName = firstName;
            LastName = lastName;
            IsBookingName = isBookingName;
            PartyId = partyId;
        }
    }
}
