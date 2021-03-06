using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
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

        //Sending SMS
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
                    Reference = textBoxReference.Text,
                    Tags = new List<string> { textBoxTags.Text }
                };

                if (radioButtonDeliveryWindow.IsChecked == true)
                {
                    sms.DeliveryWindows = new List<DeliveryWindow> { new DeliveryWindow { Opens = datePickerDeliveryWindow.SelectedDate.Value.AddHours(12) } };
                }

                HttpResponseMessage response = await client.PostAsJsonAsync("batches", sms);
                await ShowResultAsync(response);
            }
        }

        //Register Sender
        private async void buttonGetAllSenders_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"me/senders");
                await ShowResultAsync(response);
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
                await ShowResultAsync(response);
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
                await ShowResultAsync(response);
            }
        }

        //Reading Batches
        private async void buttonGetAllBatches_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"batches");
                await ShowResultAsync(response);
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
                await ShowResultAsync(response);
            }
        }

        //Reading Messages
        private async void buttonGetBatchMessages_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"batches/{textBoxBatchId.Text}/messages");
                await ShowResultAsync(response);
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
                await ShowResultAsync(response);
            }
        }

        //Conversations
        private async void buttonGetAllMessages_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"conversations/{textBoxParticipant.Text}");
                await ShowResultAsync(response);
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
                await ShowResultAsync(response);
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
                await ShowResultAsync(response);
            }
        }

        //Black List
        private async void buttonGetBlackList_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"blacklist");
                await ShowResultAsync(response);
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
                await ShowResultAsync(response);
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
                await ShowResultAsync(response);
            }
        }

        //Account Management
        private async void buttonOverviewAccount_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"api/me");
                await ShowResultAsync(response);
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
                await ShowResultAsync(response);
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
                await ShowResultAsync(response);
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
                await ShowResultAsync(response);
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
                await ShowResultAsync(response);
            }
        }

        //Manage surveys
        private async void buttonGetAllSurveys_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"surveys");
                await ShowResultAsync(response);
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
                await ShowResultAsync(response);
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
                await ShowResultAsync(response);
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
                await ShowResultAsync(response);
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
                await ShowResultAsync(response);
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
                await ShowResultAsync(response);
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

                HttpResponseMessage response = await client.PutAsync($"surveys/{textBoxSurveyId.Text}", new StringContent(textBoxNewUpdateSurvey.Text, Encoding.UTF8, "application/json"));
                await ShowResultAsync(response);
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

                HttpResponseMessage response = await client.PostAsync($"surveys", new StringContent(textBoxNewUpdateSurvey.Text, Encoding.UTF8, "application/json"));
                await ShowResultAsync(response);
            }
        }

        //Manage questions
        private async void buttonGetAllQuestions_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"surveys/{textBoxSurveyIdQuestion.Text}/questions");
                await ShowResultAsync(response);
            }
        }

        private async void buttonGetQuestion_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"surveys/{textBoxSurveyIdQuestion.Text}/questions/{textBoxQuestionId.Text}");
                await ShowResultAsync(response);
            }
        }

        private async void buttonDeleteQuestion_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.DeleteAsync($"surveys/{textBoxSurveyIdQuestion.Text}/questions/{textBoxQuestionId.Text}");
                await ShowResultAsync(response);
            }
        }

        private async void buttonUpdateQuestion_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.PutAsync($"surveys/{textBoxSurveyIdQuestion.Text}/questions/{textBoxQuestionId.Text}", new StringContent(textBoxNewUpdateQuestion.Text, Encoding.UTF8, "application/json"));
                await ShowResultAsync(response);
            }
        }

        private async void buttonAddQuestion_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.PostAsync($"surveys/{textBoxSurveyIdQuestion.Text}/questions/{textBoxQuestionId.Text}", new StringContent(textBoxNewUpdateQuestion.Text, Encoding.UTF8, "application/json"));
                await ShowResultAsync(response);
            }
        }

        //Get Answers
        private async void buttonGetAllAnswers_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"surveys/{textBoxSurveyIdAnswers.Text}/questions/{textBoxQuestionIdAnswers.Text}/answers");
                await ShowResultAsync(response);
            }
        }

        private async void buttonGetAllParticipants_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"surveys/{textBoxSurveyIdAnswers.Text}/participants");
                await ShowResultAsync(response);
            }
        }

        private async void buttonGetParticipant_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"surveys/{textBoxSurveyIdAnswers.Text}/participants/{textBoxParticipantId.Text}");
                await ShowResultAsync(response);
            }
        }

        //Manage Templates
        private async void buttonGetAllTemplates_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"surveys/templates");
                await ShowResultAsync(response);
            }
        }

        private async void buttonGetTemplate_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"surveys/templates/{textBoxTemplateId.Text}");
                await ShowResultAsync(response);
            }
        }

        private async void buttonDeleteTemplate_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.DeleteAsync($"surveys/templates/{textBoxTemplateId.Text}");
                await ShowResultAsync(response);
            }
        }

        private async void buttonAddTemplate_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.PostAsync($"surveys/templates", new StringContent(textBoxNewTemplate.Text, Encoding.UTF8, "application/json"));
                await ShowResultAsync(response);
            }
        }

        //Manage Links
        private async void buttonGetAllLinks_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"surveys/{textBoxSurveyIdLinks.Text}/links");
                await ShowResultAsync(response);
            }
        }

        private async void buttonGetLink_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"surveys/{textBoxSurveyIdLinks.Text}/links/{textBoxLinksId.Text}");
                await ShowResultAsync(response);
            }
        }

        private async void buttonDeleteLink_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.DeleteAsync($"surveys/{textBoxSurveyIdLinks.Text}/links/{textBoxLinksId.Text}");
                await ShowResultAsync(response);
            }
        }

        private async void buttonAddLink_Click(object sender, RoutedEventArgs e)
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
                    SurveyLink link = JsonSerializer.Deserialize<SurveyLink>(textBoxNewUpdateLink.Text, options);
                    HttpResponseMessage response = await client.PostAsJsonAsync($"surveys/{textBoxSurveyIdLinks.Text}/links", link);
                    await ShowResultAsync(response);
                }
                catch (Exception ex) { MessageBox.Show("please check your inputs \n" + ex.Message); }
            }
        }

        private async void buttonUpdateLink_Click(object sender, RoutedEventArgs e)
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
                    SurveyLink link = JsonSerializer.Deserialize<SurveyLink>(textBoxNewUpdateLink.Text, options);
                    HttpResponseMessage response = await client.PutAsJsonAsync($"surveys/{textBoxSurveyIdLinks.Text}/links/{textBoxLinksId.Text}", link);
                    await ShowResultAsync(response);
                }
                catch (Exception ex) { MessageBox.Show("please check your inputs \n" + ex.Message); }
            }
        }

        //Manage Sendings
        private async void buttonGetAllSendings_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"surveys/{textBoxSurveyIdSendings.Text}/sendings");
                await ShowResultAsync(response);
            }
        }

        private async void buttonGetSending_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"surveys/{textBoxSurveyIdSendings.Text}/sendings/{textBoxSendingId.Text}");
                await ShowResultAsync(response);
            }
        }

        private async void buttonAddSending_Click(object sender, RoutedEventArgs e)
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
                    SendingRequest sending = JsonSerializer.Deserialize<SendingRequest>(textBoxNewSending.Text, options);
                    HttpResponseMessage response = await client.PostAsJsonAsync($"surveys/{textBoxSurveyIdSendings.Text}/sendings", sending);
                    await ShowResultAsync(response);
                }
                catch (Exception ex) { MessageBox.Show("please check your inputs \n" + ex.Message); }
            }
        }

        //Manage 2FA
        private async void buttonCreateSendAuthentication_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://api.ip1sms.com/api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", textBoxAccountId.Text, textBoxBasicApiKey.Text))));

                var authentication = new AuthenticationDTO()
                {
                    Phone = textBoxPhone.Text,
                    From = textBoxFrom.Text,
                    MessageFormat = textBoxMessage.Text,
                    Length = textBoxLength.Text,
                    ExpirationTime = textBoxExpirationTime.Text
                };

                HttpResponseMessage response = await client.PostAsJsonAsync("authentications", authentication);
                await ShowResultAsync(response);
            }
        }

        private async void buttonValidate_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://api.ip1sms.com/api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", textBoxAccountId.Text, textBoxBasicApiKey.Text))));

                var validation = new AuthenticationValidationRequest()
                {
                    Phone = textBoxPhoneValidate.Text,
                    Code = textBoxSubmittedCode.Text,
                };

                HttpResponseMessage response = await client.PostAsJsonAsync("authentications/validate", validation);
                await ShowResultAsync(response);
            }
        }

        //Manage shop
        private async void buttonGetCountries_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://shopapi.ip1sms.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", textBoxAccountId.Text, textBoxBasicApiKey.Text))));

                HttpResponseMessage response = await client.GetAsync("api/countries");
                await ShowResultAsync(response);
            }
        }

        private async void buttonGetMainProducts_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://shopapi.ip1sms.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", textBoxAccountId.Text, textBoxBasicApiKey.Text))));

                HttpResponseMessage response = await client.GetAsync("api/products/main");
                await ShowResultAsync(response);
            }
        }

        private async void buttonGetAccountSummary_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://shopapi.ip1sms.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", textBoxAccountId.Text, textBoxBasicApiKey.Text))));

                HttpResponseMessage response = await client.GetAsync("api/me");
                await ShowResultAsync(response);
            }
        }

        private async void buttonGetAllOrders_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://shopapi.ip1sms.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", textBoxAccountId.Text, textBoxBasicApiKey.Text))));

                HttpResponseMessage response = await client.GetAsync($"api/me/orders");
                await ShowResultAsync(response);
            }
        }

        private async void buttonGetOrder_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://shopapi.ip1sms.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", textBoxAccountId.Text, textBoxBasicApiKey.Text))));

                HttpResponseMessage response = await client.GetAsync($"api/me/orders/{textBoxOrderId.Text}");
                await ShowResultAsync(response);
            }
        }

        private async void buttonPostOrder_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://shopapi.ip1sms.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", textBoxAccountId.Text, textBoxBasicApiKey.Text))));

                HttpResponseMessage response = await client.PostAsync($"api/me/orders", new StringContent(textBoxNewOrder.Text, Encoding.UTF8, "application/json"));
                await ShowResultAsync(response);
            }
        }

        private async void buttonVatValidate_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://shopapi.ip1sms.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"api/organization/vat/{textBoxVatNumber.Text}");
                await ShowResultAsync(response);
            }
        }

        private async void buttonGetExtraProducts_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://shopapi.ip1sms.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"api/products/extra");
                await ShowResultAsync(response);
            }
        }

        private async void buttonGetavailableBalanceProducts_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://shopapi.ip1sms.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"api/products/balance");
                await ShowResultAsync(response);
            }
        }

        private async void buttonGetYourOwnedProducts_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://shopapi.ip1sms.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", textBoxAccountId.Text, textBoxBasicApiKey.Text))));

                HttpResponseMessage response = await client.GetAsync($"api/me/products");
                await ShowResultAsync(response);
            }
        }

        //Manage contacts
        private async void buttonGetAllContacts_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"contacts");
                await ShowResultAsync(response);
            }
        }

        private async void buttonGetFilteredContacts_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"contacts?labels={textBoxFilterLabel.Text}");
                await ShowResultAsync(response);
            }
        }

        private async void buttonGetContact_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"contacts/{textBoxContactId.Text}");
                await ShowResultAsync(response);
            }
        }

        private async void buttonDeleteContact_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.DeleteAsync($"contacts/{textBoxContactId.Text}");
                await ShowResultAsync(response);
            }
        }

        private async void buttonAddContacts_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.PostAsync($"contacts", new StringContent(textBoxNewUpdateContact.Text, Encoding.UTF8, "application/json"));
                await ShowResultAsync(response);
            }
        }

        private async void buttonUpdateContact_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.PutAsync($"contacts/{textBoxContactId.Text}", new StringContent(textBoxNewUpdateContact.Text, Encoding.UTF8, "application/json"));
                await ShowResultAsync(response);
            }
        }

        private async void buttonGetMeta_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.GetAsync($"contacts/meta");
                await ShowResultAsync(response);
            }
        }

        private async void buttonAddNewLabel_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.PostAsync($"labels/{textBoxNewLabel.Text}/contacts", new StringContent(textBoxContactsId.Text, Encoding.UTF8, "application/json"));
                await ShowResultAsync(response);
            }
        }

        private async void buttonDeleteLabel_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.DeleteAsync($"labels/{textBoxLabelName.Text}");
                await ShowResultAsync(response);
            }
        }

        private async void buttonDeleteContactsByLabel_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", textBoxAPIKey.Text);

                HttpResponseMessage response = await client.DeleteAsync($"contacts?labels={textBoxLabelNameDeleteContacts.Text}");
                await ShowResultAsync(response);
            }
        }

        //Printing results on screen
        private async Task ShowResultAsync(HttpResponseMessage response)
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            labelStatus.Content = $"StatusCode: {(int)response.StatusCode} {response.ReasonPhrase}";

            treeViewResponse.Items.Clear();
            treeViewResponse.ProcessJson(await response.Content.ReadAsStringAsync());

            try
            {
                var jsonElement = JsonSerializer.Deserialize<JsonElement>(await response.Content.ReadAsStringAsync());
                textBoxResponse.Text = JsonSerializer.Serialize(jsonElement, options);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                textBoxResponse.Text = await response.Content.ReadAsStringAsync();
            }
        }

        //organizing methods
        private void ListViewItemRegisterSender_Selected(object sender, RoutedEventArgs e)
        {
            unselectAll();
            tabItemRegisterSender.IsSelected = true;
        }

        private void ListViewItemSendingSMS_Selected(object sender, RoutedEventArgs e)
        {
            unselectAll();
            tabItemSendMessage.IsSelected = true;
        }

        private void ListViewItemReadingBatches_Selected(object sender, RoutedEventArgs e)
        {
            unselectAll();
            tabItemReadingBatches.IsSelected = true;
        }

        private void ListViewItemReadingMessages_Selected(object sender, RoutedEventArgs e)
        {
            unselectAll();
            tabItemReadingMessages.IsSelected = true;
        }

        private void ListViewItemConversations_Selected(object sender, RoutedEventArgs e)
        {
            unselectAll();
            tabItemConversations.IsSelected = true;
        }

        private void ListViewItemBlackList_Selected(object sender, RoutedEventArgs e)
        {
            unselectAll();
            tabItemBlackList.IsSelected = true;
        }

        private void ListViewItemAccountManagement_Selected(object sender, RoutedEventArgs e)
        {
            unselectAll();
            tabItemAccountManagement.IsSelected = true;
        }

        private void ListViewItemManageSurveys_Selected(object sender, RoutedEventArgs e)
        {
            unselectAll();
            tabItemGetSurveys.IsSelected = true;
        }

        private void ListViewItemManageQuestions_Selected(object sender, RoutedEventArgs e)
        {
            unselectAll();
            tabItemGetquestions.IsSelected = true;
        }

        private void ListViewItemGetAnswers_Selected(object sender, RoutedEventArgs e)
        {
            unselectAll();
            tabItemGetAnswers.IsSelected = true;
        }

        private void ListViewItemManageTemplates_Selected(object sender, RoutedEventArgs e)
        {
            unselectAll();
            tabItemManageTemplates.IsSelected = true;
        }

        private void ListViewItemManageLinks_Selected(object sender, RoutedEventArgs e)
        {
            unselectAll();
            tabItemManageLinks.IsSelected = true;
        }

        private void ListViewItemManageSendings_Selected(object sender, RoutedEventArgs e)
        {
            unselectAll();
            tabItemManageSendings.IsSelected = true;
        }

        private void ListViewItemManage2FA_Selected(object sender, RoutedEventArgs e)
        {
            unselectAll();
            tabItemManage2FA.IsSelected = true;
        }

        private void ListViewItemShop_Selected(object sender, RoutedEventArgs e)
        {
            unselectAll();
            tabItemManageShop.IsSelected = true;
        }

        private void ListViewItemContacts_Selected(object sender, RoutedEventArgs e)
        {
            unselectAll();
            tabItemManageContacts.IsSelected = true;
        }

        private void unselectAll()
        {
            listViewSurveyApis.SelectedItem = null;
            listViewSmsApis.SelectedItem = null;
            listViewShopApis.SelectedItem = null;
            listViewContactsApis.SelectedItem = null;
        }

        //"go to" methods
        private void buttonLinkPortal_Click(object sender, RoutedEventArgs e)
        {
            var ps = new ProcessStartInfo("https://portal.ip1.net/#/accounts")
            {
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(ps);
        }

        private void buttonLinkCapacity_Click(object sender, RoutedEventArgs e)
        {
            var ps = new ProcessStartInfo("https://capacity.ip1.net/")
            {
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(ps);
        }

        private void buttonLinkSettings_Click(object sender, RoutedEventArgs e)
        {
            var ps = new ProcessStartInfo("https://capacity.ip1.net/en-US/settings/services")
            {
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(ps);
        }

        private void buttonLinkBatches_Click(object sender, RoutedEventArgs e)
        {
            var ps = new ProcessStartInfo("https://capacity.ip1.net/en-US/batch/list")
            {
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(ps);
        }

        private void buttonLinkBlackList_Click(object sender, RoutedEventArgs e)
        {
            var ps = new ProcessStartInfo("https://contacts.ip1.net/en-US/privacy")
            {
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(ps);
        }

        private void buttonLinkSurveys_Click(object sender, RoutedEventArgs e)
        {
            var ps = new ProcessStartInfo("https://onesurvey.ip1.net/")
            {
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(ps);
        }

        private void buttonLinkShop_Click(object sender, RoutedEventArgs e)
        {
            var ps = new ProcessStartInfo("https://shop.ip1sms.com/")
            {
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(ps);
        }

        private void buttonLinkContacts_Click(object sender, RoutedEventArgs e)
        {
            var ps = new ProcessStartInfo("https://contacts.ip1.net/")
            {
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(ps);
        }

        private void buttonLinkDeveloper_Click(object sender, RoutedEventArgs e)
        {
            var ps = new ProcessStartInfo("https://www.ip1sms.com/en/developer/")
            {
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(ps);
        }
    }
}
