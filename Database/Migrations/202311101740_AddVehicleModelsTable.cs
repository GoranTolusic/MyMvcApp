using FluentMigrator;
using System.Data;

[Migration(202311101740)]
public class AddVehicleModelTable : Migration
{
    public override void Up()
    {
        Create.Table("VehicleModels")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("VehicleId").AsInt32()
            .WithColumn("Name").AsString(255);

        Create.ForeignKey("FK_VehicleModel_Vehicle")
            .FromTable("VehicleModels").ForeignColumn("VehicleId")
            .ToTable("Vehicles").PrimaryColumn("Id")
            .OnDelete(Rule.Cascade);
    }

    public override void Down()
    {
        Delete.Table("VehicleModels");
    }
}