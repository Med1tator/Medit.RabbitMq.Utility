using Medit.RabbitMq.Utility.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medit.RabbitMq.Utility.Test.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            MsgConsumer consumer = new MsgConsumer();
            consumer.Consume((msg) =>
            {
                Console.WriteLine("接收到消息：" + msg);
                return msg == "2018";
            });

            // 如果监听Second消息队列 需要注释上行
            //consumer.Consume((msg) =>
            //{
            //    Console.WriteLine("接收到消息2：" + msg);
            //    return msg == "2018";
            //}, "Second");
        }
    }
}
