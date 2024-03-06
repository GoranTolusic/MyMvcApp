using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class FilterValidation
{
    public int pageNumber { get; set; } = 1;

    public int pageSize { get; set; } = 10;

    public string sortBy { get; set; } = "id";

    public string? searchTerm { get; set; }

    public int? VehicleMakeId { get; set; }

}

