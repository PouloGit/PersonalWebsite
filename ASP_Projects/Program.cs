using ASP_Projects;
using ASP_Projects.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
    {
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (IServiceScope scope = app.Services.CreateScope())
{
    RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    UserManager<IdentityUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    IdentityUser? defaultAdmingUser = await userManager.FindByEmailAsync("admin@email.com"); // password: Admin*852

    if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
        await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

    if (defaultAdmingUser is not null && !await userManager.IsInRoleAsync(defaultAdmingUser, UserRoles.Admin))
        await userManager.AddToRoleAsync(defaultAdmingUser, UserRoles.Admin);
}

app.Run();
