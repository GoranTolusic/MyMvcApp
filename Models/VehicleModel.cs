using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyMvcApp
{
    [Table("VehicleModels")]
    public class VehicleModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int? Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

         [Column("VehicleMakeId")]
        public int? VehicleMakeId { get; set; }

        public VehicleMake? VehicleMake { get; set; }

    }
}
