using Microsoft.EntityFrameworkCore;
using PropertyService.Repository.Entities;
using PropertyService.Shared.enums;

namespace PropertyService.Data.Context
{
    public class PropertyDbContext : DbContext
    {
        public DbSet<Property> properties { get; set; }
        public DbSet<PropertyStatusHistory> propertiesStatusHistory { get; set; }

        public PropertyDbContext(DbContextOptions<PropertyDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Property>()
                    .Property(p => p.Price)
                    .HasColumnType("decimal(18,2)");
            builder.Entity<Property>(entity =>
            {
                entity.Property(p => p.Status)
                    .HasConversion<string>()
                    .IsRequired()
                    .HasDefaultValue(PropertyStatus.Available);

                entity.ToTable(t => t.HasCheckConstraint(
                    name: "CK_Property_Status",
                    sql: "Status IN ('Available','UnderOffer','Sold')"
                ));
            });

            builder.Entity<PropertyStatusHistory>(entity =>
            {
                entity.Property(p => p.NewStatus)
                    .HasConversion<string>()
                    .IsRequired();

                entity.Property(p => p.OldStatus)
                    .HasConversion<string>()
                    .IsRequired();

                entity.Property(h => h.ChangedAt)
                      .HasDefaultValueSql("GETUTCDATE()");

                entity.ToTable(t => t.HasCheckConstraint(
                    name: "CK_PropertyHistory_Status",
                    sql: "NewStatus IN ('Available','UnderOffer','Sold') AND OldStatus IN ('Available','UnderOffer','Sold')"
                ));
            });
        }
    }     
}
