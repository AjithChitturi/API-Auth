using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using UserInterface.Models;

namespace UserInterface.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _client;

        public LoginController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:7106/api");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var loginUser = new
                {
                    model.Username,
                    model.Password
                };

                var json = JsonSerializer.Serialize(loginUser);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                string apiUrl = _client.BaseAddress + "/Authentactation/Login";
                var response = await _client.PostAsync(apiUrl, content);
                var responseBody = await response.Content.ReadAsStringAsync();

                var responseObject = JsonSerializer.Deserialize<Response>(responseBody);

                if (response.IsSuccessStatusCode)
                {
                    
                        return RedirectToAction("OTP", "OTP");
                                        
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username or password.");
                }
            }

            return View(model);
        }
    }
}
