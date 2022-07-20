using System.Collections.Generic;
using ExternalPeopleApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExternalPeopleApp.ViewModels
{
    public class PersonEditViewModel : PersonEditModel
    {
         public List<SelectListItem> DepartmentSelectListItems { get; set; }
         public List<SelectListItem> LocationSelectListItems { get; set; }
    }
}
