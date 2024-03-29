
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

    public VehicleModel Create(VehicleModel vehicleModel)
    {
        this._context.Add(vehicleModel);
        this._context.SaveChanges();
        return vehicleModel;
    }

    public VehicleModel Get(int id)
    {
        var getModel = this._context.VehicleModel
            .Include(a => a.VehicleMake)
            .ToList()
            .SingleOrDefault(a => a.Id == id);
        return getModel;
    }

    public void Delete(int id)
    {
        var model = this._context.VehicleModel.Find(id);
        if (model != null)
        {
            // Remove the vehicle model from the database context
            this._context.VehicleModel.Remove(model);
            this._context.SaveChanges();
        }
    }

    public VehicleModel Update(VehicleModel vehicleModel)
    {
        var model = this._context.VehicleModel.Find(vehicleModel.Id);
        if (model != null)
        {
            model.Name = vehicleModel.Name;
            this._context.SaveChanges();
        }
        return model;
    }

    public List<VehicleModel> Filter(FilterValidation filters)
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

        var query = this._context.VehicleModel.AsQueryable();
        if (!string.IsNullOrEmpty(filters.searchTerm))
        {
            query = query.Where(e => e.Name.Contains(filters.searchTerm));
        }

        if (filters.VehicleMakeId != null && filters.VehicleMakeId > 0)
        {
            query = query.Where(e => e.VehicleMakeId == filters.VehicleMakeId);
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