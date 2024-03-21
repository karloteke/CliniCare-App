using CliniCareApp.Business;
using CliniCareApp.Data;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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



 
var app = builder.Build();

// Configure the HTTP request pipeline. 
//Cuando creo un contenedor(Docker) la api detectara que no es desarrollo y nunca se ejecutara.(Quitar el "if")
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
