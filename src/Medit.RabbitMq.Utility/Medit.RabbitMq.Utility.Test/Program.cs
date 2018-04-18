using Medit.RabbitMq.Utility.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medit.RabbitMq.Utility.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            RabbitMqConfiguration configuration = new RabbitMqConfiguration();
            MsgPublisher publisher = new MsgPublisher(configuration);

            publisher.Publish("Hello...");
        }
        
    }
    class Msg {
        
    }
}
