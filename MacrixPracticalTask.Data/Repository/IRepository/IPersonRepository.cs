using MacrixPracticalTask.Models;
using MacrixPracticalTask.Models.Paging;

namespace MacrixPracticalTask.Repository.IRepository
{
    public interface IPersonRepository : IRepository<Person>
    {
        IEnumerable<Person> GetAllPeople(PersonParameters personParameters);
        Person GetPersonById(int personId);
        void CreatePerson(Person person);
        void UpdatePerson(Person person);
        void DeletePerson(Person person);
    }
}
