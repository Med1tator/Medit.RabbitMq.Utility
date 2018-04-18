using Medit.RabbitMq.Utility.Configurations;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medit.RabbitMq.Utility.Buliders
{
    internal class ConnectionBuilder
    {

        public IConnection Build()
        {
            try
            {
                return new ConnectionFactory()
                {
                    HostName = RabbitMqParameters.Host,
                    UserName = RabbitMqParameters.UserName,
                    Password = RabbitMqParameters.Password,
                    VirtualHost = RabbitMqParameters.VirtualHost
                }.CreateConnection();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
