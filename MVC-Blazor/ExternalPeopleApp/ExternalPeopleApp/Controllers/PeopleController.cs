using System.Linq;
using ExternalPeopleApp.Data;
using ExternalPeopleApp.Services;
using ExternalPeopleApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExternalPeopleApp.Controllers
{
    public class PeopleController : Controller
    {
        private readonly IPeopleRepository _peopleRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ILocationRepository _locationRepository;

        public PeopleController(IPeopleRepository peopleRepository, IDepartmentRepository departmentRepository, ILocationRepository locationRepository)
        {
            _peopleRepository = peopleRepository;
            _departmentRepository = departmentRepository;
            _locationRepository = locationRepository;
        }
        public IActionResult Index()
        {
            return View(_peopleRepository.GetAll());
        }

        public IActionResult Create()
        {
            var model = new PersonEditViewModel
            {
                DepartmentSelectListItems = _departmentRepository.GetAll().Select(d => new SelectListItem
                {
                    Text = d.DepartmentName,
                    Value = d.DepartmentId.ToString()
                }).ToList(),
                LocationSelectListItems = _locationRepository.GetAll().Select(l => new SelectListItem
                {
                    Text = l.LocationName,
                    Value = l.LocationId.ToString()
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(PersonEditViewModel personEditViewModel)
        {
            _peopleRepository.Add(personEditViewModel);
            return RedirectToAction(nameof(Index));
        }
    }
}
