using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IPersonRepository
    {
        void AddPerson(Person person);
        Person GetPerson(int id);
        IEnumerable<Person> GetAllPersons();
        void UpdatePerson(Person person);
        void DeletePerson(int id);
    }
}
