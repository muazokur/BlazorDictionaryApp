using BlazorDictionary.Api.Application.Extensions;
using BlazorDictionary.Api.WebApi.Infrastructure.ActionFilters;
using BlazorDictionary.Api.WebApi.Infrastructure.Extensions;
using BlazorDictionary.Infrastructure.Persistence.Extensions;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt=>opt.Filters.Add<ValidationModelFilter>())
    .AddJsonOptions(opt => 
    {
        opt.JsonSerializerOptions.PropertyNamingPolicy = null;
    })
    .AddFluentValidation().ConfigureApiBehaviorOptions(o=>o.SuppressModelStateInvalidFilter=true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureAuth(builder.Configuration);


builder.Services.AddApplicationRegistiration();
builder.Services.AddInfrastructureRegistiration(builder.Configuration);


//AddCors
builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
}));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.ConfigureExceptionHandling(app.Environment.IsDevelopment());

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("MyPolicy");

app.MapControllers();

app.Run();
