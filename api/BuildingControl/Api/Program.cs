using Api;
using Api.Configurations;
using Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenTelemetry(builder.Configuration);
builder.Host.AddSerilog();
builder.Services.AddApplicationServices();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseOpenTelemetryPrometheusScrapingEndpoint();
app.MapEndpoints();

app.Run();
