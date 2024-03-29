using Microsoft.EntityFrameworkCore;
using WorldAPI.CommonMapping;
using WorldAPI.Data;
using WorldAPI.Repository;
using WorldAPI.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Configure CORS

builder.Services.AddCors(option =>
{
    option.AddPolicy("CustomPolicy", x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

#endregion 

#region Configure Database

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
#endregion

#region Configure AutoMapper

builder.Services.AddAutoMapper(typeof(MappingProfile));

#endregion

#region Configure Repository Pattern

builder.Services.AddTransient(typeof(IGenericRepository<>),typeof(GenericRepository<>));
builder.Services.AddTransient<ICountryRepository,CountryRepository>();
builder.Services.AddTransient<IStatesRepository, StatesRepository>();

#endregion

builder.Services.AddControllers();
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

app.UseCors("CustomPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
