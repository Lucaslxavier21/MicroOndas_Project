using Microondas.Data.Repositories;
using MicroOndas_Project.Context;
using MicroOndas_Project.Interfaces;
using MicroOndas_Project.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddRazorPages();

builder.Services.AddSingleton<DapperContext>();

builder.Services.AddScoped<ProgramaService>();
builder.Services.AddScoped<MicroondasService>();
builder.Services.AddScoped<IProgramaRepository, ProgramaRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API v1"); 
        options.RoutePrefix = string.Empty; 
    });
}

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
    pattern: "{controller=Microondas}/{action=Index}/{id?}"); 

app.MapControllers(); 

app.Run();
