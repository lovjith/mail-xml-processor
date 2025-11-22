using Microsoft.OpenApi.Models;

using MailXmlProcessor.Application.Interfaces;
using MailXmlProcessor.Application.Services;
using MailXmlProcessor.Domain.Services;
using MailXmlProcessor.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

#region Configure Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MailXmlProcessor API",
        Version = "v1",
        Description = "Enterprise API to extract and process structured XML and tagged data from email content.",
        Contact = new OpenApiContact
        {
            Name = "Platform Engineering",
            Email = "developer@company.com"
        }
    });
});
# endregion

#region Dependency Injection
builder.Services.AddScoped<IEmailParser, EmailExtractionService>();
builder.Services.AddScoped<IEmailExtractionService, EmailProcessingService>();
#endregion

var app = builder.Build();

// Middleware Placeholder

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
