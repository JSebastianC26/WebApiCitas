using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebApiCitas.Interfaces;

namespace WebApiCitas.Services
{
    /// <summary>
    /// Implementación de Message Bus usando RabbitMQ
    /// Permite publicar eventos que serán consumidos por otros servicios
    /// </summary>
    public class RabbitMQMessageBus : IMessageBus, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _exchangeName = "medical_appointments_exchange";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostName"></param>
        /// <exception cref="Exception"></exception>
        public RabbitMQMessageBus(string hostName = "localhost")
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = hostName,
                    UserName = "guest",
                    Password = "guest",
                    VirtualHost = "/",
                    Port = 44385
                };

                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                // Declarar el exchange como tipo 'topic' para routing flexible
                _channel.ExchangeDeclare(
                    exchange: _exchangeName,
                    type: ExchangeType.Topic,
                    durable: true,
                    autoDelete: false);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al conectar con RabbitMQ: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Publica un mensaje de forma asíncrona en el bus
        /// </summary>
        public Task PublishAsync<T>(T message) where T : class
        {
            return Task.Run(() =>
            {
                try
                {
                    var routingKey = GetRoutingKey<T>();
                    var messageBody = JsonConvert.SerializeObject(message);
                    var body = Encoding.UTF8.GetBytes(messageBody);

                    var properties = _channel.CreateBasicProperties();
                    properties.Persistent = true; // Mensaje persistente
                    properties.ContentType = "application/json";
                    properties.Timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

                    _channel.BasicPublish(
                        exchange: _exchangeName,
                        routingKey: routingKey,
                        basicProperties: properties,
                        body: body);

                    Console.WriteLine($"[MessageBus] Publicado: {routingKey}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[MessageBus] Error al publicar: {ex.Message}");
                    throw;
                }
            });
        }

        /// <summary>
        /// Obtiene la clave de routing basada en el tipo de mensaje
        /// </summary>
        private string GetRoutingKey<T>()
        {
            var typeName = typeof(T).Name;

            // Conversión de PascalCase a snake_case
            var routingKey = string.Concat(
                typeName.Select((x, i) => i > 0 && char.IsUpper(x)
                    ? "_" + x.ToString()
                    : x.ToString())).ToLower();

            return routingKey;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }
}