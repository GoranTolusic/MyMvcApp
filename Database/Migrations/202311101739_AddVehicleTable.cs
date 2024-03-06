using FluentMigrator;

[Migration(202311101739)]
public class AddVehicleTable : Migration
{
    public override void Up()
    {
        Create.Table("VehicleMakes")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Name").AsString(255).NotNullable()
            .WithColumn("Year").AsInt32();
    }

    public override void Down()
    {
        Delete.Table("VehicleMakes");
    }
}