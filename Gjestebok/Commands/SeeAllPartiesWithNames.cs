using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook.Commands
{
    internal class SeeAllPartiesWithNames : ICommand
    {
        public int Id => 4;
        public string Text => "See all parties with names";
        public void Execute(DbConnection connection, List<Party> parties)
        {
            connection.GetAllPartiesWNames();
            "".PrintStringToConsole();
        }
    }
}
