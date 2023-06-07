using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace GameProject.DataAccess.Postgres
{
    public class PostgresItemRepository : IItemRepository
    {
        private readonly IDatabaseConnection databaseConnection;

        public PostgresItemRepository(IDatabaseConnection databaseConnection)
        {
            this.databaseConnection = databaseConnection;
        }

        public Item GetById(int id)
        {
            using (var connection = databaseConnection.CreateConnection())
            {
                string query = "SELECT * FROM Item WHERE id = @Id";
                return connection.QueryFirstOrDefault<Item>(query, new { Id = id });
            }
        }

        public List<Item> GetAll()
        {
            using (var connection = databaseConnection.CreateConnection())
            {
                string query = "SELECT * FROM item";
                return connection.Query<Item>(query).ToList();
            }
        }

        public void Insert(Item item)
        {
            using (var connection = databaseConnection.CreateConnection())
            {
                string query = "INSERT INTO Item (Name, Description, Type, Attribute, Value, Cost) VALUES (@Name, @Description, @Type, @Attribute, @Value, @Cost)";
                connection.Execute(query, item);
            }
        }

        public void Update(Item item)
        {
            using (var connection = databaseConnection.CreateConnection())
            {
                string query = "UPDATE Item SET Name = @Name, Description = @Description, Type = @Type, Attribute = @Attribute, Value = @Value, Cost = @Cost WHERE id = @Id";
                connection.Execute(query, item);
            }
        }

        public void Delete(Item i)
        {
            using (var connection = databaseConnection.CreateConnection())
            {
                string query = "DELETE FROM Item WHERE id = @Id";
                connection.Execute(query, new { Id = i.Id });
            }
        }
    }
}
