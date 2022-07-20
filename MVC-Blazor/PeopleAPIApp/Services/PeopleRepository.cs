using PeopleApp.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PeopleApp.Services
{
    public class PeopleRepository : IPeopleRepository
    {
        private DataContext _context;

        public PeopleRepository(DataContext context)
        {
            _context = context;
        }
        public void Add(Person person)
        {
            _context.People.Add(person);
            _context.SaveChanges();
        }

        public void Delete(Person person)
        {
            _context.People.Remove(person);
            _context.SaveChanges();
        }

        public IEnumerable<Person> GetAll()
        {
            return _context.People.Include(p => p.Department)
                .Include(p => p.Location).ToList();
        }

        public Person GetById(long id)
        {
            return _context.People.Include(p => p.Department)
                .Include(p => p.Location).FirstOrDefault(p => p.Id == id);
        }

        public void Update(Person person)
        {
            _context.People.Update(person);
            _context.SaveChanges();
        }
    }
}
