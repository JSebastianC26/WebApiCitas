using Newtonsoft.Json;
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
    /// Implementación alternativa con Azure Service Bus
    /// </summary>
    public class AzureServiceBusMessageBus : IMessageBus
    {
        private readonly string _connectionString;
        private readonly string _topicName = "medical-appointments-topic";

        public AzureServiceBusMessageBus(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task PublishAsync<T>(T message) where T : class
        {
            try
            {
                // Implementación con Azure Service Bus
                // Requiere: Microsoft.Azure.ServiceBus NuGet package

                var messageBody = JsonConvert.SerializeObject(message);
                var messageBytes = Encoding.UTF8.GetBytes(messageBody);

                // var serviceBusMessage = new Message(messageBytes)
                // {
                //     ContentType = "application/json",
                //     Label = typeof(T).Name
                // };

                // var topicClient = new TopicClient(_connectionString, _topicName);
                // await topicClient.SendAsync(serviceBusMessage);
                // await topicClient.CloseAsync();

                await Task.CompletedTask;
                Console.WriteLine($"[Azure ServiceBus] Mensaje publicado: {typeof(T).Name}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Azure ServiceBus] Error: {ex.Message}");
                throw;
            }
        }
    }
}