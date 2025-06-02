using StackExchange.Redis;
using Yitter.IdGenerator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

YitIdHelper.SetIdGenerator(new IdGeneratorOptions(0));

builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.AddSingleton<ConnectionMultiplexer>(_ =>
    ConnectionMultiplexer.Connect(
        builder.Configuration.GetSection("Redis").Get<string>()
            ?? throw new Exception("缺少Redis配置")
    )
);

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(opt =>
    {
        opt.AddDefaultPolicy(policy =>
        {
            policy
                .SetIsOriginAllowed(_ => true)
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

app.MapHealthChecks("health");
app.UseAuthorization();
app.MapControllers();

app.Run();
