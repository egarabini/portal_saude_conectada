using Npgsql;
using BCrypt.Net;

var connectionString = "Host=161.97.141.186;Port=5432;Database=GCCuidadoDEV;Username=postgres;Password=Crazy#57LB";

Console.WriteLine("=== Testando Login ===");
Console.WriteLine();

try
{
    await using var connection = new NpgsqlConnection(connectionString);
    await connection.OpenAsync();

    Console.WriteLine("✓ Conectado ao banco de dados");
    Console.WriteLine();

    // Buscar usuário admin
    var sql = "SELECT id, username, email, password_hash, tipo, status FROM saude_conectada.usuarios WHERE username = @username AND is_deleted = false";
    await using var cmd = new NpgsqlCommand(sql, connection);
    cmd.Parameters.AddWithValue("username", "admin");

    await using var reader = await cmd.ExecuteReaderAsync();

    if (!await reader.ReadAsync())
    {
        Console.WriteLine("✗ Usuário 'admin' não encontrado no banco de dados!");
        return;
    }

    var id = reader.GetGuid(0);
    var username = reader.GetString(1);
    var email = reader.GetString(2);
    var passwordHash = reader.GetString(3);
    var tipo = reader.GetInt32(4);
    var status = reader.GetInt32(5);

    Console.WriteLine($"Usuário encontrado:");
    Console.WriteLine($"  ID: {id}");
    Console.WriteLine($"  Username: {username}");
    Console.WriteLine($"  Email: {email}");
    Console.WriteLine($"  Tipo: {tipo} (0=Admin, 1=Gestor, 2=Profissional)");
    Console.WriteLine($"  Status: {status} (0=PendenteAtivacao, 1=Ativo, 2=Inativo, 3=Bloqueado)");
    Console.WriteLine($"  Password Hash: {passwordHash.Substring(0, 20)}...");
    Console.WriteLine();

    // Testar senha
    var testPassword = "Admin@123";
    Console.WriteLine($"Testando senha: {testPassword}");

    var isValid = BCrypt.Net.BCrypt.Verify(testPassword, passwordHash);

    if (isValid)
    {
        Console.WriteLine("✓ Senha VÁLIDA!");
    }
    else
    {
        Console.WriteLine("✗ Senha INVÁLIDA!");
        Console.WriteLine();
        Console.WriteLine("Gerando novo hash para comparação:");
        var newHash = BCrypt.Net.BCrypt.HashPassword(testPassword);
        Console.WriteLine($"  Novo hash: {newHash}");
        Console.WriteLine($"  Hash no banco: {passwordHash}");
        Console.WriteLine();
        Console.WriteLine("Testando novo hash:");
        var newHashValid = BCrypt.Net.BCrypt.Verify(testPassword, newHash);
        Console.WriteLine($"  Novo hash válido: {newHashValid}");
    }

    Console.WriteLine();

    // Verificar status
    if (status != 1)
    {
        Console.WriteLine($"⚠ ATENÇÃO: Usuário não está ATIVO! Status atual: {status}");
        Console.WriteLine("  Para ativar, execute:");
        Console.WriteLine($"  UPDATE saude_conectada.usuarios SET status = 1 WHERE username = 'admin';");
    }
    else
    {
        Console.WriteLine("✓ Usuário está ATIVO");
    }
}
catch (Exception ex)
{
    Console.WriteLine();
    Console.WriteLine($"✗ Erro: {ex.Message}");
    Console.WriteLine();
    if (ex.InnerException != null)
    {
        Console.WriteLine($"Detalhes: {ex.InnerException.Message}");
    }
    Environment.Exit(1);
}
