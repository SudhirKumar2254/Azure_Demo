using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.WindowsAzure.Storage;
using Azure.Data.Tables;

namespace Azure_Demo
{
    [StorageAccount("ClassLevelStorageAppSetting")]
    public static class ValidateRequestAndCheckCircuitStatus
    {
        [FunctionName("ValidateRequestAndCheckCircuitStatus")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log, TableClient tableClient)
        {
            log.LogInformation("Inside ValidateRequestAndCheckCircuitStatus");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = "Welcome - " + name;

            return new OkObjectResult(responseMessage);
        }
       
    }
}
