using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;


namespace InfalibleRealEstate.Services;

public class StorageService
{
    private readonly HttpClient _httpClient;
    private readonly string _supabaseUrl;
    private readonly string _supabaseKey;
    private readonly string _bucket;

    public StorageService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _supabaseUrl = configuration["Supabase:Url"]!;
        _supabaseKey = configuration["Supabase:ApiKey"]!;
        _bucket = configuration["Supabase:StorageBucket"]!;
    }

    public async Task<List<string>> UploadFiles(IEnumerable<(string FileName, string ContentType, byte[] Content)> files)
    {
        var urls = new List<string>();

        foreach (var file in files)
        {
            var fileName = $"{Guid.NewGuid()}_{file.FileName.Replace(" ", "_")}";

            using var content = new ByteArrayContent(file.Content);
            content.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

            var request = new HttpRequestMessage(HttpMethod.Post, $"{_supabaseUrl}/storage/v1/object/{_bucket}/{fileName}")
            {
                Content = content
            };

            request.Headers.Add("apikey", _supabaseKey);
            request.Headers.Add("Authorization", $"Bearer {_supabaseKey}");

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var publicUrl = $"{_supabaseUrl}/storage/v1/object/public/{_bucket}/{fileName}";
                urls.Add(publicUrl);
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al subir archivo: {error}");
            }
        }

        return urls;
    }

    public async Task<bool> DeleteFile(string fileUrl)
    {
        if (string.IsNullOrEmpty(fileUrl))
        {
            return false;
        }

        try
        {
            var fileName = Path.GetFileName(new Uri(fileUrl).AbsolutePath);

            var payload = new { prefixes = new[] { fileName } };
            var jsonPayload = JsonSerializer.Serialize(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Delete, $"{_supabaseUrl}/storage/v1/object/{_bucket}")
            {
                Content = content
            };

            request.Headers.Add("apikey", _supabaseKey);
            request.Headers.Add("Authorization", $"Bearer {_supabaseKey}");

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error al eliminar archivo de Supabase: {error}");
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Excepción al eliminar archivo: {ex.Message}");
            return false;
        }
    }
}
