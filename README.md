# Azure Serverless Template using Angular-standalone, Azure Functions App, Cosmos DB and APIM

I built this template project using Angular17-Standalone, Azure Functions, CosmosDB and APIM, along with MSAL (Microsoft Authentication Library) for authentication and authorization. I hope this will serve as a useful starting point for anyone exploring serverless development and the Microsoft Identity platform.

> The integration of MSAL is based on this official MSAL JavaScript template: [msal-angular](https://github.com/AzureAD/microsoft-authentication-library-for-js/tree/dev/lib/msal-angular "msal-angular")

> I created the Angular frontend by following the tutorial on [Angualr.dev](https://angular.dev/tutorials "Angualr.dev") 

> The Photos/mages that are being generated are coming from [Lorem Picsum](https://picsum.photos/ "Lorem Picsum")

![](https://github.com/ryannninodizon/Azure-ServerlessApp-Template-for-Dotnet-Angular/blob/main/banner.png)

# Simple Architecture Diagram 
![](https://github.com/ryannninodizon/Azure-ServerlessApp-Template-for-Dotnet-Angular/blob/main/serverless-application-flow.png)

### Prerequisites
Make sure to install the items listed below.
- [Azure Developer CLI](https://learn.microsoft.com/en-us/azure/developer/azure-developer-cli/install-azd?tabs=winget-windows%2Cbrew-mac%2Cscript-linux&pivots=os-windows "Azure Developer CLI") - For a quick Azure deployment.
- [Node.js with npm (18.17.1+) ](https://nodejs.org/en "Node.js with npm (18.17.1+) ")- for Angular installation and SPA development.
- [Azure SDK Tools](https://azure.github.io/azure-sdk/ "Azure SDK Tools") - For wokring with Azure Functions locally and other any Azure services related stuff.
- [.NET Runtime](https://dotnet.microsoft.com/en-us/ ".NET Runtime")
- [Angular CLI](https://angular.io/cli "Angular CLI")

# How to make this working?
### Update environment values 
You have to get the **ClientId** and **TenantId** from the App Registration page and update this file: 
> frontend-angular/src/environments/environment.dev.ts

```json
 msalConfig: {
        auth: {
            clientId: <client-id>,
            authority: 'https://login.microsoftonline.com/<tenant-id>'
        }
    }
```

watch my YouTube video if you want to know how to get the clientId and TenantId  [How to use the Microsoft Identity Platform with Angular Application](https://youtu.be/QZnX_KXTpfI&t=60s "How to use the Microsoft Identity Platform with Angular Application")    

### Install Angular dependencies and run
```csharp
npm install
```
```csharp
ng start or ng serve
```
Update the **httpRyanInterceptor** to match how you prefer to get an Access Token, which you will then use for Bearer Authorization.
> /frontend-angular/src/app/http-ryan.interceptor.ts

```javascript
//Getting access token from localStorage Session
  let token = localStorage.getItem("<msal.access_token> - this can be different, so I strongly suggest to use the MSAL object to get the correct value"); 
  let secret = token?.split(",")[4].split(":")[1].toString();
  secret = secret?.substring(1, secret.length - 1);
  
  const authToken = secret; 
  // Clone the request and add the authorization header
  const authReq = req.clone({
    setHeaders: {      
      Authorization: `Bearer ${authToken}`
    }
  });
```

### Install CosmosClient dependency

```bash
dotnet add package Microsoft.Azure.Cosmos
```

Update this **CosmosDbConfig** class. Add the necessary values. Get it from Azure portal. 
```csharp
namespace Serverless.Api
{
    public static class CosmosDbConfig{
        public static readonly string PrimaryKey = "<CosmosDb Primary Key>";
        public static readonly string EndpointUri = "<CosmosDb Endpoint URI>";
        public static readonly string DbName = "<your cosmos-db-name>";
        public static readonly string DbContainerName = "your <cosmos-db-container-name>";
    }
}
```

Feel free to watch my YouTube video if you want to see how I created different Azure Services for this **template project**:   [Serverless Backend API using Azure Functions and Cosmos DB](https://youtu.be/D9fWa6KOhHg "Serverless Backend API using Azure Functions and Cosmos DB")
# Azure Deployment
Deploying this template project can be easily done uzing Azure Developer CLI - make sure to install this locally - get it [here](https://learn.microsoft.com/en-us/azure/developer/azure-developer-cli/install-azd?tabs=winget-windows%2Cbrew-mac%2Cscript-linux&pivots=os-windows "here").
```bash
# Make sure to login to your Azure Subscription account by executing this azd command 
azd auth login

# You can still execure this to make sure everything is ok.
azd init

# Provision and deploy to Azure
azd up

# Delete any created servcices/resouces when you don't need it anymore
azd down
```


# Screenshots
#### Welcome page
![](https://github.com/ryannninodizon/msal-angular17-with-listdata/blob/main/Screenshots/welcome-pag.JPG)


#### List page
![](https://github.com/ryannninodizon/msal-angular17-with-listdata/blob/main/Screenshots/list-page.JPG)


