using DashboardApi;
using DashboardApi.DataAccess;
using DashboardApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext();
});
Serilog.Debugging.SelfLog.Enable(Console.WriteLine);

builder.Services.AddTransient<GlobalErrorHandlingMiddeware>();
builder.Services.AddTransient<RequestLogContextMiddleware>();

// Add Sqlite database context
builder.Services.AddDbContext<FinancesDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("FinancesDb");
    options.UseSqlite(connectionString);
});

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        corsPolicyBuilder =>
        {
            corsPolicyBuilder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IFileRepository, FileRepository>();
builder.Services.AddSingleton<IFileReader, FileReader>();
builder.Services.AddScoped<IFinancesRepository, FinancesRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();
app.UseMiddleware<GlobalErrorHandlingMiddeware>();
app.UseMiddleware<RequestLogContextMiddleware>();


app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();