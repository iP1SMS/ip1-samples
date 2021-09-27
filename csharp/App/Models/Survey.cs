using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Windows;

namespace IP1.Samples
{
    public class Survey
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Header { get; set; }

        public bool ShowQuestionHeader { get; set; } = true;

        public TextAlignment TextAlignment { get; set; } = TextAlignment.Left;

        public string BackgroundColor { get; set; } = "#f7f7f7";

        public string TextColor { get; set; } = "#111";

        public string ButtonColor { get; set; } = "#89b642";

        public string Logotype { get; set; }

        public string BackgroundImage { get; set; }

        public bool UseDisclaimer { get; set; } = false;

        public string CustomDisclaimer { get; set; }

        public string DisclaimerLink { get; set; }

        public string CustomEnding { get; set; }

        public string EndingLink { get; set; }

        public bool MultipleLanguageSupport { get; set; } = false;

        public List<string> SupportedLanguages { get; set; } = new List<string>();

        public string BaseLanguage { get; set; }

        public bool AnonymousAnswers { get; set; } = false;

        public DateTime? EndDate { get; set; }

        public bool Pinned { get; set; } = false;

        public DateTime LastModified { get; set; }

        [JsonIgnore]
        public List<Question> Questions { get; set; } = new List<Question>();

        public Dictionary<string, Dictionary<string, string>> Translations { get; set; } = new Dictionary<string, Dictionary<string, string>>();
    }
}
