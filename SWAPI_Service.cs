
using Models;
using Newtonsoft.Json;

public class SWAPI_Service
{
    private readonly HttpClient _client;

    private const string baseURL = "https://swapi.dev/api/";
    private const string peopleURL = baseURL + "people/";
    private const string vehicleURL = baseURL + "vehicle/";

    public SWAPI_Service()
    {
        _client = new HttpClient();
    }

    //Async Method
    public async Task<Person> GetPersonAsync(string url)
    {
        HttpResponseMessage response = await _client.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            //success 200Ok response...
            var content = await response.Content.ReadAsStringAsync();
            var person = JsonConvert.DeserializeObject<Person>(content);
            return person!;
        }
        return null!;
    }

    public async Task<Vehicle> GetVehicleAsync(string url)
    {
        HttpResponseMessage vehresponse = await _client.GetAsync(url);

        //success 200Ok response...
        return vehresponse.IsSuccessStatusCode ?
                await vehresponse.Content.ReadAsAsync<Vehicle>()
                : null!;
    }

    public async Task<T> GetVehicleAsync<T>(string url) where T : class
    {
        var response = await _client.GetAsync(url);
        if(response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<T>(content);
            return obj!;
        }
        return null!;
    }

    public async Task<Person> GetPersonAsync(int index)
    {
        HttpResponseMessage response = await _client.GetAsync(peopleURL + index);
        if(response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var person = JsonConvert.DeserializeObject<Person>(content);
            return person!;
        }
        return null!;
    }

    public async Task<SearchResult<T>> GetSearchResultAsync<T>(string query, string category) where T : class
    {
        HttpResponseMessage response = await _client.GetAsync($"{baseURL}{category}?search={query}");
        if( response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var searchResult = JsonConvert.DeserializeObject<SearchResult<T>>(content);
            return searchResult!;
        }
        return null!;
    }
}
