using BatchProcessor.ManagerApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BatchProcessor.ManagerApi.Interfaces.Repository
{
    public interface IApplicationContext
    {
        object Lock { get; }

        DbSet<Process> Processes { get; set; }
        DbSet<Batch> Batches { get; set; }
        DbSet<Number> Numbers { get; set; }
    }
}
