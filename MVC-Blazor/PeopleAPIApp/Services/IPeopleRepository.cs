using PeopleApp.Data;
using System.Collections;
using System.Collections.Generic;

namespace PeopleApp.Services
{
    public interface IPeopleRepository
    {
        // get
        IEnumerable<Person> GetAll();
        // get
        Person GetById(long id);
        // post request
        void Add(Person person);
        // put 
        void Update(Person person);
        // get
        void Delete(Person person);
    }
}
 