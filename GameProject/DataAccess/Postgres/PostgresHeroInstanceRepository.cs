using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
                    return connection.QueryFirstOrDefault<Hero>(query, new { Id = hi });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while getting the hero: {ex.Message}");
                return null;
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
                MessageBox.Show($"An error occurred while getting the hero instance by ID: {ex.Message}");
                return null;
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
                MessageBox.Show($"An error occurred while getting all hero instances: {ex.Message}");
                return new List<HeroInstance>();
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
                MessageBox.Show($"An error occurred while inserting the hero instance and retrieving the ID: {ex.Message}");
                return -1;
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
                MessageBox.Show($"An error occurred while updating the hero instance: {ex.Message}");
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
                MessageBox.Show($"An error occurred while deleting the hero instance: {ex.Message}");
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
                MessageBox.Show($"An error occurred while inserting the hero instance item: {ex.Message}");
                return -1;
            }
        }
    }
}
