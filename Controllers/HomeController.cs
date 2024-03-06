using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace MyMvcApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly VehicleModelService _service;

    public HomeController(ILogger<HomeController> logger, VehicleModelService vehicleService)
    {
        _logger = logger;
        _service = vehicleService;
    }

    public IActionResult Index()
    {
        //Create Vehicle (vehicle variable actually contains updated info about inserte object)
        // this._context.Add(vehicle);
        // var created = this._context.SaveChanges();

        // //Get One for vehicle
        // var getOneVehicleWithModels = this._context.Vehicle.Include(a => a.VehicleModels).ToList().SingleOrDefault(a => a.Id == 1);

        // //Get all vehicles with Models
        // var getAllVehiclesWithModels = this._context.Vehicle.Include(a => a.VehicleModels).ToList();

        // VarDumper.Dump(vehicle);
        // VarDumper.Dump(getOneVehicleWithModels);
        // VarDumper.Dump(getAllVehiclesWithModels);
        _service.Get(1);
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
