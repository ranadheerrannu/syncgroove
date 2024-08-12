using ConnectingApps.SmartInject;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SyncGroove;
using SyncGroove.Interface;
using SyncGroove.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<SyncGrooveDbContext>(
    options=>options.UseNpgsql("Host=syncgroovebackend.postgres.database.azure.com; Database=syncgroove-4; Port=5432; Username=prasannanaik; Password=aryaP*0027;")
    );
builder.Services.AddControllers()
    .AddJsonOptions(opt => opt.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddCors(policyBuilder =>
    policyBuilder.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
    )
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IJiraWorkItemService, JiraWorkItemService>();
builder.Services.AddScoped<IAzureWorkItemService, AzureWorkItemService>();
builder.Services.AddScoped<IWorkItemMappingService, WorkItemMappingService>();
builder.Services.AddScoped<ISyncService, SyncService>();
var app = builder.Build();

// Add Content Security Policy middleware
//app.Use(async (context, next) =>
//{
//    context.Response.Headers.Add("Content-Security-Policy-Report-Only", "default-src 'self'; script-src 'self' https://api.atlassian.com/metal/ingest https://syncgroove-backend.azurewebsites.net  https://marketplace.atlassian.com");
//    await next();
//});


app.UseCors();

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
