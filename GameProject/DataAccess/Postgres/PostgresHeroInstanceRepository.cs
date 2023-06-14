using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace GameProject.DataAccess.Postgres
{
    public class PostgresHeroInstanceRepository : IHeroInstanceRepository
    {
        private readonly IDatabaseConnection databaseConnection;

        public PostgresHeroInstanceRepository(IDatabaseConnection databaseConnection)
        {
            this.databaseConnection = databaseConnection;
        }

        public Hero GetHero(HeroInstance hi)
        {
            try
            {
                using (NpgsqlConnection connection = databaseConnection.CreateConnection())
                {
                    string query = "SELECT * FROM Hero WHERE id = (SELECT hero_id FROM HeroInstance WHERE id = @Id)";
                    return connection.QueryFirstOrDefault<Hero>(query, new { Id = hi.Id });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HeroInstance GetById(int id)
        {
            try
            {
                using (NpgsqlConnection connection = databaseConnection.CreateConnection())
                {
                    string query = "SELECT * FROM HeroInstance WHERE id = @Id";
                    return connection.QueryFirstOrDefault<HeroInstance>(query, new { Id = id });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HeroInstance> GetAll()
        {
            try
            {
                using (NpgsqlConnection connection = databaseConnection.CreateConnection())
                {
                    string query = "SELECT * FROM HeroInstance";
                    return connection.Query<HeroInstance>(query).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Insert(HeroInstance heroInstance)
        {
            try
            {
                using (NpgsqlConnection connection = databaseConnection.CreateConnection())
                {
                    connection.Open();

                    string query = "INSERT INTO HeroInstance (current_level, current_experience, gold, hero_id, targetable, attackable) "
                        + "VALUES (@Current_Level, @Current_Experience, @Gold, @HeroId, @Targetable, @Attackable) RETURNING id;";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Current_Level", heroInstance.CurrentLevel);
                        command.Parameters.AddWithValue("@Current_Experience", heroInstance.CurrentExperience);
                        command.Parameters.AddWithValue("@Gold", heroInstance.Gold);
                        command.Parameters.AddWithValue("@HeroId", heroInstance.Hero.Id);
                        command.Parameters.AddWithValue("@Targetable", true);
                        command.Parameters.AddWithValue("@Attackable", true);

                        return (int)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(HeroInstance heroInstance)
        {
            try
            {
                using (NpgsqlConnection connection = databaseConnection.CreateConnection())
                {
                    string query = "UPDATE HeroInstance SET current_level = @CurrentLevel, current_experience = @CurrentExperience, gold = @Gold WHERE id = @Id";
                    connection.Execute(query, heroInstance);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(HeroInstance heroInstance)
        {
            try
            {
                using (NpgsqlConnection connection = databaseConnection.CreateConnection())
                {
                    string query = "DELETE FROM HeroInstance WHERE id = @Id";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", heroInstance.Id);

                        command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertItem(HeroInstance heroInstance, Item item)
        {
            try
            {
                using (NpgsqlConnection connection = databaseConnection.CreateConnection())
                {
                    connection.Open();
                    string query = "INSERT INTO HeroInstanceItem (hero_instance_id, item_id) VALUES (@HeroInstanceId, @ItemId) RETURNING id;";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@HeroInstanceId", heroInstance.Id);
                        command.Parameters.AddWithValue("@ItemId", item.Id);

                        return (int)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Item> GetAllHeroItems(HeroInstance heroInstance)
        {
            try
            {
                using (NpgsqlConnection connection = databaseConnection.CreateConnection())
                {
                    connection.Open();
                    string query = "SELECT * FROM item i, heroinstance hi, heroinstanceitem hii WHERE i.id = hii.item_id AND hii.hero_instance_id = hi.id AND hi.id = @HeroInstanceId;";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@HeroInstanceId", heroInstance.Id);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            List<Item> items = new List<Item>();

                            while (reader.Read())
                            {
                                Item item = new Item((int)reader["id"], (string)reader["Name"], (string)reader["Description"], (string)reader["Type"], (string)reader["Attribute"], (int)reader["Value"], (int)reader["Cost"]);
                                items.Add(item);
                            }

                            return items;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

