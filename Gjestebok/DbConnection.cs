using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.Collections;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;

namespace GuestBook
{
    internal class DbConnection
    {
        private SqlConnection DbCon()
        {
            const string _connStr =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GuestBookDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;";
            SqlConnection connection = new(_connStr);
            connection.Open();
            return connection;
        }

        public void GetAllParties(List<Party> parties)
        {
            using SqlConnection connection = DbCon();

            const string querySelectAllGuests = "SELECT LastName, FirstName, PartyDate, NumOfGuests, Id FROM dbo.Parties";

            SqlCommand command = new(querySelectAllGuests, connection);

            try
            {
                SqlDataReader reader = command.ExecuteReader(); //sqldatareader leser dataen så fort første bokstav er lest in real time, så man slipper å vente på at all dataen er lasta inn. 

                while (reader.Read())
                {
                    string firstName = (string)reader["FirstName"];
                    string lastName = (string)reader["LastName"];
                    DateTime partyDate = (DateTime)reader["PartyDate"];
                    int numOfGuests = (int)reader["NumOfGuests"];
                    int id = (int)reader["ID"];

                    parties.Add(new Party(firstName, lastName, partyDate, numOfGuests, id));
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                $"Error: {ex.Message}".PrintStringToConsole();
            }
        }

        public int GetNumOfGuests(int partyId)
        {
            using SqlConnection connection = DbCon();
            int PartyId = Convert.ToInt32(partyId);
            string query = $"SELECT COUNT(*) FROM dbo.Guests WHERE PartyId = {PartyId}";
            
            SqlCommand command = new(query, connection);

            try
            {
                int count = (int)command.ExecuteScalar(); //gjøre det sånn eller med tryparse?
                return count;
            }
            catch (Exception ex)
            {
                $"Error: {ex.Message}".PrintStringToConsole();
                return -1;
            }
        }

        public void UpdateNumOfGuests(int partyId, int count)
        {
            using SqlConnection connection = DbCon();
            string query = $"UPDATE dbo.Parties SET NumOfGuests = {count} WHERE Id = {partyId}";
            SqlCommand command = new(query, connection);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                $"Error + {ex.Message}".PrintStringToConsole();
            }
        }

        public void GetAllPartiesWNames(List<Party> parties)
        {
            GetAllParties(parties);
            using SqlConnection connection = DbCon();
            const string query = "SELECT Lastname, FirstName, IsBookingName, PartyId, Id FROM dbo.Guests";
            SqlCommand command = new(query, connection);

            try
            {
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string firstName = (string)reader["FirstName"];
                    string lastName = (string)reader["LastName"];
                    bool isBookingName = (bool)reader["IsBookingName"];
                    int partyId = (int)reader["PartyId"];
                    int id = (int)reader["Id"];

                    foreach (Party p in parties.Where(p => p.Id == partyId))
                    {
                        p.AddGuest(new Guest(firstName, lastName, isBookingName, partyId, id));
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                $"Error: {ex.Message}".PrintStringToConsole();
            }
        }

        public string FindParty(string search)
        {
            using SqlConnection connection = DbCon();
            string query = $"SELECT p.Id AS PartyId, " +
                           $"STRING_AGG(CONCAT(g.LastName, ', ', g.FirstName), CHAR(10)) AS Guests " +
                           $"FROM dbo.Guests AS g " +
                           $"JOIN dbo.Parties AS p ON g.PartyId = p.Id " +
                           $"WHERE g.LastName = '{search}' OR g.FirstName = '{search}' " +
                           $"GROUP BY p.Id " +
                           $"ORDER BY p.Id";
            SqlCommand command = new(query, connection);

            try
            {
                SqlDataReader reader = command.ExecuteReader();
                StringBuilder result = new StringBuilder();
                string firstName = (string)reader["FirstName"];
                string lastName = (string)reader["LastName"];
                string guests = (string)reader["Guests"];

                while (reader.Read())
                {
                    result.AppendLine($"Booking for: {lastName}, {firstName}");
                    result.AppendLine($"Guests:");
                    result.AppendLine(guests);
                    result.AppendLine();
                }

                return result.ToString();
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        //#region adding
        public int AddPartyToDb(Party newParty)
        {
            using SqlConnection connection = DbCon();
            //string getPartyId = "SELECT TOP 1 Id FROM dbo.parties ORDER BY Id DESC";
            const string query = @"INSERT INTO dbo.Parties (FirstName, LastName, PartyDate, NumOfGuests)
                                 VALUES (@FirstName, @LastName, @PartyDate, @NumOfGuests);
                                 SELECT SCOPE_IDENTITY()"; //SCOPE_IDENTITY() gir ID'en til den siste raden satt inn av denne spørringen

            SqlCommand command = new(query, connection);

            var parameters = new[]
            {
                new SqlParameter("@FirstName", newParty.FirstName),
                new SqlParameter("@LastName", newParty.LastName),
                new SqlParameter("@PartyDate", newParty.PartyDate),
                new SqlParameter("@NumOfGuests", newParty.NumOfGuests)
            };

            command.Parameters.AddRange(parameters);

            try
            {
                object result = command.ExecuteScalar(); //ExecuteScalar() returnerer verdien fra SELECT-delen av spørringen, som er ID-en til den nye raden
                if (result != null) return Convert.ToInt32(result); // denne returnerer noe rart? 
                else throw new Exception("Failed to retrieve the new party ID");
            } 
            catch (Exception ex)
            {
                $"Error: {ex.Message}".PrintStringToConsole();
                return -1; // Returner en verdi som indikerer feil
            }
        }

        public void AddGuestToDb(Guest newGuest)
        {
            using SqlConnection connection = DbCon();
            //string getPartyId = "SELECT TOP 1 Id FROM dbo.parties ORDER BY Id DESC";
            const string query = @"INSERT INTO dbo.Guests (FirstName, LastName, IsBookingName, PartyId)
                                 VALUES (@FirstName, @LastName, @IsBookingName, @PartyId)";
            

            SqlCommand command = new(query, connection);

            var parameters = new[]
            {
                new SqlParameter("@FirstName", newGuest.FirstName),
                new SqlParameter("@LastName", newGuest.LastName),
                new SqlParameter("@IsBookingName", newGuest.IsBookingName),
                new SqlParameter("@PartyId", newGuest.PartyId)
            };

            command.Parameters.AddRange(parameters); 

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                $"Error: {ex.Message}".PrintStringToConsole();
            }
        }
        //#endregion 
    }
}
