﻿using System.Diagnostics;
using BlogApp.Filters;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Models;

namespace BlogApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [ViewLayout("_NoHeaderAndFooterLayout")]
    public IActionResult Login()
    {
        return View();
    }
    
    [ViewLayout("_NoHeaderAndFooterLayout")]
    public IActionResult Register()
    {
        return View();
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