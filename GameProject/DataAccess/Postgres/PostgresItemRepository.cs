using System;
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

        public Item GetItemById(int id)
        {
            using (var connection = databaseConnection.CreateConnection())
            {
                string query = "SELECT * FROM Item WHERE Id = @Id";
                return connection.QueryFirstOrDefault<Item>(query, new { Id = id });
            }
        }

        public List<Item> GetAllItems()
        {
            using (var connection = databaseConnection.CreateConnection())
            {
                string query = "SELECT * FROM Item";
                return connection.Query<Item>(query).ToList();
            }
        }

        public void AddItem(Item item)
        {
            using (var connection = databaseConnection.CreateConnection())
            {
                string query = "INSERT INTO Item (Name, Description, Type, Attribute, Value) VALUES (@Name, @Description, @Type, @Attribute, @Value)";
                connection.Execute(query, item);
            }
        }

        public void UpdateItem(Item item)
        {
            using (var connection = databaseConnection.CreateConnection())
            {
                string query = "UPDATE Item SET Name = @Name, Description = @Description, Type = @Type, Attribute = @Attribute, Value = @Value WHERE Id = @Id";
                connection.Execute(query, item);
            }
        }

        public void DeleteItem(int id)
        {
            using (var connection = databaseConnection.CreateConnection())
            {
                string query = "DELETE FROM Item WHERE Id = @Id";
                connection.Execute(query, new { Id = id });
            }
        }

        public Item GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Item entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Item entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Item entity)
        {
            throw new NotImplementedException();
        }
    }
}
