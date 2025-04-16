using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using PinFood.Application.Providers;

namespace PinFood.Tests.UnitTests.Application.Providers;

public class FileUrlProviderTests
{
	[Fact]
	public void GenerateFileUrl_ShouldReturnCorrectUrl_WhenHttpContextAndConfigurationAreValid()
	{
		// Arrange
		var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
		var configurationMock = new Mock<IConfiguration>();
		var httpContext = new DefaultHttpContext();
		httpContext.Request.Scheme = "https";
		httpContext.Request.Host = new HostString("localhost:5001");

		httpContextAccessorMock.Setup(a => a.HttpContext).Returns(httpContext);
		configurationMock.Setup(c => c["FileStorage:StaticFileRequestPath"]).Returns("/files");

		var fileUrlProvider = new FileUrlProvider(httpContextAccessorMock.Object, configurationMock.Object);
		var filePath = "sample/image.png";

		// Act
		var result = fileUrlProvider.GenerateFileUrl(filePath);

		// Assert
		result.ShouldBe("https://localhost:5001/files/sample/image.png");
	}

	[Fact]
	public void GenerateFileUrl_ShouldThrowInvalidOperationException_WhenHttpContextIsNull()
	{
		// Arrange
		var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
		var configurationMock = new Mock<IConfiguration>();

		httpContextAccessorMock.Setup(a => a.HttpContext).Returns((HttpContext?)null);
		configurationMock.Setup(c => c["FileStorage:StaticFileRequestPath"]).Returns("/files");

		var fileUrlProvider = new FileUrlProvider(httpContextAccessorMock.Object, configurationMock.Object);
		var filePath = "sample/image.png";

		// Act & Assert
		Should.Throw<InvalidOperationException>(() => fileUrlProvider.GenerateFileUrl(filePath))
			.Message.ShouldBe("HttpContext is not available.");
	}

	[Fact]
	public void GenerateFileUrl_ShouldUseStaticFileRequestPathFromConfiguration()
	{
		// Arrange
		var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
		var configurationMock = new Mock<IConfiguration>();
		var httpContext = new DefaultHttpContext();
		httpContext.Request.Scheme = "http";
		httpContext.Request.Host = new HostString("example.com");

		httpContextAccessorMock.Setup(a => a.HttpContext).Returns(httpContext);
		configurationMock.Setup(c => c["FileStorage:StaticFileRequestPath"]).Returns("/static-files");

		var fileUrlProvider = new FileUrlProvider(httpContextAccessorMock.Object, configurationMock.Object);
		var filePath = "test/document.pdf";

		// Act
		var result = fileUrlProvider.GenerateFileUrl(filePath);

		// Assert
		result.ShouldBe("http://example.com/static-files/test/document.pdf");
	}
}