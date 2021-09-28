namespace IP1.Samples.Models
{
    public abstract class MultipleChoiceQuestion<T> : AlternativeQuestion<T> where T : IQuestionAlternative
    {
        public int? Min { get; set; }
        public int? Max { get; set; }
    }
}
