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
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;

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
        

        public void GetAllGuests()
        {
            using SqlConnection connection = DbCon();

            const string querySelectAllGuests = "SELECT * FROM dbo.Guests";

            SqlCommand command = new(querySelectAllGuests, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(); //sqldatareader leser dataen så fort første bokstav er lest in real time, så man slipper å vente på at all dataen er lasta inn. 

                while (reader.Read())
                {
                    $"{reader["Id"]}, {reader["FirstName"]}, {reader["LastName"]}".PrintStringToConsole();
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                $"Error + {ex.Message}".PrintStringToConsole();
            }
        }

        public int AddPartyToDb(Party newParty)
        {
            using SqlConnection connection = DbCon();
            //string getPartyId = "SELECT TOP 1 Id FROM dbo.parties ORDER BY Id DESC";
            const string query = @"INSERT INTO dbo.parties (FirstName, LastName, PartyDate, NumOfGuests)
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
                connection.Open();
                object result = command.ExecuteScalar(); //ExecuteScalar() returnerer verdien fra SELECT-delen av spørringen, som er ID-en til den nye raden
                if (result != null) return Convert.ToInt32(result);
                else throw new Exception("Failed to retrieve the new party ID");
            } 
            catch (Exception ex)
            {
                $"Error: {ex.Message}".PrintStringToConsole();
                return -1; // Returner en verdi som indikerer feil
            }
            finally
            {
                connection.Close();
            }
        }

        public void AddGuestToDb(Guest newGuest)
        {
            using SqlConnection connection = DbCon();
            //string getPartyId = "SELECT TOP 1 Id FROM dbo.parties ORDER BY Id DESC";
            const string query = @"INSERT INTO dbo.parties (FirstName, LastName, IsBookingName, PartyId)
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
                connection.Open();
            }
            catch (Exception ex)
            {
                $"Error: {ex.Message}".PrintStringToConsole();
            }
            finally
            {
                connection.Close();
            }
        }

    }
}
