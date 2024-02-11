using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors( options => options.AddPolicy("AllowWebApp",
    buil => buil.AllowAnyHeader().
    AllowAnyMethod().
    SetIsOriginAllowed(origin => true).
    AllowCredentials()
));
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();




//var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");
//builder.Services.AddDbContext<Conexion2>(option => option.UseNpgsql(connectionString));

//configure the app services
//builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
var env = builder.Environment;
app.UseRouting();
app.UseSwagger();

/*
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

});*/

app.UseSwaggerUI(c =>
{
  c.RoutePrefix = "";
  c.SwaggerEndpoint("/swagger/v1/swagger.json", "sistemaVentas");
});

/*
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "sistemaVentas"));*/


app.UseCors("AllowWebApp");
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

});

app.Run();


