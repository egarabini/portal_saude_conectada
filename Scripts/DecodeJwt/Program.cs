using System.IdentityModel.Tokens.Jwt;

var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI5NGU0MWVlMy0zNDI2LTRlZDEtOTYyYi02MjRiZmQxMmY1MWEiLCJlbWFpbCI6ImFkbWluQHNhdWRlY29uZWN0YWRhLmNvbS5iciIsImp0aSI6IjhmYzJjYzM2LTU1YTgtNDk0NC04ZmE0LWZjNmI1ZDRkYmE3ZCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJBZG1pbmlzdHJhZG9yIGRvIFNpc3RlbWEiLCJ1c2VybmFtZSI6ImFkbWluIiwidGlwbyI6IkFkbWluaXN0cmFkb3IiLCJleHAiOjE3NjQ0NzE0MjQsImlzcyI6IlBvcnRhbFNhdWRlQ29uZWN0YWRhLkFwaSIsImF1ZCI6IlBvcnRhbFNhdWRlQ29uZWN0YWRhLldlYiJ9._4eMUlNq6_Z5uathvQSDEaE2eTHg2MSJ-A-Abrld0aY";

var handler = new JwtSecurityTokenHandler();
var jwtToken = handler.ReadJwtToken(token);

Console.WriteLine("=== JWT Token Claims ===");
Console.WriteLine();

foreach (var claim in jwtToken.Claims)
{
    Console.WriteLine($"{claim.Type}: {claim.Value}");
}

Console.WriteLine();
Console.WriteLine("=== Claim 'tipo' ===");
var tipoClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "tipo");
if (tipoClaim != null)
{
    Console.WriteLine($"✓ Valor: {tipoClaim.Value}");
}
else
{
    Console.WriteLine("✗ Claim 'tipo' não encontrado!");
}

