using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.DataAccess.Postgres
{
    public class PostgresHeroRepository : IHeroRepository
    {
        private readonly IDatabaseConnection databaseConnection;

        public PostgresHeroRepository(IDatabaseConnection databaseConnection)
        {
            this.databaseConnection = databaseConnection;
        }

        public Hero GetHeroById(int id)
        {
            using (var connection = databaseConnection.CreateConnection())
            {
                string query = "SELECT * FROM Hero WHERE Id = @Id";
                return connection.QueryFirstOrDefault<Hero>(query, new { Id = id });
            }
        }

        public List<Hero> GetAllHeroes()
        {
            using (var connection = databaseConnection.CreateConnection())
            {
                string query = "SELECT * FROM Hero";
                return connection.Query<Hero>(query).ToList();
            }
        }

        public void AddHero(Hero hero)
        {
            using (var connection = databaseConnection.CreateConnection())
            {
                string query = "INSERT INTO Hero (Name, Health, Mana, Attack, Defense) VALUES (@Name, @Health, @Mana, @Attack, @Defense)";
                connection.Execute(query, hero);
            }
        }

        public void UpdateHero(Hero hero)
        {
            using (var connection = databaseConnection.CreateConnection())
            {
                string query = "UPDATE Hero SET Name = @Name, Health = @Health, Mana = @Mana, Attack = @Attack, Defense = @Defense WHERE Id = @Id";
                connection.Execute(query, hero);
            }
        }

        public void DeleteHero(int id)
        {
            using (var connection = databaseConnection.CreateConnection())
            {
                string query = "DELETE FROM Hero WHERE Id = @Id";
                connection.Execute(query, new { Id = id });
            }
        }

        public Hero GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Hero entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Hero entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Hero entity)
        {
            throw new NotImplementedException();
        }
    }
}
