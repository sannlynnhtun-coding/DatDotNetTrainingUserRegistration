using DatDotNetTrainingUserRegistration.Database.AppDbContextModels;
using DatDotNetTrainingUserRegistration.Domain.Features.Login;
using DatDotNetTrainingUserRegistration.Domain.Features.Product;
using DatDotNetTrainingUserRegistration.Domain.Features.Profile;
using DatDotNetTrainingUserRegistration.Domain.Features.Register;
using DatDotNetTrainingUserRegistration.Domain.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    //opt.UseSqlServer(builder.Configuration["ConnectionStrings:DbConnection"]);
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
});

builder.Services.AddScoped<RegisterService>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<ProfileService>();
builder.Services.AddScoped<UserSessionService>();
builder.Services.AddScoped<ProductService>();

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
