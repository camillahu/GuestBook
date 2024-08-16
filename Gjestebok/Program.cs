
using GuestBook;
using GuestBook.Commands;

DbConnection connection = new DbConnection();
List<Party> parties = new List<Party>();

List<ICommand?> commands =
[
    new AddNewParty(),
    new SearchForParty(),
    new SeeAllParties(),
    new SeeAllPartiesWithNames(),
    new Exit(),
];

MainMenu();
void MainMenu()
{
    bool mainMenuRunning = true;
    $"Welcome to the booking system".PrintStringToConsole();

    while (mainMenuRunning)
    {
        ViewCommands();
        ICommand? choice = ChooseOption();
        choice?.Execute(connection); //vits i å gjøre alt nullable?
        mainMenuRunning = true;

    }
}

ICommand? ChooseOption()
{
    ICommand? command = null;
    
    int input = "What do you want to do? ".RequestInt();
    command = commands.FirstOrDefault(c => c.Id == input);
    
    return command;
    }

void ViewCommands()
{
    foreach (ICommand? cmd in commands)
    {
        $"{cmd.Id}. {cmd.Text} ".PrintStringToConsole();
    }
}

void AddParty(Party newParty)
{
    parties.Add(newParty);
}

void FindParty(string name)
{
    // var match = parties.Where(party => party.ReservationName.Split(' ').Any(part => part.StartsWith(name, StringComparison.OrdinalIgnoreCase)));
    //foreach (Party m in match)
    //{
    //    m.PrintNameAndGuestNum();
    //}
}

void PrintBookList()
{
    foreach (Party p in parties)
    {
        p.PrintNameAndGuestNum();
    }
}

void PrintDetailedBookList()
{
    foreach (Party p in parties)
    {
        p.PrintAllGuests();
    }
}

