namespace PApp;
using Newtonsoft.Json.Linq;

public class ProxyRequest
{
    public required string Url { get; set; }
    public required object Body { get; set; }
}
public class ProxyGetRequest
{
    public required string Url { get; set; }
}
