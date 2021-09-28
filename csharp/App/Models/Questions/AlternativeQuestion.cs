using System.Collections.Generic;

namespace IP1.Samples.Models
{
    public abstract class AlternativeQuestion<T> : Question where T : IQuestionAlternative
    {
        public List<T> Alternatives { get; set; } = new List<T>();
    }
}
