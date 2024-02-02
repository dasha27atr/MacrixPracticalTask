using MacrixPracticalTask.Context;
using MacrixPracticalTask.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MacrixPracticalTask.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected ApplicationDbContext _context { get; set; }
        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> FindAll() => _context.Set<T>().AsNoTracking();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition) =>
            _context.Set<T>().Where(condition).AsNoTracking();

        public void Create(T entity) => _context.Set<T>().Add(entity);
        public void Update(T entity) => _context.Set<T>().Update(entity);
        public void Delete(T entity) => _context.Set<T>().Remove(entity);
    }
}
