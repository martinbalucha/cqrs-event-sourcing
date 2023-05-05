using SocialMedia.Command.Infrascturture.Config;
using SocialMedia.Command.Infrascturture.Persistence;
using SocialMedia.CQRS.Core.Domain;
using SocialMedia.CQRS.Core.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MongoDbConfiguration>(builder.Configuration.GetSection(nameof(MongoDbConfiguration)));
builder.Services.AddScoped<IEventStoreRepository, MongoEventStoreRepository>();
builder.Services.AddScoped<IEventStore, EventStore>();


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
