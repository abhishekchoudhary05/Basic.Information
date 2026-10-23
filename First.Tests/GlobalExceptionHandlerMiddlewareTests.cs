using System.Text.Json;
using First.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace First.Tests;

public class GlobalExceptionHandlerMiddlewareTests
{
    [Fact]
    public async Task InvokeAsync_ReturnsGenericProblemDetailsWhenNextThrows()
    {
        var middleware = new GlobalExceptionHandlerMiddleware(
            _ => throw new InvalidOperationException("Sensitive internal details"),
            NullLogger<GlobalExceptionHandlerMiddleware>.Instance);
        var context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();

        await middleware.InvokeAsync(context);

        Assert.Equal(StatusCodes.Status500InternalServerError, context.Response.StatusCode);
        Assert.Equal("application/problem+json", context.Response.ContentType);

        context.Response.Body.Position = 0;
        using var document = await JsonDocument.ParseAsync(context.Response.Body);
        var problem = document.RootElement;
        Assert.Equal(500, problem.GetProperty("status").GetInt32());
        Assert.Equal("An unexpected error occurred.", problem.GetProperty("title").GetString());
        Assert.Equal(context.TraceIdentifier, problem.GetProperty("traceId").GetString());
        Assert.DoesNotContain("Sensitive internal details", problem.ToString());
    }

    [Fact]
    public async Task InvokeAsync_RethrowsWhenResponseHasAlreadyStarted()
    {
        var exception = new InvalidOperationException("Failure after response started");
        var middleware = new GlobalExceptionHandlerMiddleware(
            _ => throw exception,
            NullLogger<GlobalExceptionHandlerMiddleware>.Instance);
        var context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();
        var responseFeature = new Mock<IHttpResponseFeature>();
        responseFeature.SetupGet(feature => feature.HasStarted).Returns(true);
        context.Features.Set(responseFeature.Object);

        var thrown = await Assert.ThrowsAsync<InvalidOperationException>(
            () => middleware.InvokeAsync(context));

        Assert.Same(exception, thrown);
    }
}
