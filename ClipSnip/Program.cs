using ClipSnip.Data;
using ClipSnip.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
// Recommendation service for hairstyle suggestions
builder.Services.AddScoped<ClipSnip.Services.IRecommendationService, ClipSnip.Services.RecommendationService>();
// Face-shape analyzer and repository used by recommendation service
builder.Services.AddScoped<ClipSnip.Services.FaceShapeServices>();
builder.Services.AddScoped<ClipSnip.Data.HairstyleRepository>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();

    var services = scope.ServiceProvider;

    var db = services.GetRequiredService<ApplicationDbContext>();
    var userManager =
        services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager =
        services.GetRequiredService<RoleManager<IdentityRole>>();

    await db.Database.MigrateAsync();

    await ApplicationDbInitializer.Initialize(
        db,
        userManager,
        roleManager);
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
