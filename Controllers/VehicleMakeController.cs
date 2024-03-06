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

public class VehicleMakeController : Controller
{
    private readonly VehicleMakeService _service;

    public VehicleMakeController(VehicleMakeService vehicleMakeService)
    {
        _service = vehicleMakeService;
    }

    [HttpGet("VehicleMake/CreateForm")]
    public IActionResult CreateForm(VehicleMake model)
    {
        return View(model);
    }

    [HttpPost]
    public IActionResult Create(VehicleMake model)
    {
        if (ModelState.IsValid) {
            _service.Create(model);
            return Redirect("/VehicleMake/Filter");
        }
        return View(model);
    }

    [HttpGet("VehicleMake/UpdateForm/{id}")]
    public IActionResult UpdateForm(int id)
    {
        var result = _service.Get(id);
        ViewData["VehicleMake"] = result;
        return View(result);
    }

    [HttpPost()]
    public IActionResult Update(VehicleMake vehicleMake)
    {
        if (ModelState.IsValid) {
            _service.Update(vehicleMake);
            return Redirect("/VehicleMake/Get/" + vehicleMake.Id);
        }
        return View(vehicleMake);
    }

    [HttpPost("VehicleMake/Delete/{id}")]
    public IActionResult Delete(int id)
    {
        VarDumper.Dump("u delete requestu");
        _service.Delete(id);
        return Redirect("/VehicleMake/Filter");
    }

    [HttpGet("VehicleMake/Get/{id}")]
    public IActionResult Get(int id, VehicleModel vehicleModel)
    {
        var result = _service.Get(id);
        if (result == null)
        {
            return NotFound();
        }
        ViewData["VehicleMake"] = result;
        vehicleModel.VehicleMakeId = result.Id;
        return View(vehicleModel);
    }

    [HttpGet("VehicleMake/Filter")]
    public IActionResult Filter(FilterValidation filters)
    {
        VarDumper.Dump("u filteru");
        var results = _service.Filter(filters);
        ViewData["VehicleMakes"] = results;
        return View(filters);
    }
}