using CSharpGuardian.Core.Analysis;
using CSharpGuardian.Core.Analysis.Rules;
using CSharpGuardian.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// 添加服务
builder.Services.AddSingleton<CodeAnalyzer>(_ => new CodeAnalyzer(
    [new MagicNumberRule(), new LongMethodRule()], new AIService()));
builder.Services.AddCors(options =>
 {
     options.AddPolicy("AllowSpecificOrigin",
         builder => builder.WithOrigins("http://localhost:5173") // 替换为允许的前端地址
                           .AllowAnyHeader()
                           .AllowAnyMethod());
 });
// 注册控制器支持
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowSpecificOrigin"); // 启用 CORS
// 配置路由
app.MapControllers();

app.Run();

