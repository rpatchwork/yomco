using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Azure.Messaging.ServiceBus;
using  System.Threading.Tasks;
using System.Text.Json.Nodes;

namespace topic.Function
{
    public class topPrototype
    {

        [FunctionName("topPrototype")]
        //public static void Run([ServiceBusTrigger("ttop1", "tTop1Sub1", Connection = "cupcakes_SERVICEBUS")]ServiceBusReceivedMessage mySbMsg, ILogger log)
        public static void Run([ServiceBusTrigger("ttop1", "tTop1Sub1", Connection = "cupcakes_SERVICEBUS")]
        string mySbMsg,
        Int32 deliveryCount,
        DateTime enqueuedTimeUtc,
        string messageId,
        //ApplicationProperties custProp,
        ILogger log)
        {
            //string msgBody = mySbMsg.Body.ToString();
            //log.LogInformation($"{Environment.NewLine}***{Environment.NewLine} Generic Operator for message : {mySbMsg.MessageId}");
            //log.LogInformation($"C# ServiceBus topic trigger function processed message: {msgBody}");
            
            
            log.LogInformation($"{Environment.NewLine}***{Environment.NewLine} Generic Operator for message : {messageId}");
            log.LogInformation($"{Environment.NewLine}EnqueuedTimeUtc={enqueuedTimeUtc}");
            log.LogInformation($"{Environment.NewLine}DeliveryCount={deliveryCount}");
            log.LogInformation($"{Environment.NewLine}C# ServiceBus topic trigger function processed message: {mySbMsg}");
        }
    }
}
