﻿using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameProject.DataAccess.Postgres
{
    public class PostgresHeroRepository : IHeroRepository
    {
        private readonly IDatabaseConnection databaseConnection;

        public PostgresHeroRepository(IDatabaseConnection databaseConnection)
        {
            this.databaseConnection = databaseConnection;
        }

        public Hero GetById(int id)
        {
            try
            {
                using (var connection = databaseConnection.CreateConnection())
                {
                    string query = "SELECT * FROM Hero WHERE id = @Id";
                    return connection.QueryFirstOrDefault<Hero>(query, new { Id = id });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Hero> GetAll()
        {
            try
            {
                using (var connection = databaseConnection.CreateConnection())
                {
                    string query = "SELECT * FROM Hero";
                    return connection.Query<Hero>(query).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Insert(Hero hero)
        {
            try
            {
                using (var connection = databaseConnection.CreateConnection())
                {
                    string query = "INSERT INTO Hero (Name, Health, Mana, Attack, Defense) VALUES (@Name, @Health, @Mana, @Attack, @Defense)  RETURNING id;";
                    return connection.QuerySingle<int>(query, hero);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Hero hero)
        {
            try
            {
                using (var connection = databaseConnection.CreateConnection())
                {
                    string query = "UPDATE Hero SET Name = @Name, Health = @Health, Mana = @Mana, Attack = @Attack, Defense = @Defense WHERE id = @Id";
                    connection.Execute(query, hero);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(Hero hero)
        {
            try
            {
                using (var connection = databaseConnection.CreateConnection())
                {
                    string query = "DELETE FROM Hero WHERE id = @Id";
                    connection.Execute(query, new { Id = hero.Id });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
