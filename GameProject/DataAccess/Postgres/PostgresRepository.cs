using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.DataAccess.Postgres
{
    public class PostgresRepository<T> : IRepository<T>
    {
        private IDatabaseConnection _databaseConnection;

        public PostgresRepository(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public T GetById(int id)
        {
            // Code to retrieve an entity by its ID from the PostgreSQL database
            throw new NotImplementedException();
        }

        public void Insert(T entity)
        {
            // Code to insert an entity into the PostgreSQL database
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            // Code to update an entity in the PostgreSQL database
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            // Code to delete an entity from the PostgreSQL database
            throw new NotImplementedException();
        }
    }
}
