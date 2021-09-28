using System.Collections.Generic;

namespace IP1.Samples.Models
{
    public interface IQuestionAlternative
    {
        string Value { get; set; }
        bool Commentable { get; set; }
        int? Order { get; set; }
        Dictionary<string, Dictionary<string, string>> Translations { get; set; }
    }
}
