using CloudNauticalSolution.DataAccess.Repository;
using CloudNauticalSolution.Domain;
using CloudNauticalSolution.Domain.AbstractClasses.IDomain;
using CloudNauticalSolution.Domain.AbstractClasses.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICustomerOrderData, CustomerOrderData>();
builder.Services.AddScoped<ICustomerOrderService, CustomerOrderService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
