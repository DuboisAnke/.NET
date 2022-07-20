using System.ComponentModel.DataAnnotations;

namespace PeopleApp.Data {

    public class Person {

        public long Id { get; set; }

        [Required(ErrorMessage = "A firstname is required")]
        [MinLength(3, ErrorMessage = "Firstnames must be 3 or more characters")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "A surname is required")]
        [MinLength(3, ErrorMessage = "Surnames must be 3 or more characters")]
        public string Surname { get; set; }

        public Department Department { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "A department must be set")]
        public long DepartmentId { get; set; }

        public Location Location { get; set; }
        public long LocationId { get; set; }
    }
}
