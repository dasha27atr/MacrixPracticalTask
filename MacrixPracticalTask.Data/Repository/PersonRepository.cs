using MacrixPracticalTask.Context;
using MacrixPracticalTask.Models;
using MacrixPracticalTask.Models.Paging;
using MacrixPracticalTask.Repository.IRepository;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;

namespace MacrixPracticalTask.Repository
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<Person> GetAllPeople(PersonParameters personParameters)
        {
            var people = FindAll()
                .Skip((personParameters.PageNumber - 1) * personParameters.PageSize)
                .Take(personParameters.PageSize);

            ApplySort(ref people, personParameters.OrderBy);

            return people;
        }

        public Person GetPersonById(int personId)
        {
            return FindByCondition(person => person.Id == personId).FirstOrDefault();
        }

        public void CreatePerson(Person person)
        {
            Create(person);
        }

        public void UpdatePerson(Person person)
        {
            Update(person);
        }

        public void DeletePerson(Person person)
        {
            Delete(person);
        }

        private void ApplySort(ref IQueryable<Person> people, string order)
        {
            if (!people.Any())
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(order))
            {
                people = people.OrderBy(x => x.LastName);
                return;
            }

            var orderParams = order.Trim().Split(',');
            var propertyInfos = typeof(Person).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var orderStringBuilder = new StringBuilder();

            foreach (var orderParam in orderParams)
            {
                if (string.IsNullOrWhiteSpace(orderParam))
                {
                    continue;
                }

                var propertyFromQueryName = orderParam.Split(' ')[0];
                var objectProperty = propertyInfos.FirstOrDefault(x => x.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                {
                    continue;
                }

                var sortingOrder = orderParam.EndsWith(" desc") ? "descending" : "ascending";

                orderStringBuilder.Append($"{objectProperty.Name.ToString()} {sortingOrder}, ");
            }

            var orderQuery = orderStringBuilder.ToString().TrimEnd(',', ' ');

            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                people = people.OrderBy(x => x.LastName);
                return;
            }

            people = people.OrderBy(orderQuery);
        }
    }
}
