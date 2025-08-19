using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MicroShopping.Web.Utils;

public static class HttpClientExtensions
{
    private static MediaTypeHeaderValue contentType = new MediaTypeHeaderValue("application/json");
    public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
    {
        if(!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Error: {response.StatusCode}, {await response.Content.ReadAsStringAsync()}");
        }
        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        }) ?? throw new InvalidOperationException("Deserialization returned null");
    }
    public static async Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, string requestUri, T value)
    {
        var content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, contentType);
        return await client.PostAsync(requestUri, content).ConfigureAwait(false);
    }
}
