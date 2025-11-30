using Npgsql;
using BCrypt.Net;

var connectionString = "Host=161.97.141.186;Port=5432;Database=GCCuidadoDEV;Username=postgres;Password=Crazy#57LB";

Console.WriteLine("=== Populando Banco de Dados ===");
Console.WriteLine();

try
{
    await using var connection = new NpgsqlConnection(connectionString);
    await connection.OpenAsync();

    Console.WriteLine("✓ Conectado ao banco de dados");
    Console.WriteLine();

    // Verificar se já existem usuários
    var checkSql = "SELECT COUNT(*) FROM saude_conectada.usuarios WHERE is_deleted = false";
    await using var checkCmd = new NpgsqlCommand(checkSql, connection);
    var count = (long)(await checkCmd.ExecuteScalarAsync() ?? 0L);

    if (count > 0)
    {
        Console.WriteLine($"⚠ Já existem {count} usuário(s) no banco de dados.");
        Console.WriteLine("Continuando para adicionar usuários de teste...");
        Console.WriteLine();
    }

    Console.WriteLine("Criando usuários de teste...");
    Console.WriteLine();

    // Hash das senhas
    var adminPassword = BCrypt.Net.BCrypt.HashPassword("Admin@123");
    var gestorPassword = BCrypt.Net.BCrypt.HashPassword("Gestor@123");
    var profissionalPassword = BCrypt.Net.BCrypt.HashPassword("Prof@123");

    // Inserir usuários
    var insertSql = @"
INSERT INTO saude_conectada.usuarios
    (nome, email, username, password_hash, cpf, telefone, tipo, status, email_verificado, created_at, is_deleted)
VALUES
    (@nome, @email, @username, @password_hash, @cpf, @telefone, @tipo, @status, @email_verificado, @created_at, false)
ON CONFLICT (email) DO NOTHING
RETURNING id, username;
";

    var usuarios = new[]
    {
        new { Nome = "Administrador do Sistema", Email = "admin@saudeconectada.com.br", Username = "admin", Password = adminPassword, Cpf = "12345678901", Telefone = "(11) 98765-4321", Tipo = 1, Status = 1 }, // Administrador = 1
        new { Nome = "Gestor da Equipe", Email = "gestor@saudeconectada.com.br", Username = "gestor", Password = gestorPassword, Cpf = "98765432109", Telefone = "(11) 98765-4322", Tipo = 3, Status = 1 }, // Gestor = 3
        new { Nome = "Profissional de Saúde", Email = "profissional@saudeconectada.com.br", Username = "profissional", Password = profissionalPassword, Cpf = "45678912345", Telefone = "(11) 98765-4323", Tipo = 2, Status = 1 }
    };

    var created = 0;
    foreach (var usuario in usuarios)
    {
        await using var cmd = new NpgsqlCommand(insertSql, connection);
        cmd.Parameters.AddWithValue("nome", usuario.Nome);
        cmd.Parameters.AddWithValue("email", usuario.Email);
        cmd.Parameters.AddWithValue("username", usuario.Username);
        cmd.Parameters.AddWithValue("password_hash", usuario.Password);
        cmd.Parameters.AddWithValue("cpf", usuario.Cpf);
        cmd.Parameters.AddWithValue("telefone", usuario.Telefone);
        cmd.Parameters.AddWithValue("tipo", usuario.Tipo);
        cmd.Parameters.AddWithValue("status", usuario.Status);
        cmd.Parameters.AddWithValue("email_verificado", true);
        cmd.Parameters.AddWithValue("created_at", DateTime.UtcNow);

        await using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            var id = reader.GetGuid(0);
            var username = reader.GetString(1);
            Console.WriteLine($"  ✓ Criado: {username} (ID: {id})");
            created++;
        }
        else
        {
            Console.WriteLine($"  ⚠ Já existe: {usuario.Username}");
        }
    }

    Console.WriteLine();
    Console.WriteLine($"✓ {created} usuário(s) criado(s) com sucesso!");
    Console.WriteLine();
    Console.WriteLine("Usuários disponíveis:");
    Console.WriteLine("  - admin / Admin@123 (Administrador)");
    Console.WriteLine("  - gestor / Gestor@123 (Gestor)");
    Console.WriteLine("  - profissional / Prof@123 (Profissional de Saúde)");
    Console.WriteLine();
    Console.WriteLine("Próximo passo: Inicie a aplicação e faça login!");
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
