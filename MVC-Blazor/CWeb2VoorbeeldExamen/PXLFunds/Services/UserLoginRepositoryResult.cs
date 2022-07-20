using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace PXLFunds.Services
{
    public class UserLoginRepositoryResult
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
