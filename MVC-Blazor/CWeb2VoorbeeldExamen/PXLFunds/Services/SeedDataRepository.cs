using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using PXLFunds.Data;
using PXLFunds.Models;
using PXLFunds.Settings;

namespace PXLFunds.Services
{
    public class SeedDataRepository : ISeedDataRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;
        private RoleManager<IdentityRole> _roleManager;
        public SeedDataRepository(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task Initialise()
        {
            //Data models
            AddBankInfo();
            AddFundInfo();
            //Identity models
            await AddRoles();
            await AddAdminUser();
        }
        private void AddBankInfo()
        {
            if (!_context.Bank.Any())
            {
                _context.Bank.AddRange(new Bank
                { BankName = "KBC" }, new Bank
                {
                    BankName = "Argenta"
                });
                _context.SaveChanges();
            }
        }
        private async void AddFundInfo()
        {
            if (!_context.Fund.Any())
            {
                _context.Fund.AddRange(new Fund
                {
                    FundName = "KBC Green",
                    Bank = _context.Bank.FirstOrDefault(x => x.BankName.Equals("KBC")),
                    BankId = _context.Bank.FirstOrDefault(x => x.BankName.Equals("KBC")).BankId,
                    FundValue = 100
                },
                    new Fund
                    {
                        FundName = "KBC Yellow",
                        Bank = _context.Bank.FirstOrDefault(x => x.BankName.Equals("KBC")),
                        BankId = _context.Bank.FirstOrDefault(x => x.BankName.Equals("KBC")).BankId,
                        FundValue = 125
                    },
                    new Fund
                    {
                        FundName = "ARG Brown",
                        Bank = _context.Bank.FirstOrDefault(x => x.BankName.Equals("Argenta")),
                        BankId = _context.Bank.FirstOrDefault(x => x.BankName.Equals("Argenta")).BankId,
                        FundValue = 135
                    },
                    new Fund
                    {
                        FundName = "ARG Black",
                        Bank = _context.Bank.FirstOrDefault(x => x.BankName.Equals("Argenta")),
                        BankId = _context.Bank.FirstOrDefault(x => x.BankName.Equals("Argenta")).BankId,
                        FundValue = 140
                    });
                _context.SaveChanges();

            }
        }
        private async Task AddRoles()
        {
            if (!_context.Roles.Any())
            {
                var adminrole = ProgramInfo.Roles.AdminRole;
                var clientrole = ProgramInfo.Roles.ClientRole;
                await _roleManager.CreateAsync(new IdentityRole(adminrole));
                await _roleManager.CreateAsync(new IdentityRole(clientrole));
            }
        }
        private async Task AddAdminUser()
        {
            if (!_context.Users.Any())
            {
                var identityUser = new IdentityUser
                {
                    UserName = "admin",
                    Email = "admin@pxl.be"
                };
                await _userManager.CreateAsync(identityUser, "Adm!n123");
            }
        }
    }
}