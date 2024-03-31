using CliniCareApp.Business;
using CliniCareApp.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

// Configure JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]))
        };
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CliniCare API", Version = "v1" });

    // Configure the security scheme for JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


//Inyecto dependencia de Patient
 builder.Services.AddScoped<IPatientRepository, PatientRepository>();
 builder.Services.AddScoped<IPatientService, PatientService>();

 //Inyecto dependencia de Appointment
 builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
 builder.Services.AddScoped<IAppointmentService, AppointmentService>();

//Inyecto dependencia de MedicalRecord
 builder.Services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();
 builder.Services.AddScoped<IMedicalRecordService, MedicalRecordService>();

 //Inyecto dependencia de AppointmentPatient
 builder.Services.AddScoped<IAppointmentPatientRepository, AppointmentPatientRepository>();
 builder.Services.AddScoped<IAppointmentPatientService, AppointmentPatientService>();
 
 //Inyecto dependencia de User
 builder.Services.AddScoped<IUserRepository, UserRepository>();
 builder.Services.AddScoped<IUserService, UserService>();

//Inyecto PrivateAreaAccess
 builder.Services.AddScoped<PrivateAreaAccess>();

 //Inyecto PatientEF
 builder.Services.AddScoped<IPatientRepository, PatientEFRepository>();

  //Inyecto AppointmentEF
 builder.Services.AddScoped<IAppointmentRepository, AppointmentEFRepository>();



// Obteniendo la cadena de conexión desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("ServerDB_localhost");

builder.Services.AddDbContext<CliniCareContext>(options =>
    options.UseSqlServer(connectionString)
);


 
var app = builder.Build();

// Configure the HTTP request pipeline.
var env = app.Environment;


if (env.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CliniCare v1"));
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

