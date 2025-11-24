using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using MailXmlProcessor.Infrastructure.Persistence;
using MailXmlProcessor.Infrastructure.Configuration;

using MailXmlProcessor.Application.Interfaces;
using MailXmlProcessor.Application.Services;
using MailXmlProcessor.Domain.Services;
using MailXmlProcessor.Infrastructure.Services;
using MailXmlProcessor.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

#region Configure Services
builder.Services.Configure<ExtractionSettings>(
    builder.Configuration.GetSection("ExtractionSettings"));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<MailXmlDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IEmailRepository, EmailRepository>();

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

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCorsPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200") // TODO: use configured dev frontend URL
            .AllowAnyMethod()
            .AllowAnyHeader();
    });

    options.AddPolicy("ProdCorsPolicy", policy =>
    {
        policy
            .WithOrigins("https://domain.com") // TODO: use configured production domain
            .AllowAnyMethod()
            .AllowAnyHeader();
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

if (app.Environment.IsDevelopment())
{
    app.UseCors("DevCorsPolicy");
}
else
{
    app.UseCors("ProdCorsPolicy");
}

if (!app.Environment.IsDevelopment())
    app.UseHttpsRedirection();
    
app.MapControllers();

app.Run();
