using System.Runtime.InteropServices.ComTypes;
using PeopleApp.Data;

namespace PeopleApp.Models
{
    public class PersonOutputModel
    {
        public long Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public long DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public long LocationId { get; set; }
        public string LocationName { get; set; }

        public static PersonOutputModel FromPersonToJSON(Person person)
        {
            return new PersonOutputModel
            {
                Id = person.Id,
                Firstname = person.Firstname,
                Surname = person.Surname,
                DepartmentId = person.DepartmentId,
                DepartmentName = person.Department.Name,
                LocationId = person.LocationId,
                LocationName = $"{person.Location.City}, {person.Location.State}"
            };
        }
    }
}
