
namespace Podcast.SpeechService
{
    public class SpeechService
    {
        

        public static async void GetSpeechAsync(String tekst, String filename)        
        {
            string voiceId = "ZKutKtutnlbOxDxkNlhk";
            string url = $"https://api.elevenlabs.io/v1/text-to-speech/{voiceId}";

            using (HttpClient client = new())
            {
                client.DefaultRequestHeaders.Add("xi-api-key", Token.API_KEY);
                client.DefaultRequestHeaders.Add("Accept", "audio/mpeg");

                var requestData = new
                {
                    text = tekst,
                    model_id = "eleven_multilingual_v2",
                    voice_settings = new
                    {
                        stability = 0.5,
                        similarity_boost = 0.5
                    }
                };

                var jsonContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(requestData), 
                    System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    using (var fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        await stream.CopyToAsync(fileStream);
                    }

                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

    }
}
