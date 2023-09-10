using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UserInterface.Models;
using static System.Net.WebRequestMethods;

namespace UserInterface.Controllers
{
    public class RegisterController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7106/api");
        private readonly HttpClient _client;

        public RegisterController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid) 

                {
                    var registerUser = new
                {
                    model.UserName,
                    model.Email,
                    model.Password,
                    model.Role
                };

                var json = JsonSerializer.Serialize(registerUser);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                string apiUrl = _client.BaseAddress + $"/Authentactation?role={model.Role}";
                var response = await _client.PostAsync(apiUrl, content);
                var responseBody = await response.Content.ReadAsStringAsync();

                var responseObject = JsonSerializer.Deserialize<Response>(responseBody);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }

            return View(model);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}