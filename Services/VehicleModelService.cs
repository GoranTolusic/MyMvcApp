
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using Microsoft.EntityFrameworkCore;
using MyMvcApp;
using System.Collections.Generic;

public class VehicleModelService
{
    private readonly PostgreDbContext _context;

    public VehicleModelService(PostgreDbContext context)
    {
        _context = context;
    }

    public VehicleModel Create(string name, int vehicleId)
    {
        //This is hardcoded for now, take params from args and make it dynamical
        //Also try to use async await
        var vehicleModel = new VehicleModel
        {
            Name = name,
            VehicleId = vehicleId
        };
        this._context.Add(vehicleModel);
        var created = this._context.SaveChanges();
        return vehicleModel;
    }

    public VehicleModel Get(int id)
    {
        var getModel = this._context.VehicleModel
            .Include(a => a.Vehicle)
            .ToList()
            .SingleOrDefault(a => a.Id == id);
        return getModel;
    }

    public void Delete(int id)
    {
        var model = this._context.VehicleModel.Find(id);
        if (model != null)
        {
            // Remove the vehicle from the database context
            this._context.VehicleModel.Remove(model);
            this._context.SaveChanges();
        }
    }

    public VehicleModel Update(string name, int id)
    {
        var model = this._context.VehicleModel.Find(id);
        if (model != null)
        {
            model.Name = name;
            this._context.SaveChanges();
        }
        return model;
    }

    public List<VehicleModel> Filter(int pageNumber, int pageSize, string sortBy, string searchTerm, int vehicleId)
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

        var query = this._context.VehicleModel.AsQueryable();
        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(e => e.Name.Contains(searchTerm));
        }

        if (vehicleId > 0)
        {
            query = query.Where(e => e.VehicleId == vehicleId);
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