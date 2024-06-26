using SkillsExam.Interfaces;
using SkillsExam.Services;
using SkillsExam.Util;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IToDoService, ToDoService>();

builder.Services.Configure<GeneralSetting>(
builder.Configuration.GetSection("ToDoService"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
				app.UseSwagger();
				app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
