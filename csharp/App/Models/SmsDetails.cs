using System.Text.Json.Serialization;

namespace IP1.Samples.Models
{
    public class SmsDetails : ChannelDetails
    {
        public string Sender { get; set; }

        public string Body { get; set; }

        public string BatchId { get; set; }

        [JsonIgnore]
        public override SendingChannel Channel => SendingChannel.Sms;
    }
}
