using PeticionService.Policies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient("Test").AddPolicyHandler(
    peticion => peticion.Method == HttpMethod.Get ? new ClientePolicy().ImmediatoHttpRetry : new ClientePolicy().LinearHttpRetry); //engadimos HttpClient factory para evitar connection exhaustion, o cal soe pasar se non usamos unha factoria e utilizamos conexions individuales

builder.Services.AddSingleton<ClientePolicy>(new ClientePolicy()); //rexistramos ClientePolicy para o noso container con Dependency Injection para que este disponible para seu uso
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
