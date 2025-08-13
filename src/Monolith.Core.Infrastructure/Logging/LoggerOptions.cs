
namespace Monolith.Core.Infrastructure.Logging;

public class LoggerOptions
{
    public string Level { get; set; }
    public ConsoleOptions Console { get; set; }
    public SeqOptions Seq { get; set; }
    public IDictionary<string, string> Overrides { get; set; }
    public IEnumerable<string> ExcludePaths { get; set; }
    public IEnumerable<string> ExcludeProperties { get; set; }
    public IDictionary<string, object> Tags { get; set; }
}

public sealed class ConsoleOptions
{
    public bool Enabled { get; set; }
}
public class SeqOptions
{
    public bool Enabled { get; set; }
    public string Url { get; set; }
    public string ApiKey { get; set; }
}