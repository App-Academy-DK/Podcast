using OpenAiService;

namespace Podcast
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnGenerateButtonClicked(object sender, EventArgs e)
        {
            string prompt = "Genfortæl denne tekst og læg vægt på de vigtigste punkter." +
                "Svaret skal kunne oplæses på cirka 2 minutter." +
                "Skriv svaret som talesprog så det kan læses op." +
                "Her er teksten: ```" + 
                Tekst.Text +
                "```";

            string manuskript = await ChatService.promptGpt4Async(prompt, Token.OPEN_AI_KEY);

            string datetime = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            string filename = $"C:\\Users\\ktlh\\Music\\output-{datetime}.mp3";

            try
            {
                SpeechService.SpeechService.GetSpeechAsync(manuskript, filename);
                DisplayAlert("Fil genereret", $"Fil gemt som {filename}", "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Fejl", $"Der skete en fejl: {ex.Message}", "OK");
            }
        }
    }
    
}
