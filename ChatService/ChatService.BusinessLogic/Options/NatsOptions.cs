namespace NatsExtensions.Options
{
    /// <summary>
    ///     NATS settings options
    /// </summary>
    public class NatsOptions
    {
        /// <summary>
        ///     Configuration section name
        /// </summary>
        public const string Section = "Nats";
        
        /// <summary>
        ///     Subject name
        /// </summary>
        public string Subject { get; set; }
        
        /// <summary>
        ///     Connection string to NATS
        /// </summary>
        public string ConnectionString { get; set; }
    }
}