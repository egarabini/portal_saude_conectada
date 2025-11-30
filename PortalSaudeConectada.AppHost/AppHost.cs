using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

// TODO: Adicionar Keycloak quando dispon�vel
// var keycloak = builder.AddKeycloak("keycloak", 8080);

// Obter a connection string do banco de dados configurada
var databaseKey = builder.Configuration.GetValue<string>("Database:Target");
Console.WriteLine($"=== AppHost - Configuração de Banco de Dados ===");
Console.WriteLine($"Database:Target = {databaseKey ?? "(não configurado)"}");

var connectionString = builder.Configuration.GetConnectionString(databaseKey ?? string.Empty)
    ?? builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
{
    Console.WriteLine("ERRO: Connection string não encontrada no AppHost!");
    Console.WriteLine("Verifique o arquivo appsettings.json do AppHost.");
    throw new InvalidOperationException("Connection string 'DefaultConnection' não configurada no AppHost.");
}

Console.WriteLine($"✓ Connection string encontrada: {connectionString.Substring(0, Math.Min(50, connectionString.Length))}...");

const string defaultConnectionEnv = "ConnectionStrings__DefaultConnection";

// Definir ambiente global - pode ser configurado via appsettings ou variável de ambiente
var applicationEnvironment = builder.Configuration.GetValue<string>("Application:Environment") ?? "Development";
const string aspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

// API Backend com Minimal APIs (Public, Admin, Auth) - Recebe a connection string do AppHost
var api = builder.AddProject<Projects.PortalSaudeConectada_Api>("api")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithEnvironment(defaultConnectionEnv, connectionString)
    .WithEnvironment(aspNetCoreEnvironment, applicationEnvironment);

// Portal Web Frontend
builder.AddProject<Projects.PortalSaudeConectada_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(api)
    .WaitFor(api)
    .WithEnvironment(aspNetCoreEnvironment, applicationEnvironment);


builder.Build().Run();
