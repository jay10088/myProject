using server.Data;
using server.Models;
using DotNetEnv;

DotNetEnv.Env.Load("../.env");

var builder = WebApplication.CreateBuilder(args);


// 1. 取得連線字串
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? "";

// 2. 註冊服務 (Dependency Injection)
builder.Services.AddSingleton(new DbService(connectionString));

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => {
    options.SerializerOptions.PropertyNamingPolicy = null; 
});

// 3. 設定 CORS
builder.Services.AddCors(options => {
    options.AddPolicy("AllowReact", policy => 
        policy.WithOrigins("http://localhost:5173").AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();
app.UseCors("AllowReact");

// 4. API 路由 - 產品清單
app.MapGet("/api/products", async (DbService db) => {
    try {
        var products = await db.GetProductsAsync();
        return Results.Ok(products);
    }
    catch (Exception ex) {
        return Results.Problem($"讀取失敗: {ex.Message}");
    }
});

// 5. API 路由 - 新增訂單
app.MapPost("/api/orders", async (OrderRequest req, DbService db) => {
    try {
        await db.CreateOrderAsync(req.TotalAmount);
        return Results.Ok(new { message = "訂單已成功存入資料庫！" });
    }
    catch (Exception ex) {
        return Results.Problem($"存入失敗: {ex.Message}");
    }
});

app.Run();