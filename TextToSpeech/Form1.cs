using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace TextToSpeech
{
    public partial class Form1 : Form
    {
        SpeechSynthesizer speech;
        public Form1()
        {
            InitializeComponent();
            speech = new SpeechSynthesizer();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(richTextBox1.Text))
            {
                //speech.SpeakAsync(richTextBox1.Text);
                Speech(richTextBox1.Text);
            }
        }

        private void Speech(string text)
        {
            String result = Task.Run(async () =>
            {
                String payload = text;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("api-key", "2xq3KDzYGVCgtMvV2E82g0Bl6pXYS8LG");
                client.DefaultRequestHeaders.Add("speed", "");
                client.DefaultRequestHeaders.Add("voice", "banmai");


                var response = await client.PostAsync("https://api.fpt.ai/hmi/tts/v5", new StringContent(payload));
                return await response.Content.ReadAsStringAsync();
            }).GetAwaiter().GetResult();

            ResponseModel responseModel = JsonConvert.DeserializeObject<ResponseModel>(result);

            download(responseModel.async);

            Console.WriteLine(result);
            Console.ReadLine();
        }

        private async void download(string uri)
        {

            var destination = Path.Combine(
               Environment.CurrentDirectory,
                   "music.mp3");

            await new WebClient().DownloadFileTaskAsync(
                new Uri(uri),
                destination);
        }
    }
}
