using BOs.Entity;
using BOs.Filer;
using DAOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using REPOs;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Nạp appsettings.json trước khi cấu hình JWT
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add
builder.Services.AddScoped<IGuestDAO, GuestDAO>();
builder.Services.AddScoped<IGuestRepository, GuestRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IBranchDAO, BranchDAO>();
builder.Services.AddScoped<IBranchRepository, BranchRepository>();
builder.Services.AddScoped<IRoomDAO, RoomDAO>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<ISlotDAO, SlotDAO>();
builder.Services.AddScoped<ISlotRepository, SlotRepository>();


// Nạp JWT Secret Key từ cấu hình
var jwtSecretKey = builder.Configuration["Jwt:SecretKey"];
if (string.IsNullOrEmpty(jwtSecretKey))
{
    throw new Exception("JWT Secret Key is missing or empty in appsettings.json");
}

// Đảm bảo rằng IJWTService được khởi tạo đúng cách
builder.Services.AddScoped<IJWTService>(provider =>
    new JWTService(jwtSecretKey));

// Cấu hình Authentication và JWT Bearer
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // Để dễ dàng cho phát triển
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecretKey)), // Sử dụng jwtSecretKey
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Đảm bảo cấu hình DbContext
builder.Services.AddDbContext<BookroomSwdContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddTransient<BookroomSwdContext>();

var app = builder.Build();

// Thực hiện migrate database và seed roles
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BookroomSwdContext>();
    context.Database.Migrate();

    // Seed default roles
    if (!context.Roles.Any())
    {
        context.Roles.AddRange(
            new Role { RoleName = "Admin" },
            new Role { RoleName = "Staff" },
            new Role { RoleName = "Customer" }
        );
        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
