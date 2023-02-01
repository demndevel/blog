using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Web.Configs;

namespace Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HomeConfig _config;

    public HomeController(ILogger<HomeController> logger, IOptions<HomeConfig> config)
    {
        _logger = logger;
        _config = config.Value;
    }

    [Route("/")]
    public IActionResult Index()
    {
        ViewBag.text = _config.Text;
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }
}