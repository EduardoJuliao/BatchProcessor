using BatchProcessor.ManagerApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BatchProcessor.ManagerApi.Repository.Mappings
{
    public class NumberMapping : IEntityTypeConfiguration<Number>
    {
        public void Configure(EntityTypeBuilder<Number> builder)
        {
            builder.ToTable("Numbers");

            builder.HasKey(x => x.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Batch)
                .WithMany(x => x.Numbers)
                .HasForeignKey(x => x.BatchId);

            builder.Property(x => x.Order)
                .IsRequired();

            builder.Property(x => x.Value)
                .IsRequired();

            builder.Property(x => x.Multiplier)
                .IsRequired();

            builder.Property(x => x.MultipliedValue)
                .IsRequired();
        }
    }
}
