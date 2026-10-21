using CleanArchitecture.Infrastructre;
using CleanArchitecture.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var AddBawabtkDbContextString = builder.Configuration.GetSection("ConnectionStrings:DefaultConnection");
builder.Services.AddBawabtkDbContext(AddBawabtkDbContextString.Value!);

builder.Services.AddEndpointsApiExplorer();

string securityKey = "this is my custom Secret key for authentication";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey)),
            ValidateLifetime = true,
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(c =>
{
    c.CustomSchemaIds(type => type.FullName?.Replace('+', '_'));
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "put brear tokean"
    });

    c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddInfrastructureResgistertion();
builder.Services.AddServicesRegistrations();

var app = builder.Build();
app.Use(async (context, next) =>
{
    try
    {
        await next(context);
    }
    catch (Exception ex)
    {
        // 1. إجبار المتصفح على قبول الرد حتى لو حدث خطأ (حل مشكلة سقوط الـ CORS)
        context.Response.Headers.Append("Access-Control-Allow-Origin", "*");

        // 2. تحديد نوع الرد ليكون JSON
        context.Response.ContentType = "application/json";

        // 3. جعل حالة الطلب 400 لكي يدخل الـ React في بلوك الـ else
        context.Response.StatusCode = 400;

        // 4. إرسال الرسالة القادمة من (ResultException)
        await context.Response.WriteAsJsonAsync(new { message = ex.Message });
    }
});
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();