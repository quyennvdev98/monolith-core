using Monolith.Core.Infrastructure;
using Monolith.Core.Infrastructure.Swagger;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers()
        .AddNewtonsoftJson(opts => opts.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc);

builder.Services.AddCoreInfrastructure(builder.Configuration);

builder.Services.AddSwagger(builder.Configuration); 

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocs();
}

app.MapControllers();
app.Run();


