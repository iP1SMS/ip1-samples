using System.Collections.Generic;

namespace IP1.Samples.Models
{
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
