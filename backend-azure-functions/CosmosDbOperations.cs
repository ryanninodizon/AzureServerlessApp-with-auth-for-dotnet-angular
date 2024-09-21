using System.Net;
using Grpc.Net.Client.Configuration;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker.Http;
using Newtonsoft.Json;

namespace Serverless.Api
{
    public static class CosmosDbOperations
    {
        public static async Task<HttpResponseData> CreateAsync(HttpRequestData req, Container container)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            await container.CreateItemAsync(data, new PartitionKey(data.id.ToString()));
            var response = req.CreateResponse(HttpStatusCode.Created);
            await response.WriteStringAsync("New Item Added");
            return response;
        }
        public static async Task<HttpResponseData> DeleteAsync(HttpRequestData req, Container container, string id)
        {
           await container.DeleteItemAsync<dynamic>(id, new PartitionKey(id));
           var response = req.CreateResponse(HttpStatusCode.OK);
           await response.WriteStringAsync("Item was successfully deleted");
           return response;
        }

        public static async Task<HttpResponseData> ReadAsync(HttpRequestData req, Container container, string id)
        {
            try{
                ItemResponse<dynamic> responseItem = await container.ReadItemAsync<dynamic>(id,new PartitionKey(id));
                var response = req.CreateResponse(HttpStatusCode.OK);
                string responseData = JsonConvert.SerializeObject(responseItem.Resource);
                await response.WriteStringAsync(responseData);
                return response;
            }catch(CosmosException e){
                var errorReponse = req.CreateResponse(HttpStatusCode.InternalServerError);
                await errorReponse.WriteStringAsync($"Error Occured: {e.Message}");  
                return errorReponse;
            }
            
        }
        public static async Task<HttpResponseData> ReadAsync(HttpRequestData req, Container container)
        {
            try{
                
                List<dynamic> returnArray = new List<dynamic>();
                FeedIterator<dynamic> feedIterator = container.GetItemQueryIterator<dynamic>();

                while(feedIterator.HasMoreResults){
                    foreach(dynamic item in await feedIterator.ReadNextAsync()){
                        returnArray.Add(item);
                    }
                }
                var response = req.CreateResponse(HttpStatusCode.OK);
                string responseData = JsonConvert.SerializeObject(returnArray);
                await response.WriteStringAsync(responseData);
                return response;  
                

            }catch(CosmosException e){
                var errorReponse = req.CreateResponse(HttpStatusCode.InternalServerError);
                await errorReponse.WriteStringAsync($"Error Occured: {e.Message}");  
                return errorReponse;
            }
        }

        public static async Task<HttpResponseData> UpdateAsync(HttpRequestData req, Container container, string id)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            await container.UpsertItemAsync(data, new PartitionKey(data.id.ToString()));
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync("Item Updated");
            return response;
        }

        
    }
}
