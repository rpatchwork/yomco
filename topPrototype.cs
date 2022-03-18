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
           
            string custProp = string.Join(Environment.NewLine,messageIncoming.ApplicationProperties);
            var frostyVar = messageIncoming.ApplicationProperties["frosted"];
            JsonNode messageJson = JsonNode.Parse(messageIncoming.Body.ToString());
            decimal frostingAmount = Convert.ToDecimal(messageJson["orderSpecs"]["frostingAmount"]);

            log.LogInformation($"{Environment.NewLine}***{Environment.NewLine}Generic Operator for message : {messageIncoming.MessageId}");
            log.LogInformation($"{Environment.NewLine}JSON Object in MessageBody: {messageJson}");
            log.LogInformation($"{Environment.NewLine}C# ServiceBus topic trigger function processed message: {messageIncoming.Body}");
            log.LogInformation($"{Environment.NewLine}Frosting Status: {frostyVar}");
            log.LogInformation($"{Environment.NewLine}Frosting Amount: {frostingAmount}");
            log.LogInformation($"{Environment.NewLine}Message Properties: {custProp}");
        }
    }
}
