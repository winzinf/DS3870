using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ISISGetEmployee
{
    private static class GetEmployee{
        private class Employee
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string CodeName { get; set; }
            public string Position { get; set; }
            public string Status { get; set; }
            public getEmployee(string strFirstName, string strLastName, string strCodeName, string strPosition, string strStatus)
            {
                FirstName = strFirstName;
                LastName = strLastName;
                CodeName = strCodeName;
                Position = strPosition;
                Status = strStatus;
            }
        }
    }
    public static class getEmployee
    {
        [FunctionName("getEmployee")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;
            string responseMessage = "";
            if (name == null)
            {
                responseMessage = "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            }
            else {
                responseMessage = $"Hello, {name}. This HTTP triggered function executed successfully.";
            }
            return new OkObjectResult(responseMessage);
        }
    }
}
