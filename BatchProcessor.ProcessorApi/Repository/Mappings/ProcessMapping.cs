using BatchProcessor.ProcessorApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BatchProcessor.ProcessorApi.Repository.Mappings
{
    public class ProcessMapping : IEntityTypeConfiguration<Process>
    {
        public void Configure(EntityTypeBuilder<Process> builder)
        {
            builder.ToTable("Processes");

            builder.HasKey(x => x.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.HasMany(x => x.Batches)
                .WithOne(x => x.Process)
                .HasForeignKey(x => x.ProcessId);

            builder.Property(x => x.StartedAt)
                .IsRequired();

            builder.Property(x => x.FinishedAt)
                .IsRequired(false);

            builder.Property(x => x.IsFinished)
                .IsRequired();
        }
    }
}
