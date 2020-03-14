using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.DataAccess.EFCore.Configurations.System
{
    public abstract class BaseEntityConfig<TType> : IEntityTypeConfiguration<TType>
        where TType : BaseEntity
    {
        protected string TableName { get; set; }

        protected BaseEntityConfig(string tableName)
        {
            this.TableName = tableName;
        }

        public virtual void Configure(EntityTypeBuilder<TType> builder)
        {
            builder.ToTable(this.TableName);
            builder.HasKey(obj => obj.Id);

            builder.Property(obj => obj.CreationDate)
                .HasColumnType("timestamp without time zone")
                .HasDefaultValueSql("now() at time zone 'utc'")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.ModifyDate)
                .HasColumnType("timestamp without time zone")
                .HasDefaultValueSql("now() at time zone 'utc'")
                .ValueGeneratedOnAddOrUpdate();
        }
    }
}