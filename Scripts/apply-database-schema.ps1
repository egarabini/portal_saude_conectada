# Script para aplicar o schema do banco de dados
# Requer psql instalado e acessível no PATH

param(
    [string]$ConnectionString = "Host=161.97.141.186;Port=5432;Database=GCCuidadoDEV;Username=postgres;Password=Crazy#57LB;Include Error Detail=true"
)

Write-Host "=== Aplicando Schema do Banco de Dados ===" -ForegroundColor Cyan
Write-Host ""

# Extrair informações da connection string
if ($ConnectionString -match "Host=([^;]+)") { $Host = $Matches[1] }
if ($ConnectionString -match "Port=([^;]+)") { $Port = $Matches[1] }
if ($ConnectionString -match "Database=([^;]+)") { $Database = $Matches[1] }
if ($ConnectionString -match "Username=([^;]+)") { $Username = $Matches[1] }
if ($ConnectionString -match "Password=([^;]+)") { $Password = $Matches[1] }

Write-Host "Host: $Host" -ForegroundColor Yellow
Write-Host "Port: $Port" -ForegroundColor Yellow
Write-Host "Database: $Database" -ForegroundColor Yellow
Write-Host "Username: $Username" -ForegroundColor Yellow
Write-Host ""

# Verificar se psql está disponível
$psqlPath = Get-Command psql -ErrorAction SilentlyContinue

if (-not $psqlPath) {
    Write-Host "ERRO: psql não encontrado no PATH!" -ForegroundColor Red
    Write-Host "Instale o PostgreSQL Client ou adicione ao PATH." -ForegroundColor Red
    Write-Host ""
    Write-Host "Alternativa: Execute o SQL manualmente usando pgAdmin ou outro cliente." -ForegroundColor Yellow
    Write-Host "Arquivo SQL: PortalSaudeConectada/Scripts/01-create-usuarios-table.sql" -ForegroundColor Yellow
    exit 1
}

Write-Host "✓ psql encontrado: $($psqlPath.Source)" -ForegroundColor Green
Write-Host ""

# Definir variável de ambiente para senha
$env:PGPASSWORD = $Password

try {
    Write-Host "Executando script SQL..." -ForegroundColor Cyan
    
    $scriptPath = Join-Path $PSScriptRoot "01-create-usuarios-table.sql"
    
    if (-not (Test-Path $scriptPath)) {
        Write-Host "ERRO: Arquivo SQL não encontrado: $scriptPath" -ForegroundColor Red
        exit 1
    }
    
    # Executar o script SQL
    psql -h $Host -p $Port -U $Username -d $Database -f $scriptPath
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host ""
        Write-Host "✓ Schema aplicado com sucesso!" -ForegroundColor Green
        Write-Host ""
        Write-Host "Próximo passo: Execute o endpoint /api/admin/seed-database para popular o banco" -ForegroundColor Yellow
    } else {
        Write-Host ""
        Write-Host "ERRO: Falha ao executar o script SQL" -ForegroundColor Red
        exit 1
    }
}
finally {
    # Limpar variável de ambiente
    Remove-Item Env:\PGPASSWORD -ErrorAction SilentlyContinue
}

