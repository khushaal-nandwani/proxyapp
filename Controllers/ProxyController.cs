using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
    private async Task<string> MakePostRequest(string url, object information)
    {
        var client = _clientFactory.CreateClient();

        string json = JsonConvert.SerializeObject(information);

        // Create HttpContent from the JSON string
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        var response = await client.PostAsync(url, content);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    [HttpPost("ProxyGet")]
    public async Task<IActionResult> ProxyGet([FromBody] ProxyGetRequest request)
    {
        try
        {
            // Call the helper method to make the POST request
            var response = await MakeGetRequest(request.Url);
            return Ok(response);
        }
        catch (Exception ex)
        {
            // Handle exceptions (e.g., invalid URL, network issues)
            return BadRequest(ex.Message);
        }
    }

    private async Task<string> MakeGetRequest(string url)
    {
        var client = _clientFactory.CreateClient();

        var response = await client.GetAsync(url);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
}

