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

    [HttpPost]
    public IActionResult Create(VehicleModel model)
    {
        if (ModelState.IsValid) {
            _service.Create(model);
            return Redirect("/VehicleModel/Filter/");
        }
        
        return View(model);
    }

    [HttpGet("VehicleModel/UpdateForm/{id}")]
    public IActionResult UpdateForm(int id)
    {
        var result = _service.Get(id);
        ViewData["VehicleModel"] = result;
        return View(result);
    }

    [HttpPost()]
    public IActionResult Update(VehicleModel vehicleModel)
    {
        if (ModelState.IsValid) {
            _service.Update(vehicleModel);
            return Redirect("/VehicleModel/Get/" + vehicleModel.Id);
        }
        return View(vehicleModel);
    }

    [HttpPost("VehicleModel/Delete/{id}")]
    public IActionResult Delete(int id)
    {
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
    public IActionResult Filter(FilterValidation filters)
    {
        var results = _service.Filter(filters);
        ViewData["VehicleModels"] = results;
        return View();
    }
}