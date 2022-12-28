using YelpReviewDataExtractor.Models;
using YelpReviewDataExtractor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<YelpSetting>(builder.Configuration.GetSection(nameof(YelpSetting)));

builder.Services.Configure<GoogleVisionSetting>(builder.Configuration.GetSection(nameof(GoogleVisionSetting)));

builder.Services.AddTransient<IYelpService, YelpService>();

builder.Services.AddControllers();

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
