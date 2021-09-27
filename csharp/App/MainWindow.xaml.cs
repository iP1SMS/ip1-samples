﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
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
                    Type = Enum.Parse<SmsType>(comboBoxSMSType.Text),
                    Datacoding = Enum.Parse<Datacoding>(comboBoxDatacoding.Text),
                    Priority = Enum.Parse<Priority>(comboBoxPriority.Text),
                    DeliveryWindows = new List<DeliveryWindow> { new DeliveryWindow { Opens = datePickerDeliveryWindow.SelectedDate.Value.AddHours(12) } },
                    Reference = textBoxReference.Text,
                    Tags = new List<string> { textBoxTags.Text }
                };

                HttpResponseMessage response = await client.PostAsJsonAsync("batches", sms);
                ShowResult(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
                else
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + response.ReasonPhrase;
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
                ShowResult(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
                else
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
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
                ShowResult(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
                else
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
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
                ShowResult(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
                else
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
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
                ShowResult(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
                else
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
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
                ShowResult(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
                else
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
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
                ShowResult(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
                else
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
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
                ShowResult(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
                else
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
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
                ShowResult(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
                else
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
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
                ShowResult(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
                else
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
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
                ShowResult(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
                else
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
            }
        }

        private async void buttonGetBlackList_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"blacklist");
                ShowResult(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
                else
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
            }
        }

        private async void buttonAddPhoneNumber_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.PutAsync($"blacklist/{textBoxPhoneNumber.Text}", null);
                ShowResult(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
                else
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
            }
        }

        private async void buttonRemovePhoneNumber_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.DeleteAsync($"blacklist/{textBoxPhoneNumber.Text}");
                ShowResult(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
                else
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
            }
        }

        private async void buttonOverviewAccount_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"api/me");
                ShowResult(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
                else
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
            }
        }

        private async void buttonDetailsAccount_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"api/me/account");
                ShowResult(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
                else
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
            }
        }

        private async void buttonGetAllSubAccounts_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"api/me/children");
                ShowResult(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
                else
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
            }
        }

        private async void buttonAddSubAccount_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                Account sub = new Account()
                {
                    Name = textBoxNewSubAccountName.Text
                };

                HttpResponseMessage response = await client.PostAsJsonAsync($"api/me/children", sub);
                ShowResult(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
                else
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
            }
        }

        private async void buttonEditSubAccount_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                Account child = new Account()
                {
                    ID = textBoxSubAccountID.Text,
                    Name = textBoxSubAccountName.Text
                };

                HttpResponseMessage response = await client.PutAsJsonAsync($"api/me/children/{textBoxSubAccountID.Text}", child);
                ShowResult(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
                else
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
            }
        }

        private async void buttonGetAllSurveys_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"surveys");
                ShowResult(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
                else
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
            }
        }

        private async void buttonGetSummary_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"surveys/summary");
                ShowResult(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
                else
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
            }
        }

        private async void buttonGetPinned_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"surveys/pinned");
                ShowResult(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
                else
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
            }
        }

        private async void buttonGetSurvey_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"surveys/{textBoxSurveyId.Text}");
                ShowResult(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
                else
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
            }
        }

        private async void buttonGetSurveysummary_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"surveys/{textBoxSurveyId.Text}/summary");
                ShowResult(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
                else
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
            }
        }

        private async void buttonDeleteSurvey_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.DeleteAsync($"surveys/{textBoxSurveyId.Text}");
                ShowResult(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
                else
                {
                    labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                }
            }
        }

        private async void buttonUpdateSurvey_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                options.Converters.Add(new JsonStringEnumConverter());
                try
                {
                    Survey survey = JsonSerializer.Deserialize<Survey>(textBoxNewUpdateSurvey.Text, options);
                    HttpResponseMessage response = await client.PutAsJsonAsync($"surveys/{survey.Id}", survey);
                    ShowResult(await response.Content.ReadAsStringAsync());

                    if (response.IsSuccessStatusCode)
                    {
                        labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                    }
                    else
                    {
                        labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                    }
                }
                catch (Exception ex) { MessageBox.Show("please check your inputs \n" + ex.Message); }
            }
        }

        private async void buttonAddSurvey_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                options.Converters.Add(new JsonStringEnumConverter());
                try
                {
                    Survey survey = JsonSerializer.Deserialize<Survey>(textBoxNewUpdateSurvey.Text, options);
                    HttpResponseMessage response = await client.PostAsJsonAsync($"surveys", survey);
                    ShowResult(await response.Content.ReadAsStringAsync());

                    if (response.IsSuccessStatusCode)
                    {
                        labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                    }
                    else
                    {
                        labelStatus.Content = "StatusCode: " + (int)response.StatusCode + " " + response.ReasonPhrase;
                    }
                }
                catch (Exception ex) { MessageBox.Show("please check your inputs \n" + ex.Message); }
            }
        }

        private void ListViewItemRegisterSender_Selected(object sender, RoutedEventArgs e)
        {
            tabItemRegisterSender.IsSelected = true;
            listViewSurveyApis.SelectedItem = null;
        }

        private void ListViewItemSendingSMS_Selected(object sender, RoutedEventArgs e)
        {
            tabItemSendMessage.IsSelected = true;
            listViewSurveyApis.SelectedItem = null;
        }

        private void ListViewItemReadingBatches_Selected(object sender, RoutedEventArgs e)
        {
            tabItemReadingBatches.IsSelected = true;
            listViewSurveyApis.SelectedItem = null;
        }

        private void ListViewItemReadingMessages_Selected(object sender, RoutedEventArgs e)
        {
            tabItemReadingMessages.IsSelected = true;
            listViewSurveyApis.SelectedItem = null;
        }

        private void ListViewItemConversations_Selected(object sender, RoutedEventArgs e)
        {
            tabItemConversations.IsSelected = true;
            listViewSurveyApis.SelectedItem = null;
        }

        private void ListViewItemBlackList_Selected(object sender, RoutedEventArgs e)
        {
            tabItemBlackList.IsSelected = true;
            listViewSurveyApis.SelectedItem = null;
        }

        private void ListViewItemAccountManagement_Selected(object sender, RoutedEventArgs e)
        {
            tabItemAccountManagement.IsSelected = true;
            listViewSurveyApis.SelectedItem = null;
        }

        private void ListViewItemGetSurveys_Selected(object sender, RoutedEventArgs e)
        {
            tabItemGetSurveys.IsSelected = true;
            listViewSurveyApis.SelectedItem = null;
        }

        private void ShowResult(string response)
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            treeViewResponse.Items.Clear();
            treeViewResponse.ProcessJson(response);
            try
            {
                var jsonElement = JsonSerializer.Deserialize<JsonElement>(response);
                textBoxResponse.Text = JsonSerializer.Serialize(jsonElement, options);
            }
            catch
            {
                textBoxResponse.Text = response;
            }
        }
    }
}
