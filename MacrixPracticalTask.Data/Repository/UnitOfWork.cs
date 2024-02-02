using MacrixPracticalTask.Context;
using MacrixPracticalTask.Repository.IRepository;

namespace MacrixPracticalTask.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        private IPersonRepository _person;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IPersonRepository Person
        {
            get
            {
                if (_person == null)
                {
                    _person = new PersonRepository(_context);
                }
                return _person;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
