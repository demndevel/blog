using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Web.Configs;
using Web.Models;

namespace Web.Controllers;

[Route("admin")]
public class AdminController : Controller
{
    private readonly string _adminPassword;

    public AdminController(IOptions<AdminPasswordConfig> config)
    {
        _adminPassword = config.Value.Password;
    }

    [Route(""), Authorize]
    public IActionResult Admin()
    {
        return View();
    }
    
    [HttpGet("login")]
    public IActionResult LoginPage()
    {
        return View();
    }
    
    [HttpPost("login")]
    public IActionResult Login([FromForm] LoginModel model)
    {
        if (model.Password != _adminPassword)
            return BadRequest("Incorrect password");

        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "admin")
        }, CookieAuthenticationDefaults.AuthenticationScheme);

        var principal = new ClaimsPrincipal(identity);

        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        return Ok("Logged in.");
    }
}