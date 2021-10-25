using System.Collections.Generic;

namespace IP1.Samples.Models
{
    public class ParticipantRequest
    {
        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
    }
}
