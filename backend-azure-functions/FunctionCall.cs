using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Serverless.Api
{
    public class FunctionCall
    {
        private static readonly CosmosClient cosmosClient = new CosmosClient(CosmosDbConfig.EndpointUri,CosmosDbConfig.PrimaryKey);
        [Function("FunctionCallApi")]
        public async Task<HttpResponseData> 
        CrudCall([HttpTrigger(AuthorizationLevel.Function, "post","put","get", "delete", Route = "items/{id?}")] 
        HttpRequestData req,
        string id)
        {
            Container container= cosmosClient.GetContainer(CosmosDbConfig.DbName, CosmosDbConfig.DbContainerName);

            switch (req.Method){
                case "GET":
                    if(id is null){
                        return await CosmosDbOperations.ReadAsync(req,container);
                    }else{
                        return await CosmosDbOperations.ReadAsync(req,container,id);//get all
                    }
                    
                case "POST":
                    return await CosmosDbOperations.CreateAsync(req,container);
                case "PUT":
                    return await CosmosDbOperations.UpdateAsync(req,container,id);
                case "DELETE":
                    return await CosmosDbOperations.DeleteAsync(req,container,id);
                default:
                    var response = req.CreateResponse(HttpStatusCode.BadRequest);
                    await response.WriteStringAsync("Bad request");
                    return response;
            }
        }
    }
}
