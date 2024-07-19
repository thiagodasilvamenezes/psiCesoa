using CesoaPsiPt.Simulador.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddScoped<ISimuladorHabitacional, SimuladorHabitacional>();

builder.Services.AddCors(options =>
{
	options.AddPolicy("CorsPolitica", builder => builder
			.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader()
			.AllowAnyMethod()
		);


});


var app = builder.Build();
// Configure the HTTP request pipeline.


app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CesoaPsiPT.Simulador v1"));


app.UseCors("CorsPolitica");
app.UseAuthorization();

app.MapControllers();

app.Run();
