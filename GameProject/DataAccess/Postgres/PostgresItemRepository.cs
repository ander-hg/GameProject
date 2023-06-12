using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            try
            {
                using (var connection = databaseConnection.CreateConnection())
                {
                    string query = "SELECT * FROM Item WHERE id = @Id";
                    return connection.QueryFirstOrDefault<Item>(query, new { Id = id });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Item> GetAll()
        {
            try
            {
                using (var connection = databaseConnection.CreateConnection())
                {
                    string query = "SELECT * FROM item";
                    return connection.Query<Item>(query).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Insert(Item item)
        {
            try
            {
                using (var connection = databaseConnection.CreateConnection())
                {
                    string query = "INSERT INTO Item (Name, Description, Type, Attribute, Value, Cost) VALUES (@Name, @Description, @Type, @Attribute, @Value, @Cost)  RETURNING id;";
                    return connection.QuerySingle<int>(query, item);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Item item)
        {
            try
            {
                using (var connection = databaseConnection.CreateConnection())
                {
                    string query = "UPDATE Item SET Name = @Name, Description = @Description, Type = @Type, Attribute = @Attribute, Value = @Value, Cost = @Cost WHERE id = @Id";
                    connection.Execute(query, item);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(Item i)
        {
            try
            {
                using (var connection = databaseConnection.CreateConnection())
                {
                    string query = "DELETE FROM Item WHERE id = @Id";
                    connection.Execute(query, new { Id = i.Id });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
