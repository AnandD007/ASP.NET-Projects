using Microsoft.EntityFrameworkCore;
using MatterManagementWebApp.Services.Data.DBContext;
using MatterManagementWebApp.Services.Repository;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MatterDBContext>(options =>
options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}
builder.Services.AddScoped<IMatterRepository, MatterRepository>();
builder.Services.AddScoped<IJurisdictionRepository, JurisdictionRepository>();
builder.Services.AddScoped<IAttorneyRepository, AttorneyRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();

builder.Services.AddCors(o => o.AddPolicy("ReactPolicy", builder =>
{
    builder.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin();
}));

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("ReactPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
