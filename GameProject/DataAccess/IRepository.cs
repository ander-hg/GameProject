﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.DataAccess
{
    public interface IRepository<T>
    {
        T GetById(int id);
        int Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        List<T> GetAll();
    }
}
