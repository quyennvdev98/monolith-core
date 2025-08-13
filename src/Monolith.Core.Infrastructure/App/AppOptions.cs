namespace Monolith.Core.Infrastructure.App;

public class AppOptions
{
    public string AppName { get; set; }
    public string InstanceId { get; init; } = Guid.NewGuid().ToString();
    public string Version { get; set; }
}