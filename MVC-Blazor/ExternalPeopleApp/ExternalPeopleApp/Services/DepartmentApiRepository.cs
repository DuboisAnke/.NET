using ExternalPeopleApp.Data;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace ExternalPeopleApp.Services
{
    public class DepartmentApiRepository : IDepartmentRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DepartmentApiRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IEnumerable<Department> GetAll()
        {
            HttpClient client = _httpClientFactory.CreateClient(APIConstants.PeopleHttpClientName);

            HttpResponseMessage response = client.GetAsync("departments").Result;
            if (response.IsSuccessStatusCode)
            {
                IList<Department> departments = response.Content.ReadAsAsync<IList<Department>>().Result;
                return departments;
            }
            else
            {
                return Enumerable.Empty<Department>();
            }
        }
    }
}
