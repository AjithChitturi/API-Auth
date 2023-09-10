using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UserInterface.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _client;

        public UserController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:7106/api/");
        }

        [HttpGet]
        public async Task<IActionResult> User()
        {
            HttpResponseMessage response = await _client.GetAsync("UserController/employees");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                // You can deserialize the data if it's in JSON format
                // For this example, assuming the data is a list of strings
                var employees = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(data);

                return View(employees);
            }
            else
            {
                // Handle error cases
                return View("Error");
            }
        }
    }
}
