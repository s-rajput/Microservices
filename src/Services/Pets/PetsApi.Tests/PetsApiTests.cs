using PetsApi.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Linq; 
using Microsoft.Extensions.Options;
using PetsApi.Config;
using PetsApi.Services.Implementations;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PetsApi.Tests
{
    [TestClass]
    public class PetsApiTests
    {

        [TestMethod]
        public async Task Test_LiveHttpClient_Pass()
        {

            //assemble 
            string[] ExpectedPetsFemale = { "Garfield", "Simba", "Tabby" };
            string[] ExpectedPetsMale = { "Garfield", "Jim", "Max", "Tom" };
            //mock configuration
            var configMock = Substitute.For<IOptions<ConfigSettings>>();
            configMock.Value.Returns(new ConfigSettings
            {
                SydneyRepositoryURL = "https://dorsavicodechallenge.azurewebsites.net/Sydney",
                MelbourneRepositoryURL = "https://dorsavicodechallenge.azurewebsites.net/Melbourne"
            });
            //mock service
            var mockService = Substitute.For<Pets>(new HttpClient(), configMock);

            //act
            var Result = await mockService.GetPets();

            //assert
            Assert.IsNotNull(Result);
            Assert.AreEqual(Result.Count, 2);
            Assert.IsTrue(Result.Where(x => x.Gender == "Male").First().Pets.Any(x => ExpectedPetsMale.Contains(x.Name)));
            Assert.IsTrue(Result.Where(x => x.Gender == "Female").First().Pets.Any(x => ExpectedPetsFemale.Contains(x.Name)));
            Console.WriteLine(Result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.PetsCustomException),
             "Invalid operation occured inside pet service.")]
        public async Task Test_Exeception()
        {

            //assemble 

            var configMock = Substitute.For<IOptions<ConfigSettings>>();
            configMock.Value.Returns(new ConfigSettings
            {
                SydneyRepositoryURL = "https://invalid.azurewebsites.net/Sydney",
                MelbourneRepositoryURL = "https://invalid.azurewebsites.net/Melbourne"
            });
            //mock service
            var mockService = Substitute.For<Pets>(new HttpClient(), configMock);

            //act
            var Result = await mockService.GetPets();
            //assert 

            Assert.IsNull(Result); 
            Console.WriteLine(Result);
        }


        [TestMethod]
        public async Task Test_MockHttpClient()
        {

            //assemble 
            var MockedResponse = "[{'name':'Bob','gender':'Male','age':23,'pets':[{'name':'Sandeep','type':'Cat'},{'name':'Rob','type':'Cat'}]}]";
             //mock configuration
            var configMock = Substitute.For<IOptions<ConfigSettings>>(); 
            configMock.Value.Returns(new ConfigSettings
            {
                SydneyRepositoryURL = "https://dorsavicodechallenge.azurewebsites.net/Sydney",
                MelbourneRepositoryURL = "https://dorsavicodechallenge.azurewebsites.net/Melbourne"
            });
            //mock service
            var mockService = Substitute.For<Pets>(MockedHttpClient(MockedResponse), configMock);
            //act
            var Result = await mockService.GetPets();

            //assert
            Assert.IsNotNull(Result);
            Assert.AreEqual(Result.Count,1);  //fake moked response with only males
            Assert.AreEqual(Result[0].Gender, "Male"); 
            Console.WriteLine(Result);
        }

       
        private HttpClient MockedHttpClient(string MockedResponse)
        { 
             var messageHandler = new MockHttpMessageHandler(MockedResponse, HttpStatusCode.OK);
             return new HttpClient(messageHandler); 
        }
  
    }

    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly string _response;
        private readonly HttpStatusCode _statusCode;

        public string Input { get; private set; }
        public int NumberOfCalls { get; private set; }

        public MockHttpMessageHandler(string response, HttpStatusCode statusCode)
        {
            _response = response;
            _statusCode = statusCode;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            NumberOfCalls++;
            if (request.Content != null) // Could be a GET-request without a body
            {
                Input = await request.Content.ReadAsStringAsync();
            }
            return new HttpResponseMessage
            {
                StatusCode = _statusCode,
                Content = new StringContent(_response)
            };
        }
    }


}
