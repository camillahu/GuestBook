using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace GuestBook
{
    internal class DbConWDapper
    {
        //const string _connStr =
        //    @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GuestBookDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;";

        private SqlConnection DbCon()
        {
            const string _connStr =
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GuestBookDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;";
            SqlConnection connection = new(_connStr);
            connection.Open();
            return connection;
        }

        public async Task<IEnumerable<Party>> PrintParties()
        {
            using SqlConnection connection = DbCon();
            {
                string query = "SELECT * FROM dbo.Parties";
                return (await connection.QueryAsync<Party>(query)).ToList();
            }
        }


    }
}
