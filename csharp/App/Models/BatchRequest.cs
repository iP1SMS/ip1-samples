using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IP1.Samples.Models
{
    public class BatchRequest
    {
        public string Sender { get; set; }

        public List<string> Recipients { get; set; }

        public string Body { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SmsType Type { get; set; } = SmsType.SMS;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Datacoding Datacoding { get; set; } = Datacoding.UCS;

        public Priority Priority { get; set; } = Priority.Normal;

        public List<DeliveryWindow> DeliveryWindows { get; set; } = new List<DeliveryWindow>();

        public string DeliveryReportUrl { get; set; }

        public string Reference { get; set; }

        public List<string> Tags { get; set; } = new List<string>();
    }
}
