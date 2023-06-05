using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.DataAccess
{
    public interface IHeroRepository : IRepository<Hero>
    {
        Hero GetHeroById(int id);
        List<Hero> GetAllHeroes();
        void AddHero(Hero hero);
        void UpdateHero(Hero hero);
        void DeleteHero(int id);
        // Additional methods specific to Hero repository if needed
    }
}
