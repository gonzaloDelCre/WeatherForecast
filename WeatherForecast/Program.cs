using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WeatherForecast.Application.AccessLog.UseCases;
using WeatherForecast.Application.Member.UseCases;
using WeatherForecast.Domain.Ports;
using WeatherForecast.Infrastructure.Persistence.DBConexion;
using WeatherForecast.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurate conection to database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Member
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<CreateMemberUseCase>();
builder.Services.AddScoped<DeleteMemberByIdUseCase>();
builder.Services.AddScoped<GetAllMembersUseCase>();
builder.Services.AddScoped<GetMemberByIdUseCase>();

//AccessLog
builder.Services.AddScoped<IAccessLogsRepository, AccessLogsRepository>();
builder.Services.AddScoped<AccessErrorUseCase>();
builder.Services.AddScoped<CreateAccessLogUseCase>();
builder.Services.AddScoped<GetAccessLogByIdUseCase>();
builder.Services.AddScoped<GetAllAccessLogsUseCase>();
builder.Services.AddScoped<DeleteAccessLogByIdUseCase>();



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
