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
        void Delete(T entity);
        T Add(T entity);
        void Update(T entity);
    }

}
