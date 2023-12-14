using Models;
using Newtonsoft.Json;
using static System.Console;

Clear();

// HttpClient _client = new HttpClient();

// HttpResponseMessage response = await _client.GetAsync("https://swapi.dev/api/people/1");
// WriteLine(response.StatusCode);

// if(response.IsSuccessStatusCode)
// {
    //we need to turn this into a C# object
    // var content = await response.Content.ReadAsStringAsync();
    // WriteLine(content);

    //Person person = JsonConvert.DeserializeObject<Person>(content)!;
    // WriteLine(person.Name);

    //faster way:
//     var luke = await response.Content.ReadAsAsync<Person>();
//     WriteLine(luke.Name);

    //look at the vehicles that luke has
//     foreach (string vehicleUrl in luke.Vehicles!)
//     {
//         HttpResponseMessage vehResponse = await _client.GetAsync(vehicleUrl);

//         Vehicle vehicle = await vehResponse.Content.ReadAsAsync<Vehicle>();
//         WriteLine(vehicle.Name);
//     }
// }

ForegroundColor = ConsoleColor.DarkBlue;
WriteLine("== Using SWAPI Service ==");
ResetColor();

SWAPI_Service service = new SWAPI_Service();

System.Console.Write("Enter a person id: ");
int userInput = int.Parse(Console.ReadLine()!);

Person luke = await service.GetPersonAsync(userInput);
WriteLine(luke.Name);

Person person = await service.GetPersonAsync("https://swapi.dev/api/people/1");
if(person is not null)
{
    WriteLine(person.Name);

    foreach(string vehUrl in person.Vehicles!)
    {
        var vehicle = await service.GetVehicleAsync(vehUrl);
        WriteLine(vehicle.Name);
        WriteLine(vehicle.CargoCapacity);
    }
}

ForegroundColor = ConsoleColor.DarkBlue;
WriteLine("== Using SWAPI Service Generics (vehicles) ==");
ResetColor();

var veh = await service.GetVehicleAsync<Vehicle>("https://swapi.dev/api/vehicles/14");
if(veh is not null)
    WriteLine(veh.Name);
else
    WriteLine("Not found.");

ForegroundColor = ConsoleColor.DarkBlue;
WriteLine("== Using SWAPI Service Generics (People) ==");
ResetColor();


var solo = await service.GetVehicleAsync<Person>("https://swapi.dev/api/people/14");
if(solo is not null)
    WriteLine(solo.Name);
else
    WriteLine("Not found.");

ForegroundColor = ConsoleColor.DarkBlue;
WriteLine("== Using SWAPI Service Search Result Person (people) ==");
ResetColor();

SearchResult<Person> skywalkers = await service.GetSearchResultAsync<Person>("darth","people");
if(skywalkers != null)
{
    foreach(Person skywalker in skywalkers.Results)
    {
        WriteLine(skywalker.Name);
    }
}

SearchResult<Vehicle> vehicles = await service.GetSearchResultAsync<Vehicle>("Crawler","vehicles");
if(vehicles != null)
{
    foreach(Vehicle skywalker in vehicles.Results)
    {
        WriteLine(skywalker.Name);
    }
}