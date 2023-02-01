using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("admin")]
public class AdminController : Controller
{
    [Route("")]
    public IActionResult Admin()
    {
        return View();
    }
}