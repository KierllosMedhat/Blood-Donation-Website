using BLL.Interfaces;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ApplicationDbContext _context { get; set; }

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public T GetById(int? id)
            => _context.Set<T>().Find(id);

        public T GetByName(string name)
            => _context.Set<T>().Find(name);
        
        public IEnumerable<T> GetAll()
            => _context.Set<T>().ToList();

        public void Add(T entity)
            => _context.Set<T>().Add(entity);


        public void Delete(T entity)
            => _context.Set<T>().Remove(entity);

        public void Update(T entity)
            => _context.Set<T>().Update(entity);

    }
}
