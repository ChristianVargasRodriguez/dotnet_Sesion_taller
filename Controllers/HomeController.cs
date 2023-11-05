using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SesionTaller.Models;


namespace SesionTaller.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("")]
    public IActionResult Index(){
        return View();
    }

    [HttpGet]
    [Route("login")]
    public IActionResult Login(){
        return View("Index");
    }


    [HttpGet]
    [Route("dashboard")]
    public IActionResult DashBoard(){
        string? Name = HttpContext.Session.GetString("Name");
        if (Name == null){
            return RedirectToAction("Index");
        }

        int numeroPrincipal = 22;

        if (HttpContext.Session.GetInt32("NumeroPrincipal") != null){
            numeroPrincipal = (int)HttpContext.Session.GetInt32("NumeroPrincipal");
        }

        ViewData["NumeroPrincipal"] = numeroPrincipal;
        return View("Dashboard");
    }




    [HttpPost]
    [Route("dashboard")]
    public IActionResult DashBoard(string operation){
        string? Name = HttpContext.Session.GetString("Name");
        if(Name == null){
            return RedirectToAction("Index");
        }


        int numeroPrincipal = 22;
        if (HttpContext.Session.GetInt32("NumeroPrincipal") != null){
            numeroPrincipal= (int)HttpContext.Session.GetInt32("NumeroPrincipal");
        }

        switch (operation){
            case "increment":
            numeroPrincipal += 1;
            break;

            case "decrement":
            numeroPrincipal -= 1;
            break;

            case "multiply":
            numeroPrincipal *= 2;
            break;
            
            case "random":
            Random random = new Random();
            int cantidadAleatoria = random.Next(1, 11);
            numeroPrincipal += cantidadAleatoria;
            break;
        }

        HttpContext.Session.SetInt32("NumeroPrincipal",numeroPrincipal);

        ViewData["NumeroPrincipal"] = numeroPrincipal;

        return View("Dashboard");
    }






    [HttpPost("/")]
    public IActionResult IniciarSesion(Login login){
        if (ModelState.IsValid){
            HttpContext.Session.SetString("Name", login.Name);
            return RedirectToAction("Dashboard");
        }
        return View("Index");
    } 


    [HttpPost]
    [Route("logout")]
    public IActionResult Logout(){
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
