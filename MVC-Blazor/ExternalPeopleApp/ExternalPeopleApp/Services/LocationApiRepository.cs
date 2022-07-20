using ExternalPeopleApp.Data;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace ExternalPeopleApp.Services
{
    public class LocationApiRepository : ILocationRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LocationApiRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public IEnumerable<Location> GetAll()
        {
            HttpClient client = _httpClientFactory.CreateClient(APIConstants.PeopleHttpClientName);

            HttpResponseMessage response = client.GetAsync("locations").Result;
            if (response.IsSuccessStatusCode)
            {
                IList<Location> locations = response.Content.ReadAsAsync<IList<Location>>().Result;
                return locations;
            }
            else
            {
                return Enumerable.Empty<Location>();
            }
        }
    }
}
