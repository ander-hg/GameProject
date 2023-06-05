using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.DataAccess.Postgres
{
    public class PostgresConnection : IDatabaseConnection
    {
        private readonly string connectionString;

        public PostgresConnection(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public NpgsqlConnection CreateConnection()
        {
            return new NpgsqlConnection(connectionString);
        }
    }
}
