using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.DataAccess
{
    public interface IHeroInstanceRepository : IRepository<HeroInstance>
    {
        Hero GetHero(HeroInstance hi);
        HeroInstance GetById(int id);
        List<HeroInstance> GetAll();
        int Insert(HeroInstance hi);
        void Update(HeroInstance hi);
        void Delete(HeroInstance hi);
        int InsertItem(HeroInstance hi, Item i);
        List<Item> GetAllHeroItems(HeroInstance hi);
    }
}
