using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GuestBook.Commands
{
    internal class SearchForParty : ICommand
    {
        public int Id => 2;
        public string Text => "Search for party";
        public void Execute(DbConnection connection, List<Party> parties)
        {
            string search = "Type your name search here:".RequestString();

            string searchResult = connection.FindParty(search);
            searchResult.PrintStringToConsole();
        }
    }
}
