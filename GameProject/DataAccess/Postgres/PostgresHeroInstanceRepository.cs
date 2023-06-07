using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            using (var connection = databaseConnection.CreateConnection())
            {
                string query = "SELECT * FROM Hero WHERE id = (SELECT hero_id FROM HeroInstance WHERE id = @Id)";
                return connection.QueryFirstOrDefault<Hero>(query, new { Id = hi });
            }
        }

        public HeroInstance GetById(int id){
            using (var connection = databaseConnection.CreateConnection())
            {
                string query = "SELECT * FROM HeroInstance WHERE id = @Id";
                return connection.QueryFirstOrDefault<HeroInstance>(query, new { Id = id });
            }
        }

        public List<HeroInstance> GetAll()
        {
            using (var connection = databaseConnection.CreateConnection())
            {
                string query = "SELECT * FROM HeroInstance";
                return connection.Query<HeroInstance>(query).ToList();
            }
        }

        public void Insert(HeroInstance heroInstance)
        {
            using (var connection = databaseConnection.CreateConnection())
            {
                string query = "INSERT INTO HeroInstance (current_level, current_experience, gold, hero_id, targetable, attackable) VALUES (@Current_Level, @Current_Experience, @Gold, @HeroId, @Targetable, @Attackable) RETURNING id;";
                connection.Query(query, new { Current_Level = heroInstance.CurrentLevel, Current_Experience = heroInstance.CurrentExperience, Gold = heroInstance.Gold, HeroId = heroInstance.Hero.Id, Targetable = true, Attackable = true });

            }
        }

        public void Insert(HeroInstance heroInstance, ref int ad)
        {
            using (var connection = databaseConnection.CreateConnection())
            {
                string query = "INSERT INTO HeroInstance (current_level, current_experience, gold, hero_id, targetable, attackable) VALUES (@Current_Level, @Current_Experience, @Gold, @HeroId, @Targetable, @Attackable) RETURNING id;";
                ad = connection.QuerySingle<int>(query, new { Current_Level = heroInstance.CurrentLevel, Current_Experience = heroInstance.CurrentExperience, Gold = heroInstance.Gold, HeroId = heroInstance.Hero.Id, Targetable = true, Attackable = true });

            }
        }

        public void Update(HeroInstance heroInstance)
        {
            using (var connection = databaseConnection.CreateConnection())
            {
                string query = "UPDATE HeroInstance SET current_level = @CurrentLevel, current_experience = @CurrentExperience, gold = @Gold WHERE id = @Id";
                connection.Execute(query, heroInstance);
            }
        }

        public void Delete(HeroInstance heroInstance)
        {
            using (var connection = databaseConnection.CreateConnection())
            {
                string query = "DELETE FROM HeroInstance WHERE id = @Id";
                connection.Execute(query, new { Id = heroInstance.Id });
            }
        }

        public void InsertItem(HeroInstance heroInstance, Item item, ref int ad)
        {
            using (var connection = databaseConnection.CreateConnection())
            {
                string query = "INSERT INTO HeroInstanceItem (hero_instance_id, item_id) VALUES (@HeroInstanceId, @ItemId) RETURNING id;";
                ad = connection.QuerySingle<int>(query, new { HeroInstanceId = heroInstance.Id, ItemId = item.Id});
            }
        }
    }
}
