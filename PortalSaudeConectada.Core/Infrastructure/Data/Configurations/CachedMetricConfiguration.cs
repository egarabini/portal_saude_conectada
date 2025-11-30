using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalSaudeConectada.Core.Domain.Entities;

namespace PortalSaudeConectada.Core.Infrastructure.Data.Configurations;

public class CachedMetricConfiguration : IEntityTypeConfiguration<CachedMetric>
{
    public void Configure(EntityTypeBuilder<CachedMetric> builder)
    {
        builder.ToTable("cached_metrics");
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.Project).HasColumnName("project").HasMaxLength(100).IsRequired();
        builder.Property(e => e.MetricName).HasColumnName("metric_name").HasMaxLength(200).IsRequired();
        builder.Property(e => e.MetricValue).HasColumnName("metric_value").HasColumnType("jsonb").IsRequired();
        builder.Property(e => e.ExpiresAt).HasColumnName("expires_at");
        
        builder.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(e => e.CreatedBy).HasColumnName("created_by").HasMaxLength(100);
        builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        builder.Property(e => e.UpdatedBy).HasColumnName("updated_by").HasMaxLength(100);
        builder.Property(e => e.IsDeleted).HasColumnName("is_deleted").IsRequired();
        builder.Property(e => e.DeletedAt).HasColumnName("deleted_at");
        builder.Property(e => e.DeletedBy).HasColumnName("deleted_by").HasMaxLength(100);
        
        builder.HasIndex(e => new { e.Project, e.MetricName, e.CreatedAt });
        builder.HasIndex(e => e.ExpiresAt);
        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}

