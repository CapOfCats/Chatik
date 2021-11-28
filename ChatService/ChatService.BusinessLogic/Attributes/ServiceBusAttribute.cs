using System;

namespace NatsExtensions.Attributes
{
    /// <summary>
    ///     Attribute that contains target code
    ///     for nats request-reply interaction
    /// </summary>
    public class ServiceBusAttribute : Attribute
    {
        /// <summary>
        ///     Target code for request reply interaction
        /// </summary>
        public int Code { get; set; }
    }
}