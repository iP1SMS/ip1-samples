using System.Collections.Generic;

namespace IP1.Samples.Models
{
    public class SurveyLink
    {
        public string Name { get; set; }
        public ParticipantRequest ParticipantTemplate { get; set; } = new ParticipantRequest();
        public string Id { get; set; }
        public string SurveyId { get; set; }
        public List<string> Participants { get; set; } = new List<string>();
        public string Token { get; set; }
        public string Link { get; set; }
        public string ShortLink { get; set; }

    }
}
