using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Services;
using Swashbuckle.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// ★ 追加1: CORS（フロントからの通信許可）の設定
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNextJs", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Next.js のURLを許可
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// 1. Controllerを使えるように登録
builder.Services.AddControllers();

// Swaggerの登録
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext（SQLite）の登録
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// TaskItemServiceの登録
builder.Services.AddScoped<ITaskItemService, TaskItemService>();

var app = builder.Build();

// ★ 追加2: CORS ポリシーを有効化（必ず MapControllers の前に書く！）
app.UseCors("AllowNextJs");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();