using Newtonsoft.Json;

namespace GranTitan.Model
{
    /// <summary>
    /// Clase de la respuesta
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Response<T>
    {
        /// <summary>
        /// Cuerpo de la respuesta
        /// </summary>
        [JsonProperty("body")]
        public T Body { get; set; }
    }

}
