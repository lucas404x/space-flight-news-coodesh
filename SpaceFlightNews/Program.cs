using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using SpaceFlightNews.Infrastructure.Database;
using SpaceFlightNews.Infrastructure.Repositories;
using SpaceFlightNews.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseSettings>((options) =>
{
    options.ConnectionString = builder.Configuration.GetSection("Mongo:ConnectionString").Value;
    options.DatabaseName = builder.Configuration.GetSection("Mongo:Database").Value;
});

builder.Services.AddScoped<IDatabaseContext, DatabaseContext>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<IArticleServices, ArticleServices>();

ConventionPack pack = new() { new EnumRepresentationConvention(BsonType.String) };
ConventionRegistry.Register("EnumStringConvention", pack, t => true);


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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
