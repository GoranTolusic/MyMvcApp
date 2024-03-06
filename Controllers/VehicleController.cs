using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MyMvcApp.Models;
using System;
using System.Web;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MyMvcApp.Controllers;

public class VehicleController : Controller
{
    private readonly VehicleService _service;

    public VehicleController(VehicleService vehicleService)
    {
        _service = vehicleService;
    }

    [HttpGet("Vehicle/CreateForm")]
    public IActionResult CreateForm()
    {
        VarDumper.Dump("u create form requestu");
        return View();
    }

    [HttpPost("Vehicle/Create")]
    public IActionResult Create()
    {
        VarDumper.Dump("u create post requestu");
        string name = HttpContext.Request.Form["name"];
        int year = int.Parse(HttpContext.Request.Form["Year"]);
        var result = _service.Create(name, year);
        return Redirect("/Vehicle/Filter");
    }

    [HttpGet("Vehicle/UpdateForm/{id}")]
    public IActionResult UpdateForm(int id)
    {
        VarDumper.Dump("u update form requestu");
        var result = _service.Get(id);
        ViewData["Vehicle"] = result;
        return View();
    }

    [HttpPost("Vehicle/Update/{id}")]
    public IActionResult Update(int id)
    {
        VarDumper.Dump("u update requestu");
        string name = HttpContext.Request.Form["name"];
        int year = int.Parse(HttpContext.Request.Form["Year"]);
        var result = _service.Update(name, year, id);
        return Redirect("/Vehicle/Get/" + id);
    }

    [HttpPost("Vehicle/Delete/{id}")]
    public IActionResult Delete(int id)
    {
        VarDumper.Dump("u delete requestu");
        _service.Delete(id);
        return Redirect("/Vehicle/Filter");
    }

    [HttpGet("Vehicle/Get/{id}")]
    public IActionResult Get(int id)
    {
        var result = _service.Get(id);
        if (result == null)
        {
            return NotFound();
        }
        ViewData["Vehicle"] = result;
        return View();
    }

    [HttpGet("Vehicle/Filter")]
    public IActionResult Filter()
    {
        string searchTerm = HttpContext.Request.Query["searchTerm"];
        string sortBy = HttpContext.Request.Query["sortBy"];

        // Default values for pageNumber and pageSize
        int pageNumber = 0;
        int pageSize = 10;

        // Check if the query parameters exist before parsing
        if (!string.IsNullOrEmpty(HttpContext.Request.Query["pageNumber"]))
        {
            int.TryParse(HttpContext.Request.Query["pageNumber"], out pageNumber);
        }

        if (!string.IsNullOrEmpty(HttpContext.Request.Query["pageSize"]))
        {
            int.TryParse(HttpContext.Request.Query["pageSize"], out pageSize);
        }

        VarDumper.Dump(pageSize);
        VarDumper.Dump(pageNumber);

        var results = _service.Filter(pageNumber, pageSize, sortBy, searchTerm);

        ViewData["Vehicles"] = results;
        return View();
    }
}