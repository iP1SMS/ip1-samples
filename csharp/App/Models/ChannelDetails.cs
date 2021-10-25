using System.Collections.Generic;

namespace IP1.Samples.Models
{
    public abstract class ChannelDetails
    {
        public List<string> PendingParticipants { get; set; } = new List<string>();
        public List<string> DeliveredParticipants { get; set; } = new List<string>();
        public List<string> FailedParticipants { get; set; } = new List<string>();

        public abstract SendingChannel Channel { get; }

        public SendingStatus Status { get; set; } = SendingStatus.Ready;
    }
}
