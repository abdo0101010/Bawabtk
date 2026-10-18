using CleanArchitecture.Infrastructre;
using CleanArchitecture.Service;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var AddBawabtkDbContextString = builder.Configuration.GetSection("ConnectionStrings:DefaultConnection");
builder.Services.AddBawabtkDbContext(AddBawabtkDbContextString.Value!);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // إضافة هذا السطر يخبر Swagger باستخدام المسار الكامل للكلاس كمعرف فريد
    options.CustomSchemaIds(type => type.FullName);
}); builder.Services.AddInfrastructureResgistertion();
builder.Services.AddServicesRegistrations();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
