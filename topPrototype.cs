using System;
using Microsoft.Azure.WebJobs;
//using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Azure.Messaging.ServiceBus;
using  System.Threading.Tasks;
using System.Text.Json.Nodes;

namespace topic.Function
{
    public class topPrototype
    {
        
        [FunctionName("topPrototype")]
         public static void Run(
            [ServiceBusTrigger(
                "ttop1", 
                "tTop1Sub1", 
                Connection = "cupcakes_SERVICEBUS")]
                ServiceBusReceivedMessage  messageIncoming,
                ILogger log)
        {
            log.LogInformation($"{Environment.NewLine}***{Environment.NewLine}Generic Operator for message : {messageIncoming.MessageId}");
            string custProp = string.Join(Environment.NewLine,messageIncoming.ApplicationProperties);
            var frostyVar = messageIncoming.ApplicationProperties["frosted"];
            //log.LogInformation($"{Environment.NewLine}Custom Message Properties : {messageIncoming.ApplicationProperties.ToString()}");
            log.LogInformation($"{Environment.NewLine}C# ServiceBus topic trigger function processed message: {messageIncoming.Body}");
            log.LogInformation($"{Environment.NewLine}Frosty Status: {frostyVar}");
            log.LogInformation($"{Environment.NewLine}Message Properties: {custProp}");
        }
    }
}
