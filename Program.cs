using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Notes.Data;
using Notes.Models;
using Notes.Repositories;
using Notes.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IRepositoryNotes, RepositotyNotes>();
builder.Services.AddTransient<IRepositoryNoteLike, RepositotyNoteLike>();
builder.Services.AddTransient<IRepositoryNoteImportance, RepositotyNoteImportance>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IRepositoryUser, RepositoryUser>();
builder.Services.AddTransient<IUserStore<User>, UserStore>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<SignInManager<User>>();
builder.Services.AddIdentityCore<User>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireNonAlphanumeric = false;
});

//configure the cookies for the authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignOutScheme = IdentityConstants.ApplicationScheme;
}).AddCookie(IdentityConstants.ApplicationScheme);

builder.Services.AddAutoMapper(typeof(Program));
//configure the db context for our models
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer("name=DefaultConnection"));

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Notes}/{action=Index}/{id?}");

app.Run();
