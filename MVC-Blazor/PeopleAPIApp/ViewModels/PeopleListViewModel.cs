using System.Collections.Generic;
using PeopleApp.Data;

namespace PeopleApp.ViewModels
{
    public class PeopleListViewModel
    {
        public IEnumerable<Person> People { get; set; }
        public IEnumerable<string> Cities { get; set; }
        public string SelectedCity { get; set; }

        public string GetClass(string city) =>
            SelectedCity == city ? "bg-info text-white" : "";
    }
}