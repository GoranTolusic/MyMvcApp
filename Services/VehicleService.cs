
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using Microsoft.EntityFrameworkCore;
using MyMvcApp;
using System.Collections.Generic;

public class VehicleService
{
    private readonly PostgreDbContext _context;

    public VehicleService(PostgreDbContext context)
    {
        _context = context;
    }

    public Vehicle Create(string name, int year)
    {
        //This is hardcoded for now, take params from args and make it dynamical
        //Also try to use async await
        var vehicle = new Vehicle
        {
            Name = name,
            Year = year
        };
        this._context.Add(vehicle);
        var created = this._context.SaveChanges();
        return vehicle;
    }

    public Vehicle Get(int id)
    {
        var getOneVehicleWithModels = this._context.Vehicle
            .Include(a => a.VehicleModels)
            .ToList()
            .SingleOrDefault(a => a.Id == id);
        return getOneVehicleWithModels;
    }

    public void Delete(int id)
    {
        var vehicle = this._context.Vehicle.Find(id);
        if (vehicle != null)
        {
            // Remove the vehicle from the database context
            this._context.Vehicle.Remove(vehicle);
            this._context.SaveChanges();
        }
    }

    public Vehicle Update(string name, int year, int id)
    {
        var vehicle = this._context.Vehicle.Find(id);
        if (vehicle != null)
        {
            vehicle.Name = name;
            vehicle.Year = year;
            this._context.SaveChanges();
        }
        return vehicle;
    }

    public List<Vehicle> Filter(int pageNumber, int pageSize, string sortBy, string searchTerm)
    {
        int skipCalucation;

        if (pageNumber == 0)
        {
            skipCalucation = 0;
        }
        else
        {
            skipCalucation = (pageNumber - 1) * pageSize;
        }

        var query = this._context.Vehicle.AsQueryable();
        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(e => e.Name.Contains(searchTerm));
        }

        if (!string.IsNullOrEmpty(sortBy))
        {
            switch (sortBy)
            {
                case "name":
                    query = query.OrderBy(e => e.Name);
                    break;
                case "id":
                    query = query.OrderBy(e => e.Id);
                    break;
            }
        }

        query = query.Skip(skipCalucation)
                     .Take(pageSize);


        var result = query.ToList();
        return result;
    }
}