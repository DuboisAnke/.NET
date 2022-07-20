using PeopleApp.Data;
using System.Collections;
using System.Collections.Generic;

namespace PeopleApp.Services
{
    public interface ILocationRepository
    {
        IEnumerable<Location> GetAll();
    }
}
