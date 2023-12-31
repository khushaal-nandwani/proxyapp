using Microsoft.AspNetCore.Mvc;

namespace PApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ProxyController : ControllerBase
{
    private readonly IHttpClientFactory _clientFactory;

    public ProxyController(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    [HttpPost("ProxyPost")]
    public async Task<IActionResult> ProxyPost([FromBody] ProxyRequest request)
    {
        try
        {
            // Call the helper method to make the POST request
            var response = await MakePostRequest(request.Url, request.Body);
            return Ok(response);
        }
        catch (Exception ex)
        {
            // Handle exceptions (e.g., invalid URL, network issues)
            return BadRequest(ex.Message);
        }
    }
    private async Task<string> MakePostRequest(string url, string information)
    {
        var client = _clientFactory.CreateClient();

        var content = new StringContent(information, System.Text.Encoding.UTF8, "application/json");
        var response = await client.PostAsync(url, content);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
}
}

