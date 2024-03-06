using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyMvcApp
{
    [Table("VehicleMakes")]
    public class VehicleMake
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int? Id { get; set; }

        [Column("Name")]
        [Required(ErrorMessage = "Field is required.")]  
        public string Name { get; set; }

        [Column("Year")]
        public int? Year { get; set; }

        public List<VehicleModel>? VehicleModels { get; set; }

    }
}
