using IMDBClone.Data;
using IMDBClone.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization(); // ✅ This line is required

builder.Services.AddControllersWithViews(); // ✅ enables Razor view rendering

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();



var app = builder.Build();

// ✅ Seed Roles & Admin User
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    context.Database.Migrate();

    // Seed Roles
    if (!context.Roles.Any())
    {
        roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
        roleManager.CreateAsync(new IdentityRole("User")).Wait();
    }

    // Seed Admin User
    if (!context.Users.Any(u => u.UserName == "admin"))
    {
        var adminUser = new User
        {
            Id = "6F9619FF-8B86-D011-B42D-00C04FC964FF",
            UserName = "admin",
            Email = "admin@imdbclone.com",
            NormalizedUserName = "ADMIN",
            NormalizedEmail = "ADMIN@IMDBCLONE.COM",
            EmailConfirmed = true
        };

        var passwordHasher = new PasswordHasher<User>();
        adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Admin123!");

        userManager.CreateAsync(adminUser).Wait();
        userManager.AddToRoleAsync(adminUser, "Admin").Wait();
    }
}

app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
