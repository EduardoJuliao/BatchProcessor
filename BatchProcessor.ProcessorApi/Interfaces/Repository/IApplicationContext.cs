using BatchProcessor.ProcessorApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BatchProcessor.ProcessorApi.Interfaces.Repository
{
    public interface IApplicationContext
    {
        DbSet<Process> Processes { get; set; }
        DbSet<Batch> Batches { get; set; }
        DbSet<Number> Numbers { get; set; }
    }
}
