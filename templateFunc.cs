using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Yomco.Function
{
    public static class templateFunc
    {
        [FunctionName("templateFunc")]
        public static void Run(
            [ServiceBusTrigger("inbound-queue", Connection = "cupcakes_SERVICEBUS")] 
            string myQueueItem, 
            ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
