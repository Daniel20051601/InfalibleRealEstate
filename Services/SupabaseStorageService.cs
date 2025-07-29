using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;


namespace InfalibleRealEstate.Services;

public class SupabaseStorageService
{
    private readonly HttpClient _httpClient;
    private readonly string _supabaseUrl;
    private readonly string _supabaseKey;
    private readonly string _bucket;

    public SupabaseStorageService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _supabaseUrl = configuration["Supabase:Url"]!;
        _supabaseKey = configuration["Supabase:ApiKey"]!;
        _bucket = configuration["Supabase:StorageBucket"]!;
    }

    public async Task<List<string>> UploadFiles(IEnumerable<IBrowserFile> files)
    {
        var urls = new List<string>();

        foreach (var file in files)
        {
            var fileName = $"{Guid.NewGuid()}_{file.Name.Replace(" ", "_")}";

            using var stream = file.OpenReadStream(maxAllowedSize: 10485760); // 10MB max
            using var content = new StreamContent(stream);
            content.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

            var request = new HttpRequestMessage(HttpMethod.Post, $"{_supabaseUrl}/storage/v1/object/{_bucket}/{fileName}")
            {
                Content = content
            };

            // Agregar encabezados de autenticación
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
}
