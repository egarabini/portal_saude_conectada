using Npgsql;

var connectionString = "Host=161.97.141.186;Port=5432;Database=GCCuidadoDEV;Username=postgres;Password=Crazy#57LB";

Console.WriteLine("=== Corrigindo Tipos de Usuários ===");
Console.WriteLine();
Console.WriteLine("Enum TipoUsuario:");
Console.WriteLine("  Administrador = 1");
Console.WriteLine("  Profissional = 2");
Console.WriteLine("  Gestor = 3");
Console.WriteLine("  Usuario = 4");
Console.WriteLine();

try
{
    await using var connection = new NpgsqlConnection(connectionString);
    await connection.OpenAsync();

    Console.WriteLine("✓ Conectado ao banco de dados");
    Console.WriteLine();

    // Atualizar tipos dos usuários
    var updateSql = @"
UPDATE saude_conectada.usuarios
SET tipo = CASE username
    WHEN 'admin' THEN 1
    WHEN 'gestor' THEN 3
    WHEN 'profissional' THEN 2
    ELSE tipo
END,
updated_at = NOW()
WHERE username IN ('admin', 'gestor', 'profissional')
  AND is_deleted = false
RETURNING username, tipo;
";

    await using var cmd = new NpgsqlCommand(updateSql, connection);
    await using var reader = await cmd.ExecuteReaderAsync();

    Console.WriteLine("Usuários atualizados:");
    while (await reader.ReadAsync())
    {
        var username = reader.GetString(0);
        var tipo = reader.GetInt32(1);
        var tipoNome = tipo switch
        {
            1 => "Administrador",
            2 => "Profissional",
            3 => "Gestor",
            4 => "Usuario",
            _ => "Desconhecido"
        };
        Console.WriteLine($"  ✓ {username} -> tipo = {tipo} ({tipoNome})");
    }

    Console.WriteLine();
    Console.WriteLine("✅ Tipos de usuários corrigidos com sucesso!");
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Erro: {ex.Message}");
    Console.WriteLine(ex.StackTrace);
    return 1;
}

return 0;

