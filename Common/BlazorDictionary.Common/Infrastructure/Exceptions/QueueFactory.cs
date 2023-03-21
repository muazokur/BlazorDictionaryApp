﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorDictionary.Common.Infrastructure.Exceptions
{
    public static class QueueFactory
    {
        public static void SendMessageToExchange(string exchangeName,string exchangeType, string queueName, object obj)
        {
            var channel = CreateBasicConsumer().EnsureExchange(exchangeName,exchangeType).EnsureQueue(queueName,exchangeName).Model;

            var body=Encoding.UTF8.GetBytes(JsonSerializer.Serialize(obj));

            channel.BasicPublish(exchange:exchangeName,routingKey:queueName,basicProperties:null,body:body);
        }

        public static EventingBasicConsumer CreateBasicConsumer()
        {
            var factory=new ConnectionFactory() { HostName= DictionaryConstants.RabbitMQHost};
            var connection=factory.CreateConnection();
            var channel = connection.CreateModel();

            return new EventingBasicConsumer(channel); 
        }

        public static EventingBasicConsumer EnsureExchange(this EventingBasicConsumer consumer, string exchangeName, 
            string exchangeType=DictionaryConstants.DefaultExchangeType)
        {
            consumer.Model.ExchangeDeclare(exchange: exchangeName, type: exchangeType,durable:false,autoDelete:false);

            return consumer;
        }

        public static EventingBasicConsumer EnsureQueue(this EventingBasicConsumer consumer, string queueName,
          string exchangeName)
        {
            consumer.Model.QueueDeclare(queue: queueName, durable:false,autoDelete:false, exclusive:false);

            consumer.Model.QueueBind(queueName, exchangeName,queueName);

            return consumer;
        }
    }
}
