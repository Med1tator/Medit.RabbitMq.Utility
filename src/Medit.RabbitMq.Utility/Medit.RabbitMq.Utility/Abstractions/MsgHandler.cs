using Medit.RabbitMq.Utility.Buliders;
using Medit.RabbitMq.Utility.Configurations;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Medit.RabbitMq.Utility.Abstractions
{
    public abstract class MsgHandler
    {
        protected static IConnection _conn;

        public MsgHandler()
        {
            _conn = new ConnectionBuilder().Build();
        }
    }
}