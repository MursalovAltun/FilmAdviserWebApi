using Common.DataAccess.EFCore.Configurations.System;
using Common.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.DataAccess.EFCore.Configurations
{
    public class SettingsConfig : BaseEntityConfig<Settings>
    {
        public SettingsConfig() : base("Settings")
        { }

        public override void Configure(EntityTypeBuilder<Settings> builder)
        {
            base.Configure(builder);

            builder.Property(obj => obj.ThemeName).IsRequired();
        }
    }
}