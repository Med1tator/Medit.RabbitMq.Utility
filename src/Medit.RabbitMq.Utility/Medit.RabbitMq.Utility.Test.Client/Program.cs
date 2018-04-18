using Medit.RabbitMq.Utility.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medit.RabbitMq.Utility.Test.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            MsgPublisher publisher = new MsgPublisher();
            while (true)
            {
                Console.WriteLine("请输入要发布的消息：");
                string msg = Console.ReadLine();
                publisher.Publish(msg);

                publisher.Publish(msg, "Second");
                Console.WriteLine("消息发布成！");
            }

            
        }
    }
}
