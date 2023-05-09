using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

public class IndexModel : PageModel
{
    public string DadJoke { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        httpClient.DefaultRequestHeaders.Add("User-Agent", "DadJokeGenerator");

        var responseMessage = await httpClient.GetAsync("https://icanhazdadjoke.com");
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            using var jsonDoc = JsonDocument.Parse(jsonString);
            var rootElement = jsonDoc.RootElement;
            DadJoke = rootElement.GetProperty("joke").GetString() ?? "No joke found.";
        }
        else
        {
            DadJoke = "Error fetching joke.";
        }

        return Page();
    }
}