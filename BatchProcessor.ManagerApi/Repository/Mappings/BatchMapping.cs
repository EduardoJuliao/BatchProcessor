using BatchProcessor.ManagerApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BatchProcessor.ManagerApi.Repository.Mappings
{
    public class BatchMapping : IEntityTypeConfiguration<Batch>
    {
        public void Configure(EntityTypeBuilder<Batch> builder)
        {
            builder.ToTable("Batches");

            builder.HasKey(x => x.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Process)
                .WithMany(x => x.Batches)
                .HasForeignKey(x => x.ProcessId);

            builder.HasMany(x => x.Numbers)
                .WithOne(x => x.Batch)
                .HasForeignKey(x => x.BatchId)
                .HasPrincipalKey(x => x.Id);

            builder.Property(x => x.Order)
                .IsRequired();

            builder.Property(x => x.Size)
                .IsRequired();
        }
    }
}
