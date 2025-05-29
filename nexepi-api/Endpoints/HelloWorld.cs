using Microsoft.AspNetCore.Builder;

public static class HelloWorldEndpoint
{
    public static void MapHelloWorld(this WebApplication app)
    {
        app.MapGet("/helloworld", () => "Hello World");
    }
}