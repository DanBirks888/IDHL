using System.Diagnostics;
using DeveloperAssessment.Web.DomainModels;
using DeveloperAssessment.Web.Extensions;
using DeveloperAssessment.Web.Interfaces;
using Microsoft.AspNetCore.Mvc;
using DeveloperAssessment.Web.Models;
using DeveloperAssessment.Web.ViewModels.Blog;
using Microsoft.AspNetCore.Components;

namespace DeveloperAssessment.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IBlogService _blogService;

    public HomeController(ILogger<HomeController> logger,
        IBlogService blogService)
    {
        _logger = logger;
        _blogService = blogService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Blog([FromRoute] int id)
    {
        var blogPost = _blogService.Get(id);
        return View(blogPost.ToViewModel());
    }

    public IActionResult BlogListing()
    {
        var blogPost = _blogService.GetAll();
        return View(blogPost.ToViewModel());
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}