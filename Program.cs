using Microsoft.EntityFrameworkCore;
using MyMvcApp;

//Create database and migrate tables
DatabaseService.SetupDatabase();

var builder = WebApplication.CreateBuilder(args);

//Dodavanje VehicleService classe da bude injectable
builder.Services.AddScoped<VehicleService>();
builder.Services.AddScoped<VehicleModelService>();

//Inicijalizacija db konekcija na postgres
builder.Services.AddDbContext<PostgreDbContext>(options =>
    options.UseNpgsql(Config.GetEnv("ConnectionStrings:DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
