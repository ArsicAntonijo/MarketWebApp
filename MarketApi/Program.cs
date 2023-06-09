﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DataLayer.Data;
using Service;
using DataLayer.Repositories;
using System.Text.Json.Serialization;
using MarketApi.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MarketApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MarketApiContext") ?? throw new InvalidOperationException("Connection string 'MarketApiContext' not found.")));

builder.Services.AddTransient<SalesService>();
builder.Services.AddTransient<SalesRepository>();
builder.Services.AddTransient<StorageService>();
builder.Services.AddTransient<StorageRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSignalR();
// Add services to the container.


//enable CORS
//var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: MyAllowSpecificOrigins,
//                      policy =>
//                      {
//                          policy.WithOrigins("https://localhost:7048",
//                                              "http://localhost:5155")
//                                .AllowAnyHeader()
//                                .AllowAnyMethod()
//                                .AllowAnyOrigin();
//                      });
//});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseCors(MyAllowSpecificOrigins);

// global cors policy
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()); // allow credentials


app.UseAuthorization();

app.MapControllers();
app.MapHub<ItemHub>("/hubs/itemHub");

app.Run();
