using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using IP1.Samples.Models;
using JSONTreeView;

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
            comboBoxSMSType.SelectedItem = comboBoxItemSMS;
            comboBoxPriority.SelectedItem = comboBoxItemNormal;
            comboBoxDatacoding.SelectedItem = comboBoxItemGSM;
            datePickerDeliveryWindow.SelectedDate = DateTime.Today;
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
                    DeliveryWindows = new List<DeliveryWindow> { new DeliveryWindow { Opens = datePickerDeliveryWindow.SelectedDate.Value.AddHours(12) } },
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

        private async void buttonGetBatchMessages_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"batches/{textBoxBatchId.Text}/messages");
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

        private async void buttonGetSingleMessage_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"batches/{textBoxBatchId.Text}/messages/{textBoxMessageId.Text}");
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

        private async void buttonGetAllMessages_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"conversations/{textBoxParticipant.Text}");
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

        private async void buttonGetOutgoingMessages_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"conversations/{textBoxParticipant.Text}/mt");
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

        private async void buttonGetIncomingMessages_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"conversations/{textBoxParticipant.Text}/mo");
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

        private async void buttonGetBlackList_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"api/blacklist");
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

        private async void buttonAddPhoneNumber_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                var phoneNumber = new IBlacklistEntry();
                phoneNumber.Phone = textBoxPhoneNumber.Text;

                HttpResponseMessage response = await client.PostAsJsonAsync($"api/blacklist", phoneNumber);
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

        private async void buttonRemovePhoneNumber_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.DeleteAsync($"api/blacklist/{textBoxIdNumber.Text}");
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

        private void ListViewItemReadingMessages_Selected(object sender, RoutedEventArgs e)
        {
            tabItemReadingMessages.IsSelected = true;
        }

        private void ListViewItemConversations_Selected(object sender, RoutedEventArgs e)
        {
            tabItemConversations.IsSelected = true;
        }

        private void ListViewItemBlackList_Selected(object sender, RoutedEventArgs e)
        {
            tabItemBlackList.IsSelected = true;
        }
    }
}
