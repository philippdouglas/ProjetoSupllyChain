using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    [Route("home")]
    public IActionResult Index()
    {
        return View();
    }
}