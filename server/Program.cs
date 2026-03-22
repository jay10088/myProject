using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// --- 服務註冊區 ---
builder.Services.AddOpenApi();

builder.Services.AddCors(options => {
  options.AddPolicy("AllowReact", policy => 
    policy.WithOrigins("http://localhost:5173")
      .AllowAnyMethod()
      .AllowAnyHeader());
});

// 註冊 Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(sp => {
  var configuration = builder.Configuration.GetConnectionString("RedisConnection");
  return ConnectionMultiplexer.Connect(configuration);
});

var app = builder.Build();

// --- 中間件設定區 ---
if (app.Environment.IsDevelopment()) {
  app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("AllowReact");

// --- API 路由區 ---

// 測試 Redis 寫入：存入資料
app.MapPost("/redis/test", async (IConnectionMultiplexer redis, string value) => {
  var db = redis.GetDatabase();
  await db.StringSetAsync("test_key", value);
  return Results.Ok($"已將 '{value}' 存入 Redis！");
});

// 測試 Redis 讀取：取出資料
app.MapGet("/redis/test", async (IConnectionMultiplexer redis) => {
  var db = redis.GetDatabase();
  var value = await db.StringGetAsync("test_key");
  return Results.Ok(new { Key = "test_key", Value = value.ToString() });
});

// 原本的天氣預報 API
app.MapGet("/weatherforecast", () => {
  var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
  var forecast = Enumerable.Range(1, 5).Select(index =>
    new WeatherForecast(
      DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
      Random.Shared.Next(-20, 55),
      summaries[Random.Shared.Next(summaries.Length)]
    )).ToArray();
  return forecast;
}).WithName("GetWeatherForecast");

app.Run();

// --- 資料模型 ---
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary) {
  public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}