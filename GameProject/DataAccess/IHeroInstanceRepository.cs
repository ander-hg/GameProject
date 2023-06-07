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
        void Insert(HeroInstance hi);
        void Insert(HeroInstance hi, ref int ad);
        void Update(HeroInstance hi);
        void Delete(HeroInstance hi);
        void InsertItem(HeroInstance hi, Item i, ref int id);
    }
}
