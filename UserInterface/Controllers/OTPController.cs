using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using UserInterface.Models;
using System.IdentityModel.Tokens.Jwt;

namespace UserInterface.Controllers
{
    public class OTPController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7106/api");
        private readonly HttpClient _client;

        public OTPController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult OTP()
        {
            return View(new OTPModel());
        }

        [HttpPost]
        public async Task<IActionResult> OTP(OTPModel model)
        {
            ModelState.Clear();

            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

                string apiUrl = _client.BaseAddress + "/Authentactation/login-2FA";

                var response = await _client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(responseContent);

                    var jwtHandler = new JwtSecurityTokenHandler();
                    var jwtToken = jwtHandler.ReadJwtToken(tokenResponse.token);

                    if (jwtToken.Claims.Any(claim => claim.Type == "role" && claim.Value == "Admin"))
                    {
                        return RedirectToAction("Admin", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("User", "User");
                    }
                }
                else
                {
                    
                    ModelState.AddModelError("", "Invalid code.");
                }
            }

            return View("OTPView", model);
        }
    }

    public class TokenResponse
    {
        public string token { get; set; }
        public DateTime expiration { get; set; }
    }
}
