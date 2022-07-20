using System;
using ExternalPeopleApp.Data;
using ExternalPeopleApp.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace ExternalPeopleApp.Services
{
    public class PeopleApiRepository : IPeopleRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PeopleApiRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public void Add(PersonEditViewModel personEditViewModel)
        {
            HttpClient client = _httpClientFactory.CreateClient(APIConstants.PeopleHttpClientName);
            HttpResponseMessage response = client.PostAsJsonAsync("people", personEditViewModel).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not save person");
            }
        }

        public void Delete(Person person)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Person> GetAll()
        {
            HttpClient client = _httpClientFactory.CreateClient(APIConstants.PeopleHttpClientName);

            HttpResponseMessage response = client.GetAsync("people").Result;
            if (response.IsSuccessStatusCode)
            {
                IList<Person> people = response.Content.ReadAsAsync<IList<Person>>().Result;
                return people;
            }
            else
            {
                return Enumerable.Empty<Person>();
            }
        }

        public Person GetById(long id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(PersonEditViewModel personEditViewModel)
        {
            throw new System.NotImplementedException();
        }
    }
}
