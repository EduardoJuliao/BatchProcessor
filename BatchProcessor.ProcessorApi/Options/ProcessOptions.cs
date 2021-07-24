namespace BatchProcessor.ProcessorApi.Options
{
    public class ProcessOptions
    {
        /// <summary>
        /// Minimum time to be awaited
        /// </summary>
        public int MinTimeInSeconds { get; set; }

        /// <summary>
        /// Maximum time to be awaited
        /// </summary>
        public int MaxTimeInSeconds { get; set; }

        /// <summary>
        /// Include max time
        /// </summary>
        public bool Inclusive { get; set; }
    }
}
