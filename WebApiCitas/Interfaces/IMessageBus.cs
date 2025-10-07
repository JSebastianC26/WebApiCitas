using System.Threading.Tasks;

namespace WebApiCitas.Interfaces
{
    /// <summary>
    /// Interfaz del Message Bus
    /// </summary>
    public interface IMessageBus
    {
        /// <summary>
        /// Publicar un mensaje asincronamente.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        Task PublishAsync<T>(T message) where T : class;
    }
}
