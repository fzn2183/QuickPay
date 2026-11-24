using System.Net.Http.Json;
using var client = new HttpClient();


//Apne api k actual Url lagana hoga yahan
string apiurl = "https://localhost:44352/api/Transfer/send";

var request1 = new
{
  FromUserId = 1,
  ToUserId = 1003,
  amount = 4000
};

var request2 = new
{
    FromUserId = 1,
    ToUserId = 1005,
    amount = 4000
};


//Dono httprequest start kro 
Task<HttpResponseMessage> task1 = client.PostAsJsonAsync(apiurl, request1);

Task<HttpResponseMessage> task2 = client.PostAsJsonAsync(apiurl,request2);

//Dono complete hone tak wait karo
HttpResponseMessage[] responses = await Task.WhenAll(task1, task2);

foreach (var response in responses)
{
    Console.WriteLine($"Status: {(int)response.StatusCode}");

    string message = await response.Content.ReadAsStringAsync();

    Console.WriteLine($"Response: {message}");
}
