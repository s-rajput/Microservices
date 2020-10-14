# Demo
![](https://github.com/s-rajput/Microservices/blob/master/demo1.gif?raw=true)
# Microservices
 [![.Net Framework](https://img.shields.io/badge/DotNet-3.1_Framework-blue.svg?style=plastic)](https://www.microsoft.com/net/download/dotnet-core/3.1) |[![Node](https://img.shields.io/badge/Node-Js-blue.svg?style=plastic)](https://nodejs.org/en/download/)
 ![GitHub language count](https://img.shields.io/github/languages/count/s-rajput/Microservices.svg) 
 ![GitHub top language](https://img.shields.io/github/languages/top/s-rajput/Microservices.svg) 
 
 #### Front end: Vue JS using TypeScript, Back-end: .Net Core, API Gateway: Ocelot, Identity server: IdentityServer4
> This repository involves a quick demonstration of API Management using Ocelot API Gateway for accessing secure micro-services built using Service-oriented Arhiecture.
> Identity server4 in-memory client credentials is implemented for securing the microservices.

 ### Architecture
 ![](https://github.com/s-rajput/Microservices/blob/master/Architecture2.jpg)
 ![Arhiecture](https://github.com/s-rajput/Microservices/blob/master/architecture1.jpg)
 
 ### Setup

>  Please clone or download the repository from [![github](https://img.shields.io/badge/git-hub-blue.svg?style=plastic)](https://github.com/s-rajput/Microservices) 

 ### Steps to start the app:
 
 #### A. Start the Identity server
 > 1. Open CMD and CD to path: **src\Services\Identity\IdentityServer** 
 > 2. Run **dotnet run**
 > 3. Identity server issues token to be able to access the secure microservice - PetApi
 > 
![](https://github.com/s-rajput/Microservices/blob/master/idp.jpg)

 #### B. Start the Microservices - PetsApi
 > 1. Open CMD and CD to path: **src\Services\Pets\PetsApi** 
 > 2. Run **dotnet run** , browse http://localhost:5003/swagger
 > 3. This is a secure API that connects to external API to fetch results. It return 401 if token is not passed.
 > 
![petsapi](https://github.com/s-rajput/Microservices/blob/master/pets1.jpg)

 #### C. Start the web applications Aggregator
 > 1. Open CMD and CD to path: **src\src\ApiGateways\Aggregator\WebAggregator** 
 > 2. Run **dotnet run** , browse http://localhost:5002/swagger
 > 3. This is an aggregator of services for all web applications.
 > 4. It makes a call to identity server and then adds the bearer authentication token to call local secure microservice
 > 
![AggregatorApis](https://github.com/s-rajput/Microservices/blob/master/AggregatorApis.jpg)

 #### D. Finally start the API Gateway
 > 1. Open CMD and CD to path: **src\ApiGateways\OcelotAPIGateway** 
 > 2. Run **dotnet run** , browse http://localhost:5000/swagger
 > 3. API Gateway re-route requests to aggregator and identity server.
 > 4. API gateway handles throttling, caching, service discovery, load balancing.
 > 
![gatewayApis](https://github.com/s-rajput/Microservices/blob/master/gatewayApis.jpg)

#### E. Now you are ready to run your SPA web client 
 > 1. Open CMD and CD to path: **src\WebApp\SPA_Client** 
 > 2. Install Node modulesCD, Run  **npm install** 
 > 2. Launch the website, Run  **npm run serve** , http://localhost:8085
 > 4. You will see the landing page making a call to the gateway (http://localhost:5000/gateway/web/pets) and displaying results
 
