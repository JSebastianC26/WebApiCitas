using System;
using System.Threading.Tasks;
using WebApiCitas.Interfaces;

namespace WebApiCitas.Services
{
    /// <summary>
    /// Message Bus simulado para desarrollo sin RabbitMQ
    /// Solo registra los eventos en consola
    /// </summary>
    public class MockMessageBus : IMessageBus
    {
        public Task PublishAsync<T>(T message) where T : class
        {
            return Task.Run(() =>
            {
                var typeName = typeof(T).Name;
                Console.WriteLine($"[MockMessageBus] Evento publicado: {typeName}");
                System.Diagnostics.Debug.WriteLine($"[MockMessageBus] {typeName} - {Newtonsoft.Json.JsonConvert.SerializeObject(message)}");
            });
        }
    }
}