using System.Diagnostics;
using DotNetCV.Models;
using DotNetCV.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCV.Controllers;

public sealed class HomeController(ICvService cvService) : Controller
{
    public IActionResult Index() => View(cvService.GetCv());

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() =>
        View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
