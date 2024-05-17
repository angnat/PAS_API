using PAS_API.Data;
using Microsoft.EntityFrameworkCore;
using PAS_API.Repository.IRepository;
using PAS_API.Repository;
using PAS_API;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Serilog;
using PAS_API.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(option => {
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});

Log.Logger = new LoggerConfiguration().MinimumLevel.Information()
    .WriteTo.File("log/pasapiLog.txt",rollingInterval:RollingInterval.Month).CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddScoped<IUnitRepository, UnitRepository>();
builder.Services.AddScoped<ITUnitRepository, TUnitRepository>();
builder.Services.AddScoped<IAdminUnitTeknikRepository, AdminUnitTeknikRepository>();
builder.Services.AddScoped<IProgressRepository, ProgressRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IClusterRepository, ClusterRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerAddressRepository, CustomerAddressRepository>();
builder.Services.AddScoped<ICustomerCommunicationRepository, CustomerCommunicationRepository>();
builder.Services.AddScoped<IAdminUnitTeknikSilverRepository, AdminUnitTeknikSilverRepository>();
builder.Services.AddScoped<IAdminUnitPengalihanListHutangRepository, AdminUnitPengalihanListHutangRepository>();
builder.Services.AddScoped<IAdminUnitPengalihanRepository, AdminUnitPengalihanRepository>();
builder.Services.AddSingleton<ILogging, Logging>();
builder.Services.AddHttpContextAccessor();
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor |
    Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto;
});


builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddControllers(option => {
    //option.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/*Untuk di Prod

*/

builder.WebHost.UseUrls("http://0.0.0.0:5001/");

/* Untuk DEV jalan swagger UI 

*/
builder.WebHost.UseIISIntegration();


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}
app.UseForwardedHeaders();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
