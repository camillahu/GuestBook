using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook.Commands
{
    internal class Exit : ICommand
    {
        public int Id => 5;
        public string Text => "Exit";
        public void Execute(DbConnection connection, List<Party> parties)
        {
            "".PrintStringToConsole();
            "Have a nice day!".PrintStringToConsole();
            Environment.Exit(5);
        }
    }
}
