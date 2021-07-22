using BatchProcessor.ProcessorApi.Entities;
using BatchProcessor.ProcessorApi.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace BatchProcessor.ProcessorApi.Repository
{
    public class ApplicationContext : DbContext, IApplicationContext
    {
        public ApplicationContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        public DbSet<Process> Processes { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<Number> Numbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        }
    }
}
