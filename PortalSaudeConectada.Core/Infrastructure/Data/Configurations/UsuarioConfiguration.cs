using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalSaudeConectada.Core.Domain.Entities;

namespace PortalSaudeConectada.Core.Infrastructure.Data.Configurations;

/// <summary>
/// Configuração do EF Core para a entidade Usuario
/// </summary>
public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuarios");

        // Chave primária
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnName("id");

        // Propriedades
        builder.Property(u => u.Nome)
            .HasColumnName("nome")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(u => u.Email)
            .HasColumnName("email")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(u => u.Username)
            .HasColumnName("username")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.PasswordHash)
            .HasColumnName("password_hash")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(u => u.Cpf)
            .HasColumnName("cpf")
            .HasMaxLength(11);

        builder.Property(u => u.Telefone)
            .HasColumnName("telefone")
            .HasMaxLength(20);

        builder.Property(u => u.Tipo)
            .HasColumnName("tipo")
            .IsRequired();

        builder.Property(u => u.Status)
            .HasColumnName("status")
            .IsRequired();

        builder.Property(u => u.UltimoLogin)
            .HasColumnName("ultimo_login");

        builder.Property(u => u.EmailVerificado)
            .HasColumnName("email_verificado")
            .IsRequired();

        builder.Property(u => u.EmailVerificationToken)
            .HasColumnName("email_verification_token")
            .HasMaxLength(500);

        builder.Property(u => u.PasswordResetToken)
            .HasColumnName("password_reset_token")
            .HasMaxLength(500);

        builder.Property(u => u.PasswordResetTokenExpires)
            .HasColumnName("password_reset_token_expires");

        // Campos de auditoria (BaseEntity)
        builder.Property(u => u.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(u => u.CreatedBy)
            .HasColumnName("created_by")
            .HasMaxLength(100);

        builder.Property(u => u.UpdatedAt)
            .HasColumnName("updated_at");

        builder.Property(u => u.UpdatedBy)
            .HasColumnName("updated_by")
            .HasMaxLength(100);

        builder.Property(u => u.IsDeleted)
            .HasColumnName("is_deleted")
            .IsRequired();

        builder.Property(u => u.DeletedAt)
            .HasColumnName("deleted_at");

        builder.Property(u => u.DeletedBy)
            .HasColumnName("deleted_by")
            .HasMaxLength(100);

        // Índices
        builder.HasIndex(u => u.Email).IsUnique();
        builder.HasIndex(u => u.Username).IsUnique();
        builder.HasIndex(u => u.Cpf);
        builder.HasIndex(u => u.Status);
        builder.HasIndex(u => u.IsDeleted);

        // Query Filter (Global) - Ignora registros deletados
        builder.HasQueryFilter(u => !u.IsDeleted);
    }
}

