using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.DataAccess
{
    public interface IHeroRepository : IRepository<Hero>
    {
        Hero GetById(int id);
        List<Hero> GetAll();
        int Insert(Hero hero);
        void Update(Hero hero);
        void Delete(Hero hero);
        // Additional methods specific to Hero repository if needed
    }
}
