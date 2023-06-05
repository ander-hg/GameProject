using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.DataAccess.Postgres
{
    public class PostgresHeroInstanceRepository : PostgresRepository<HeroInstance>, IHeroInstanceRepository
    {
        public PostgresHeroInstanceRepository(IDatabaseConnection databaseConnection) : base(databaseConnection)
        {
        }

        // Additional methods specific to HeroInstance repository if needed
    }
}
