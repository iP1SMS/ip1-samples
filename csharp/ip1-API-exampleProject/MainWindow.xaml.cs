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
using IP1.Samples.Models;

namespace IP1.Samples
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

        private async void buttonSend_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);
                var sms = new BatchRequest()
                {
                    Sender = textBoxSender.Text,
                    Recipients = new List<string> { textBoxRecipients.Text },
                    Body = textBoxBody.Text,
                    Type = (SmsType)Enum.Parse(typeof(SmsType), comboBoxSMSType.Text),
                    Datacoding = (Datacoding)Enum.Parse(typeof(Datacoding), comboBoxDatacoding.Text),
                    Priority = (Priority)Enum.Parse(typeof(Priority), comboBoxPriority.Text),
                    Reference = textBoxReference.Text,
                    Tags = new List<string> { textBoxTags.Text }
                };

                HttpResponseMessage response = await client.PostAsJsonAsync("batches", sms);
                treeViewResponse.Items.Clear();
                treeViewResponse.ProcessJson(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " Sent";
                }
                else
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " Failed";
                }
            }
        }

        private async void buttonGetAllSenders_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"me/senders");
                treeViewResponse.Items.Clear();
                treeViewResponse.ProcessJson(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " Sent";
                }
                else
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " Failed";
                }
            }
        }

        private async void buttonAddNewSender_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.PutAsync($"me/senders/{textBoxAddNewSender.Text}", null);
                treeViewResponse.Items.Clear();
                treeViewResponse.ProcessJson(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " Sent";
                }
                else
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " Failed";
                }
            }
        }

        private async void buttonDeleteSender_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.DeleteAsync($"me/senders/{textBoxDeleteSender.Text}");
                treeViewResponse.Items.Clear();
                treeViewResponse.ProcessJson(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " Sent";
                }
                else
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " Failed";
                }
            }
        }

        private async void buttonGetAllBatches_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"batches");
                treeViewResponse.Items.Clear();
                treeViewResponse.ProcessJson(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " Sent";
                }
                else
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " Failed";
                }
            }
        }

        private async void buttonGetBatch_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"batches/{textBoxGetBatch.Text}");
                treeViewResponse.Items.Clear();
                treeViewResponse.ProcessJson(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " Sent";
                }
                else
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " Failed";
                }
            }
        }

        private void ListViewItemRegisterSender_Selected(object sender, RoutedEventArgs e)
        {
            tabItemRegisterSender.IsSelected = true;
        }

        private void ListViewItemSendingSMS_Selected(object sender, RoutedEventArgs e)
        {
            tabItemSendMessage.IsSelected = true;
        }

        private void ListViewItemReadingBatches_Selected(object sender, RoutedEventArgs e)
        {
            tabItemReadingBatches.IsSelected = true;
        }
    }
}
