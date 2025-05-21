using Yitter.IdGenerator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

YitIdHelper.SetIdGenerator(new IdGeneratorOptions(0));

builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.AddMemoryCache();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(opt =>
    {
        opt.AddDefaultPolicy(policy =>
        {
            policy
                .SetIsOriginAllowed((string _) => true)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
    });
}

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseCors();
}

// Configure the HTTP request pipeline.

app.MapHealthChecks("health");
app.UseAuthorization();
app.MapControllers();

app.Run();
