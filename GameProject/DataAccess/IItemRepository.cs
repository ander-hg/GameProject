using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.DataAccess
{
    public interface IItemRepository : IRepository<Item>
    {
        Item GetById(int id);
        List<Item> GetAll();
        int Insert(Item item);
        void Update(Item item);
        void Delete(Item item);
    }
}
