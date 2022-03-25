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
            //Pull custom metadata from the message 
            string custProp = string.Join(Environment.NewLine,messageIncoming.ApplicationProperties);
            //Inspect singular custom property
            string frostyVar = (string)messageIncoming.ApplicationProperties["frosted"];
            
            //Convert body from Byte to String and parse string to JSON Node
            JsonNode messageJson = JsonNode.Parse(messageIncoming.Body.ToString());

            //Example of using path to interact with value in JSON Node
            float frostingRequest = ((float)messageJson["LegacyOrder"]["orderSpecs"]["frostingAmount"]);
            
            //Convert to JSON Object for manipulation
            JsonObject jobTest = (JsonObject)messageJson;
            
            //Scope to fulfillment JSON Node in Object.  Use AsObject to expose Add() in correct path location
            //FrostingCannon adds requested amount + randomized variance
            jobTest["valueAddFulfillment"].AsObject().Add("frostingActual",frostingCannon(frostingRequest));
            jobTest["valueAddFulfillment"].AsObject().Add("frostingActualTime",DateTime.UtcNow );

            //Job's done - now put the cupcake back in the hopper

            ServiceBusMessage messageOutgoing = new ServiceBusMessage(messageIncoming);
            messageOutgoing.MessageId = messageIncoming.MessageId;
            messageOutgoing.Body = new BinaryData(jobTest);
            messageOutgoing.ApplicationProperties["frosted"] = "2";
            ServiceBusClient sbClient = new ServiceBusClient(Environment.GetEnvironmentVariable("cupcakes_SERVICEBUS"));
            ServiceBusSender sbSender = sbClient.CreateSender("ttop1");
            sbSender.SendMessageAsync(messageOutgoing);


            
            //Screen outputs
            log.LogInformation($"{Environment.NewLine}***{Environment.NewLine}Generic Operator for message : {messageIncoming.MessageId}");
            //log.LogInformation($"{Environment.NewLine}JSON Object in MessageBody: {messageJson}");
            //log.LogInformation($"{Environment.NewLine}C# ServiceBus topic trigger function processed message: {messageIncoming.Body}");
            log.LogInformation($"{Environment.NewLine}Frosting Status: {frostyVar}");
            log.LogInformation($"{Environment.NewLine}Frosting Amount: {frostingRequest}");
            log.LogInformation($"{Environment.NewLine}Message Properties: {custProp}");
            log.LogInformation($"{Environment.NewLine}Modified Message: {jobTest}");
        }
        public static float frostingCannon(float frostingSpec)
        {
            Random compRand = new Random();
            float toleranceVal = compRand.Next(1,100);
            float appliedVal = 0;
            if(toleranceVal < 6)
            {
                appliedVal = frostingSpec + (toleranceVal/10);
            }
            else
            {
                appliedVal = frostingSpec + (toleranceVal/1000);
            };

            return appliedVal;
        }
    }
    
}
