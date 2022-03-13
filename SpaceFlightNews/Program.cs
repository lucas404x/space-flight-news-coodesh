using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using SpaceFlightNews.Infrastructure.Database;
using SpaceFlightNews.Infrastructure.Repositories;
using SpaceFlightNews.Invocables;
using SpaceFlightNews.Services;
using Coravel;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseSettings>((options) =>
{
    options.ConnectionString = builder.Configuration.GetSection("Mongo:ConnectionString").Value;
    options.DatabaseName = builder.Configuration.GetSection("Mongo:Database").Value;
});


builder.Services.AddScoped<IDatabaseContext, DatabaseContext>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<IArticleServices, ArticleServices>();

builder.Services.AddScheduler();
builder.Services.AddScoped<GetDailyNewsJob>();

ConventionPack pack = new() { new EnumRepresentationConvention(BsonType.String) };
ConventionRegistry.Register("EnumStringConvention", pack, t => true);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.Services.UseScheduler(scheduler =>
{
    scheduler.Schedule<GetDailyNewsJob>()
    .DailyAtHour(9)
    .Zoned(TimeZoneInfo.Local);
});

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
