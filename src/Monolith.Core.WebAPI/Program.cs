using Monolith.Core.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCoreInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();


app.Run();


