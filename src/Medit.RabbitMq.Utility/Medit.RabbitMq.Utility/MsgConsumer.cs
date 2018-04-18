using Medit.RabbitMq.Utility.Abstractions;
using Medit.RabbitMq.Utility.Configurations;
using Medit.RabbitMq.Utility.Enums;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Medit.RabbitMq.Utility
{
    public class MsgConsumer : MsgHandler
    {
        public MsgConsumer()
            : base()
        { }

        public void Consume(Action<string> consumeAction)
        {
            Consume(consumeAction, RabbitMqParameters.QueueName);
        }

        public void Consume(Action<string> consumeAction,string queue)
        {
            try
            {
                using (var channel = _conn.CreateModel())
                {
                    channel.QueueDeclare(queue, RabbitMqParameters.IsDurable, false, true, null);
                    //如果接受一个消息但是没有应答，客户端不会接受下一个消息
                    channel.BasicQos(0, 1, false);
                    //定义一个消费者在队列上
                    var consumer2 = new EventingBasicConsumer(channel);
                    //消费队列  设置应答模式为程序主动应答

                    while (true)
                    {
                        Thread.Sleep(100);
                        consumer2.Received += (sender, ea) =>
                        {
                            MsgConsumeResult consumeResult = MsgConsumeResult.Retry;
                            ulong delivertTag = 0;

                            delivertTag = ea.DeliveryTag;
                            byte[] data = ea.Body;
                            string msg = Encoding.UTF8.GetString(data);
                            try
                            {
                                consumeAction(msg);
                                consumeResult = MsgConsumeResult.Accept;
                            }
                            catch (Exception)
                            {
                                consumeResult = MsgConsumeResult.Reject;
                                // 记录日志
                                // TODO...
                            }
                            finally
                            {
                                switch (consumeResult)
                                {
                                    case MsgConsumeResult.Accept:
                                        //回复确认处理成功
                                        channel.BasicAck(delivertTag, false);
                                        break;
                                    case MsgConsumeResult.Reject:
                                        //严重错误  写日志
                                        channel.BasicNack(delivertTag, false, false);
                                        break;
                                }
                            }
                        };
                        channel.BasicConsume(queue, false, consumer2);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Consume(Func<string, bool> consumeFunc)
        {
            Consume(consumeFunc, RabbitMqParameters.QueueName);
        }

        public void Consume(Func<string, bool> consumeFunc,string queue)
        {
            try
            {
                using (var channel = _conn.CreateModel())
                {
                    channel.QueueDeclare(queue, RabbitMqParameters.IsDurable, false, true, null);
                    //如果接受一个消息但是没有应答，客户端不会接受下一个消息
                    channel.BasicQos(0, 1, false);
                    //定义一个消费者在队列上
                    var consumer = new EventingBasicConsumer(channel);
                    //消费队列  设置应答模式为程序主动应答

                    while (true)
                    {
                        Thread.Sleep(100);
                        int tryNum = 0;
                        consumer.Received += (sender, ea) =>
                        {
                            MsgConsumeResult consumeResult = MsgConsumeResult.Retry;
                            ulong delivertTag = 0;

                            delivertTag = ea.DeliveryTag;
                            byte[] data = ea.Body;
                            string msg = Encoding.UTF8.GetString(data);
                            try
                            {
                                if (consumeFunc(msg))
                                    consumeResult = MsgConsumeResult.Accept;
                                else
                                    consumeResult = MsgConsumeResult.Retry;
                            }
                            catch (Exception)
                            {
                                consumeResult = MsgConsumeResult.Reject;
                                // 记录日志
                                // TODO...
                            }
                            finally
                            {
                                switch (consumeResult)
                                {
                                    case MsgConsumeResult.Accept:
                                        //回复确认处理成功
                                        channel.BasicAck(delivertTag, false);
                                        break;
                                    case MsgConsumeResult.Reject:
                                        //从消息队列中抛弃
                                        channel.BasicNack(delivertTag, false, false);
                                        break;
                                    case MsgConsumeResult.Retry:
                                        tryNum += 1;
                                        if (tryNum < 5)
                                        {
                                            channel.BasicNack(delivertTag, false, true);//重试次数小于等于5，继续重试
                                        }
                                        else
                                        {
                                            channel.BasicNack(delivertTag, false, false);//重试次数大于5，抛弃并记录日志
                                            tryNum = 0;
                                            // 记录日志
                                            // TODO...
                                        }
                                        break;
                                }
                            }
                        };
                        channel.BasicConsume(queue, false, consumer);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
