using System.Collections.Generic;

namespace IP1.Samples.Models
{
    public class SendingRequest
    {
        public List<string> Participants { get; set; } = new List<string>();

        public List<ParticipantRequest> NewParticipants { get; set; } = new List<ParticipantRequest>();

        public List<SendingChannel> Channels { get; set; } = new List<SendingChannel>();

        public Dictionary<string, string> DefaultProperties { get; set; } = new Dictionary<string, string>();

        public SmsDetails Sms { get; set; }
        public EmailDetails Email { get; set; }
    }
}
