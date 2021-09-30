using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace IP1.Samples.Models
{
    public class EmailDetails : ChannelDetails
    {
        public string Sender { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public List<string> EmailIds { get; set; }

        [JsonIgnore]
        public override SendingChannel Channel => SendingChannel.Email;
    }
}
