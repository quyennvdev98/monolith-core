using Microsoft.AspNetCore.Mvc;
using Monolith.Core.Application.Responses;
using Monolith.Core.Infrastructure;
using Monolith.Core.Infrastructure.Exceptions;
using Monolith.Core.Infrastructure.Swagger;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers()
        .AddNewtonsoftJson(opts => opts.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc)
        .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .SelectMany(kvp => kvp.Value!.Errors.Select(e => new ApiError(
                            $"Validation.{kvp.Key}",
                            e.ErrorMessage,
                            kvp.Key)))
                        .ToList();

                    var response = ApiResponse.FailureResult(
                        "Validation failed. Please check your input.",
                        errors);

                    return new BadRequestObjectResult(response);
                };
            });
builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

builder.Services.AddCoreInfrastructure(builder.Configuration);

builder.Services.AddSwagger(builder.Configuration);

var app = builder.Build();

app.UseErrorHandling(); 

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocs();
    app.UseCors();
}

app.UseHttpsRedirection();
      
app.MapControllers();
app.Run();


