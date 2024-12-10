using System.Text.Json.Serialization;
using DotnetSkyApiPostgres.Models;
using DotnetSkyApiPostgres.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("default")!;

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(opts => {
        var enumConverter = new JsonStringEnumConverter();
        opts.JsonSerializerOptions.Converters.Add(enumConverter);
    }
);

builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
});

// ApiServiceInfo.Setup(builder.Configuration);

// builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddDbContext<ApplicationDbContext>(op => op.UseNpgsql(connectionString));

builder.Services.AddScoped<IUsersService, UsersService>();

var app = builder.Build();

app.UseCors(x => x.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseAuthentication();
// app.UseMiddleware<ErrorHandlerMiddleware>();
// app.UseAuthorization();

app.MapControllers();

app.Run();