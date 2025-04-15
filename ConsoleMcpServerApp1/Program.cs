// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModelContextProtocol.Server;
using System.ComponentModel;

Console.WriteLine("Hello MCP World!");

var builder = Host.CreateEmptyApplicationBuilder(settings: null);

builder.Services
       .AddMcpServer()
       .WithStdioServerTransport()
       .WithToolsFromAssembly();

await builder.Build().RunAsync();

[McpServerToolType]
public static class TimeTools
{
    [McpServerTool, Description("Gets the current time")]
    public static string GetCurrentTime()
    {
        return DateTimeOffset.Now.ToString();
    }

    [McpServerTool, Description("Gets time in specific timezone")]
    public static string GetTimeInTimezone(string timezone)
    {
        try
        {
            var tz = TimeZoneInfo.FindSystemTimeZoneById(timezone);
            return TimeZoneInfo.ConvertTime(DateTimeOffset.Now, tz).ToString();
        }
        catch
        {
            return "Invalid timezone specified";
        }
    }
}