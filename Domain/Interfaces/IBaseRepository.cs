﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        List<T> Get();
        T? Get<TId>(TId id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }

}
