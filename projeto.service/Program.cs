using projeto.infra.Data;

var builder = WebApplication.CreateBuilder(args);
var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

/*Realizado configurações do Swagger, para que seja possivel estar authorizando via webtoken*/
builder.Services.AddSwaggerConfiguration();

/*Realizado injeção de dependencia dos dados necessários*/
builder.Services.AddDependencies();

/*Adicionado o worker no backend*/
builder.Services.AddHostedService<RabbitConsumer>();

/*Configurações referentes ao Json Web Token*/
builder.Services.AddJwtToken(builder.Configuration);

builder.Services.AddCors();
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(c =>
{
    c.AllowAnyOrigin();
    c.AllowAnyHeader();
    c.AllowAnyMethod();
});
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");
app.Run();