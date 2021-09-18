using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IP1.Samples.Models
{
    public class BatchRequest
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(15, MinimumLength = 1)]
        [RegularExpression("^([1-9]{1}[0-9]{2,14})|([a-zA-Z0-9]{3,11})$", ErrorMessage = "Invalid sender")]
        public string Sender { get; set; }

        [Required]
        public List<string> Recipients { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Body { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SmsType Type { get; set; } = SmsType.SMS;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Datacoding Datacoding { get; set; } = Datacoding.UCS;

        public Priority Priority { get; set; } = Priority.Normal;

        [Required(ErrorMessage = "Can not explicitly be set to null")]
        public List<DeliveryWindow> DeliveryWindows { get; set; } = new List<DeliveryWindow>();

        [StringLength(2047)]
        [Url]
        public string DeliveryReportUrl { get; set; }

        [StringLength(40, MinimumLength = 1)]
        public string Reference { get; set; }

        public List<string> Tags { get; set; } = new List<string>();
    }
}
