namespace IP1.Samples.Models
{
    public class Sending : SendingRequest
    {
        public Sending()
        {
        }

        public Sending(SendingRequest request)
        {
            Participants = request.Participants;
            NewParticipants = request.NewParticipants;
            Channels = request.Channels;
            DefaultProperties = request.DefaultProperties;
            Sms = request.Sms;
            Email = request.Email;
        }

        public string Id { get; set; }

        public string SurveyId { get; set; }

        public SendingStatus Status { get; set; }
    }
}
