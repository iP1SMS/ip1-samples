using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace IP1.Samples.Models
{
    public abstract class Question
    {
        public string Id { get; set; }

        public string Title { get; set; }
        public string Subtitle { get; set; }

        public bool Required { get; set; }

        public bool Commentable { get; set; }

        public int? Order { get; set; }

        public string Condition { get; set; }

        public Dictionary<string, Dictionary<string, string>> Translations { get; set; } = new Dictionary<string, Dictionary<string, string>>();

        public string Type => Regex.Replace(GetType().Name, @"Question$", string.Empty);
    }
}
