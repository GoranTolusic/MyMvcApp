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

public class VehicleModelController : Controller
{
    private readonly VehicleModelService _service;

    public VehicleModelController(VehicleModelService vehicleModelService)
    {
        _service = vehicleModelService;
    }

    [HttpPost("VehicleModel/Create")]
    public IActionResult Create()
    {
        VarDumper.Dump("u create post requestu");
        string name = HttpContext.Request.Form["name"];
        int vehicleId = int.Parse(HttpContext.Request.Form["vehicleId"]);
        var result = _service.Create(name, vehicleId);
        return Redirect("/Vehicle/Get/" + vehicleId);
    }

    [HttpGet("VehicleModel/UpdateForm/{id}")]
    public IActionResult UpdateForm(int id)
    {
        VarDumper.Dump("u update form requestu");
        var result = _service.Get(id);
        ViewData["VehicleModel"] = result;
        return View();
    }

    [HttpPost("VehicleModel/Update/{id}")]
    public IActionResult Update(int id)
    {
        VarDumper.Dump("u update requestu");
        string name = HttpContext.Request.Form["name"];
        var result = _service.Update(name, id);
        return Redirect("/VehicleModel/Get/" + id);
    }

    [HttpPost("VehicleModel/Delete/{id}")]
    public IActionResult Delete(int id)
    {
        VarDumper.Dump("u delete requestu");
        _service.Delete(id);
        return Redirect("/VehicleModel/Filter");
    }

    [HttpGet("VehicleModel/Get/{id}")]
    public IActionResult Get(int id)
    {
        var result = _service.Get(id);
        if (result == null)
        {
            return NotFound();
        }
        ViewData["VehicleModel"] = result;
        return View();
    }

    [HttpGet("VehicleModel/Filter")]
    public IActionResult Filter()
    {
        string searchTerm = HttpContext.Request.Query["searchTerm"];
        string sortBy = HttpContext.Request.Query["sortBy"];

        // Default values for pageNumber and pageSize
        int pageNumber = 0;
        int pageSize = 10;
        int vehicleId = 0;

        // Check if the query parameters exist before parsing
        if (!string.IsNullOrEmpty(HttpContext.Request.Query["pageNumber"]))
        {
            int.TryParse(HttpContext.Request.Query["pageNumber"], out pageNumber);
        }

        if (!string.IsNullOrEmpty(HttpContext.Request.Query["pageSize"]))
        {
            int.TryParse(HttpContext.Request.Query["pageSize"], out pageSize);
        }

        if (!string.IsNullOrEmpty(HttpContext.Request.Query["vehicleId"]))
        {
            int.TryParse(HttpContext.Request.Query["vehicleId"], out pageSize);
        }

        VarDumper.Dump(pageSize);
        VarDumper.Dump(pageNumber);

        var results = _service.Filter(pageNumber, pageSize, sortBy, searchTerm, vehicleId);

        ViewData["VehicleModels"] = results;
        return View();
    }
}