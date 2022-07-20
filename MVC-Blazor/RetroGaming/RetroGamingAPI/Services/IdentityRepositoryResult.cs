using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetroGamingAPI.Services
{
    public class IdentityRepositoryResult
    {
        List<string> errors = new List<string>();
        public IEnumerable<string> Errors => errors;

        public IdentityUser IdentityUser { get; set; }
        public SignInResult SignInResult { get; set; }
        public IdentityResult IdentityUserCreationResult { get; set; }
        public IdentityResult IdentityExternalLoginResult { get; set; }

        public bool Succeeded { get; set; }

        public void AddError(string error)
        {
            errors.Add(error);
        }
    }
}
