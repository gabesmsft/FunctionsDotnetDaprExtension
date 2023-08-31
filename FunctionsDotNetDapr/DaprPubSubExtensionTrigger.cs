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
using Microsoft.Azure.WebJobs.Extensions.Dapr;

namespace FunctionsDotNetDapr
{
    public static class DaprPubSubExtensionTrigger
    {
        [FunctionName("DaprPubSubExtensionTrigger")]
        public static void Run(
            [DaprTopicTrigger("messagebus", Topic = "mytopic1")] string subEvent,
            ILogger log)
        {
            log.LogInformation("DaprPubSubExtensionTrigger processed a request.");

            log.LogInformation($"Mytopic1 received a message: {subEvent}.");
        }
    }
}