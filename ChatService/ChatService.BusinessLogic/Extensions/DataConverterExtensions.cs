using System;
using System.Text.Json;

namespace NatsExtensions.Extensions
{
    /// <summary>
    ///     Data converter for transformation data
    ///     model -> byte[]
    ///     byte -> model
    /// </summary>
    public static class DataConverterExtensions
    {
        /// <summary>
        ///     Convert object to byte array
        /// </summary>
        /// <param name="data">Data for transforming</param>
        /// <typeparam name="T">Target data type</typeparam>
        /// <returns>Byte array</returns>
        public static byte[] ConvertToByteArray<T>(this T data) where T : class
            => data != null
                ? JsonSerializer.SerializeToUtf8Bytes(data)
                : default;

        /// <summary>
        ///     Convert byte array to target object
        /// </summary>
        /// <param name="data">Data in byte array format</param>
        /// <typeparam name="T">Target data type</typeparam>
        /// <returns>Targer object data</returns>
        public static T ConvertFromByteArray<T>(this byte[] data) where T : class
            => data != null
                ? JsonSerializer.Deserialize<T>(new ReadOnlySpan<byte>(data))
                : default;
    }
}