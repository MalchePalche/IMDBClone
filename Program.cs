using IMDBClone.Data;
using IMDBClone.Models;
using IMDBClone.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity with roles
builder.Services.AddDefaultIdentity<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

// Fake email sender
builder.Services.AddTransient<IEmailSender, FakeEmailSender>();

// Cookie paths
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

builder.Services.AddAuthorization();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

builder.Services.AddControllersWithViews()
    .AddApplicationPart(typeof(IMDBClone.NonAPI.MoviesPageController).Assembly);
builder.Services.AddRazorPages();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Seed Roles and Admin user
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    context.Database.Migrate();

    // Create roles
    string[] roles = { "Admin", "User" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // FORCE RE-CREATE ADMIN USER
    var adminEmail = "admin@imdbclone.com";
    var existingUser = await userManager.FindByEmailAsync(adminEmail);
    if (existingUser != null)
    {
        await userManager.DeleteAsync(existingUser); // 💥 Force delete if exists
    }

    var adminUser = new User
    {
        UserName = adminEmail,
        Email = adminEmail,
        EmailConfirmed = true
    };

    var result = await userManager.CreateAsync(adminUser, "Admin123!");
    if (result.Succeeded)
    {
        await userManager.AddToRoleAsync(adminUser, "Admin");
        await userManager.AddToRoleAsync(adminUser, "User");
    }
}


app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
