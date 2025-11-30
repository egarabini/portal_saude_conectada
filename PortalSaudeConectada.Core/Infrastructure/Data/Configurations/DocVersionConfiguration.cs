using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalSaudeConectada.Core.Domain.Entities;

namespace PortalSaudeConectada.Core.Infrastructure.Data.Configurations;

public class DocVersionConfiguration : IEntityTypeConfiguration<DocVersion>
{
    public void Configure(EntityTypeBuilder<DocVersion> builder)
    {
        builder.ToTable("doc_versions");
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.Project).HasColumnName("project").HasMaxLength(100).IsRequired();
        builder.Property(e => e.FilePath).HasColumnName("file_path").HasMaxLength(500).IsRequired();
        builder.Property(e => e.Version).HasColumnName("version").HasMaxLength(50).IsRequired();
        builder.Property(e => e.Content).HasColumnName("content").IsRequired();
        builder.Property(e => e.Author).HasColumnName("author").HasMaxLength(100);
        builder.Property(e => e.CommitHash).HasColumnName("commit_hash").HasMaxLength(40);
        
        builder.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(e => e.CreatedBy).HasColumnName("created_by").HasMaxLength(100);
        builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        builder.Property(e => e.UpdatedBy).HasColumnName("updated_by").HasMaxLength(100);
        builder.Property(e => e.IsDeleted).HasColumnName("is_deleted").IsRequired();
        builder.Property(e => e.DeletedAt).HasColumnName("deleted_at");
        builder.Property(e => e.DeletedBy).HasColumnName("deleted_by").HasMaxLength(100);
        
        builder.HasIndex(e => new { e.Project, e.FilePath, e.Version }).IsUnique();
        builder.HasIndex(e => e.CreatedAt);
        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}

