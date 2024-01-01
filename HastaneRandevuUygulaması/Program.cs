using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HastaneRandevuUygulaması.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<HastaneRandevuUygulamasıContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HastaneRandevuUygulamasıContext") ?? throw new InvalidOperationException("Connection string 'HastaneRandevuUygulamasıContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(45);
}

);
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
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
