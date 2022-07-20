using System.Collections.Generic;

namespace PeopleApp.Data {
    public class Department {

        public long Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<Person> People { get; set; }
    }
}
