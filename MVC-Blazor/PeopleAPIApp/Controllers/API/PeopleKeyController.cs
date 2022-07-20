using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeopleApp.Attributes;
using PeopleApp.Data;
using PeopleApp.Models;
using PeopleApp.Services;

namespace PeopleApp.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]

    public class PeopleKeyController : ControllerBase
    {
        private readonly IPeopleRepository _peopleRepository;

        public PeopleKeyController(IPeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Person> people = _peopleRepository.GetAll();
            List<PersonOutputModel> models = people.Select(x => PersonOutputModel.FromPersonToJSON(x)).ToList();

            return Ok(models);

        }

        [HttpGet("{id}")]
        public IActionResult GetDetails(long id)
        {
            Person person = _peopleRepository.GetById(id);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(PersonOutputModel.FromPersonToJSON(person));
        }

        [HttpPost]
        public IActionResult AddPerson(PersonEditModel personEditModel)
        {
            Person person = new Person
            {
                Firstname = personEditModel.Firstname,
                Surname = personEditModel.Surname,
                DepartmentId = personEditModel.DepartmentId,
                LocationId = personEditModel.LocationId
            };
            _peopleRepository.Add(person);
            person = _peopleRepository.GetById(person.Id);
            var outputModel = PersonOutputModel.FromPersonToJSON(person);

            return CreatedAtAction("GetDetails", new { id = person.Id }, outputModel);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePerson(int id, PersonEditModel personEditModel)
        {
            Person person = _peopleRepository.GetById(id);
            if (person == null)
            {
                return NotFound();
            }

            person.Firstname = personEditModel.Firstname;
            person.Surname = personEditModel.Surname;
            person.DepartmentId = personEditModel.DepartmentId;
            person.LocationId = personEditModel.LocationId;
            _peopleRepository.Update(person);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePerson(int id)
        {
            Person person = _peopleRepository.GetById(id);
            if (person == null)
            {
                return NotFound();
            }

            _peopleRepository.Delete(person);
            return Ok();
        }
    }
}
