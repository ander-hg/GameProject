using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.DataAccess
{
    public interface IDatabaseConnection
    {
        NpgsqlConnection CreateConnection();
    }
}
