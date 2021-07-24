namespace BatchProcessor.ProcessorApi.Options
{
    public class NumberGeneratorOptions
    {
        /// <summary>
        /// Minimum number value to be generated
        /// </summary>
        public int MinValue { get; set; }

        /// <summary>
        /// Maximum number value to be generated
        /// </summary>
        public int MaxValue { get; set; }

        /// <summary>
        /// Include Max Value
        /// </summary>
        public bool Inclusive { get; set; }
    }
}
