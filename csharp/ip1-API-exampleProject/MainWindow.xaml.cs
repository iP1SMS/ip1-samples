using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.Json;
using JSONTreeView;

namespace ip1_API_exampleProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void button_ClickAsync(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBox_API_key.Text);

                var sms = new OutgoingSMS()
                {
                    Sender = textBox_sender.Text,
                    Recipients = new List<string> { textBox_recipients.Text },
                    Body = textBox_body.Text,
                    Type = textBox_type.Text,
                    Datacoding = textBox_datacoding.Text,
                    Priority = int.Parse(textBox_priority.Text),
                    Reference = textBox_reference.Text,
                    Tags = new List<string> { textBox_tags.Text }
                };

                HttpResponseMessage response = await client.PostAsJsonAsync("batches", sms);
                treeView_response.Items.Clear();
                treeView_response.ProcessJson(await response.Content.ReadAsStringAsync());
                label_status.Content = "StatusCode: "+(int)response.StatusCode;
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Sent");
                }
                else
                {
                    Console.WriteLine("Failed, " + response.StatusCode + ": " + await response.Content.ReadAsStringAsync());
                }
            }
    }

    }
    public class OutgoingSMS
    {
        public string Sender { get; set; }
        public List<string> Recipients { get; set; }
        public string Body { get; set; }
        public string Type { get; set; }
        public string Datacoding { get; set; }
        public int Priority { get; set; }
        public string Reference { get; set; }
        public List<string> Tags { get; set; }
    }
}
