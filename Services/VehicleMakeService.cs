
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using Microsoft.EntityFrameworkCore;
using MyMvcApp;
using System.Collections.Generic;

public class VehicleMakeService
{
    private readonly PostgreDbContext _context;

    public VehicleMakeService(PostgreDbContext context)
    {
        _context = context;
    }

    public VehicleMake Create(VehicleMake vehicleMake)
    {
        this._context.Add(vehicleMake);
        this._context.SaveChanges();
        return vehicleMake;
    }

    public VehicleMake Get(int id)
    {
        var getOneVehicleMakeWithModels = this._context.VehicleMake
            .Include(a => a.VehicleModels)
            .ToList()
            .SingleOrDefault(a => a.Id == id);
        return getOneVehicleMakeWithModels;
    }

    public void Delete(int id)
    {
        var vehicleMake = this._context.VehicleMake.Find(id);
        if (vehicleMake != null)
        {
            // Remove the vehicle from the database context
            this._context.VehicleMake.Remove(vehicleMake);
            this._context.SaveChanges();
        }
    }

    public VehicleMake Update(VehicleMake vehicleMakeFromRequest)
    {
        var vehicleMake = this._context.VehicleMake.Find(vehicleMakeFromRequest.Id);
        if (vehicleMake != null)
        {
            vehicleMake.Name = vehicleMakeFromRequest.Name;
            vehicleMake.Year = vehicleMakeFromRequest.Year;
            this._context.SaveChanges();
        }
        return vehicleMake;
    }

    public List<VehicleMake> Filter(FilterValidation filters)
    {
        int skipCalucation;

        if (filters.pageNumber == 0)
        {
            skipCalucation = 0;
        }
        else
        {
            skipCalucation = (filters.pageNumber - 1) * filters.pageSize;
        }

        var query = this._context.VehicleMake.AsQueryable();
        if (!string.IsNullOrEmpty(filters.searchTerm))
        {
            query = query.Where(e => e.Name.Contains(filters.searchTerm));
        }

        if (!string.IsNullOrEmpty(filters.sortBy))
        {
            switch (filters.sortBy)
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
                     .Take(filters.pageSize);


        var result = query.ToList();
        return result;
    }
}