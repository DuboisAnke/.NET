using System.Collections.Generic;
using ExternalPeopleApp.Data;
using ExternalPeopleApp.ViewModels;

namespace ExternalPeopleApp.Services
{
    public interface IPeopleRepository
    {
        IEnumerable<Person> GetAll();
        Person GetById(long id);
        void Add(PersonEditViewModel personEditViewModel);
        void Update(PersonEditViewModel personEditViewModel);
        void Delete(Person person);
    }
}
