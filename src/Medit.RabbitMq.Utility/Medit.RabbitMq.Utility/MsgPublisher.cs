using Medit.RabbitMq.Utility.Abstractions;
using Medit.RabbitMq.Utility.Buliders;
using Medit.RabbitMq.Utility.Configurations;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medit.RabbitMq.Utility
{
    public class MsgPublisher : MsgHandler
    {
        public MsgPublisher()
            : base()
        { }

        public bool Publish(string msg)
        {
            return Publish(msg, RabbitMqParameters.QueueName);
        }

        public bool Publish(string msg, string queue)
        {
            if (string.IsNullOrEmpty(msg))
                return false;

            try
            {
                using (var channel = _conn.CreateModel())
                {
                    channel.QueueDeclare(queue, RabbitMqParameters.IsDurable, false, true, null);

                    IBasicProperties properties = channel.CreateBasicProperties();
                    properties.DeliveryMode = Convert.ToByte(2); //支持持久化数据

                    byte[] data = Encoding.UTF8.GetBytes(msg);
                    channel.BasicPublish("", queue, properties, data);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
