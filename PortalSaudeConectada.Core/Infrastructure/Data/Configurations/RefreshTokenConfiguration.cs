using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalSaudeConectada.Core.Domain.Entities;

namespace PortalSaudeConectada.Core.Infrastructure.Data.Configurations;

/// <summary>
/// Configuração do EF Core para a entidade RefreshToken
/// </summary>
public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("refresh_tokens", "saude_conectada");

        builder.HasKey(rt => rt.Id);
        builder.Property(rt => rt.Id).HasColumnName("id");

        builder.Property(rt => rt.Token)
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnName("token");

        builder.HasIndex(rt => rt.Token)
            .IsUnique()
            .HasDatabaseName("ix_refresh_tokens_token");

        builder.Property(rt => rt.UsuarioId)
            .IsRequired()
            .HasColumnName("usuario_id");

        builder.Property(rt => rt.ExpiresAt)
            .IsRequired()
            .HasColumnName("expires_at");

        builder.Property(rt => rt.IsRevoked)
            .IsRequired()
            .HasDefaultValue(false)
            .HasColumnName("is_revoked");

        builder.Property(rt => rt.RevokedAt)
            .HasColumnName("revoked_at");

        builder.Property(rt => rt.IpAddress)
            .HasMaxLength(45) // IPv6 max length
            .HasColumnName("ip_address");

        builder.Property(rt => rt.UserAgent)
            .HasMaxLength(500)
            .HasColumnName("user_agent");

        // Relacionamento com Usuario
        builder.HasOne(rt => rt.Usuario)
            .WithMany()
            .HasForeignKey(rt => rt.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        // Índices para performance
        builder.HasIndex(rt => rt.UsuarioId)
            .HasDatabaseName("ix_refresh_tokens_usuario_id");

        builder.HasIndex(rt => rt.ExpiresAt)
            .HasDatabaseName("ix_refresh_tokens_expires_at");

        builder.HasIndex(rt => rt.IsRevoked)
            .HasDatabaseName("ix_refresh_tokens_is_revoked");

        // BaseEntity properties
        builder.Property(rt => rt.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(rt => rt.UpdatedAt).HasColumnName("updated_at");
        builder.Property(rt => rt.DeletedAt).HasColumnName("deleted_at");
        builder.Property(rt => rt.IsDeleted).HasColumnName("is_deleted").IsRequired().HasDefaultValue(false);
        builder.Property(rt => rt.CreatedBy).HasColumnName("created_by");
        builder.Property(rt => rt.UpdatedBy).HasColumnName("updated_by");
        builder.Property(rt => rt.DeletedBy).HasColumnName("deleted_by");

        builder.HasQueryFilter(rt => !rt.IsDeleted);
    }
}

