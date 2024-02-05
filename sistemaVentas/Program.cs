var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

//configure the app services
//builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
  c.RoutePrefix = "";
  c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});

app.Run();


