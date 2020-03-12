using Common.DataAccess.EFCore.Configurations.System;
using Common.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.DataAccess.EFCore.Configurations
{
    public class GenreConfig : BaseEntityConfig<Genre>
    {
        public GenreConfig() : base("Genres") { }

        public override void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.ExternalId)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(x => x.ExternalId)
                .IsUnique();

            builder.Property(x => x.Type)
                .HasMaxLength(5)
                .IsRequired();

            base.Configure(builder);
        }
    }
}