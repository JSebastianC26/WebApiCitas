using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCitas.Interfaces
{
    /// <summary>
    /// Interfaz del Message Bus
    /// </summary>
    public interface IMessageBus
    {
        Task PublishAsync<T>(T message) where T : class;
    }
}
