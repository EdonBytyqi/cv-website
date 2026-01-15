using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DotNetCV.Models;
using DotNetCV.Services;
using DotNetCV.ViewModels;

namespace DotNetCV.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICvService _cvService;

    public HomeController(ILogger<HomeController> logger, ICvService cvService)
    {
        _logger = logger;
        _cvService = cvService;
    }

    public async Task<IActionResult> Index()
    {
        var cvData = await _cvService.GetCompleteCvAsync();
        return View(cvData);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
