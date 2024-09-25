using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        List<T> items { get; set; }
        public List<T> GetAll()
        {
            return items;
        }
        public void Delete(T entity)
        {
            items.Remove(entity);
        }
        public void Add(T entity)
        {
           items.Add(entity);
        }

        public void Update(T entity) { 
        
        
        }
    }
}
