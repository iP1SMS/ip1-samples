using System.Collections.Generic;

namespace IP1.Samples.Models
{
    public abstract class Alternative : IQuestionAlternative
    {
        public string Value { get; set; }

        public bool Commentable { get; set; }

        public int? Order { get; set; }

        public Dictionary<string, Dictionary<string, string>> Translations { get; set; } = new Dictionary<string, Dictionary<string, string>>();
    }
}
