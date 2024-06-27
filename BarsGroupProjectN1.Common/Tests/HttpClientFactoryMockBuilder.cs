using BarsGroupProjectN1.Core.AppSettings;
using Microsoft.Extensions.Options;
using Moq;

namespace BarsGroupProjectN1.Core.Tests;

public class HttpClientFactoryMockBuilder
{
    private readonly Mock<IHttpClientFactory> _mockHttpClientFactory;
    private readonly IOptions<ServicesOptions> _servicesOptions;

    private HttpClientFactoryMockBuilder(IOptions<ServicesOptions> servicesOptions)
    {
        _mockHttpClientFactory = new Mock<IHttpClientFactory>();
        _servicesOptions = servicesOptions;
    }

    public static HttpClientFactoryMockBuilder Create(IOptions<ServicesOptions> servicesOptions)
    {
        return new HttpClientFactoryMockBuilder(servicesOptions);
    }

    public IHttpClientFactory Build() => _mockHttpClientFactory.Object;

    public HttpClientFactoryMockBuilder WithMenuClient()
    {
        _mockHttpClientFactory
            .Setup(x => x.CreateClient(It.Is<string>(x => x == nameof(_servicesOptions.Value.MenuUrl))))
            .Returns(() => new HttpClient
            {
                BaseAddress = new Uri(_servicesOptions.Value.MenuUrl)
            });
        return this;
    }
}