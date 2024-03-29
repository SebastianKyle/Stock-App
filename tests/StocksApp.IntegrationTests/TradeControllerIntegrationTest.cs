using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fizzler.Systems.HtmlAgilityPack;
using FluentAssertions;
using HtmlAgilityPack;
using Xunit;

namespace StocksApp.IntegrationTests
{
    public class TradeControllerIntegrationTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;        

        public TradeControllerIntegrationTest(CustomWebApplicationFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        #region Index

        [Fact]
        public async Task Index_ToReturnView()
        {
            // Arrange
        
            // Act
            HttpResponseMessage responseMessage = await _httpClient.GetAsync("/Trade/Index/MSFT");
                                                                                 
            // Then
            responseMessage.Should().BeSuccessful(); // 2xx code

            string responseBody = await responseMessage.Content.ReadAsStringAsync();

            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(responseBody);
            var document = html.DocumentNode;

            document.QuerySelectorAll(".price").Should().NotBeNull();
        }

        #endregion

    }
}