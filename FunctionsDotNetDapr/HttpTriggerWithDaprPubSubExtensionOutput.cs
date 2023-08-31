using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text;
using Microsoft.Azure.Functions.Extensions.Dapr.Core;
using Microsoft.Azure.WebJobs.Extensions.Dapr;

namespace FunctionsDotNetDapr
{
    public static class HttpTriggerWithDaprPubSubExtensionOutput
    {

        private static HttpClient httpClient = new HttpClient();

        [FunctionName("HttpTriggerWithDaprPubSubExtensionOutput")]
        public static void Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "mypubsubpublisher")] HttpRequest req,
             [DaprPublish(PubSubName = "messagebus", Topic = "mytopic1")] out string pubSubEvent,
            ILogger log)
        {
            log.LogInformation("HttpTriggerWithDaprPubSubExtensionOutput processed a request.");

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            pubSubEvent = requestBody;
        }
    }
}
