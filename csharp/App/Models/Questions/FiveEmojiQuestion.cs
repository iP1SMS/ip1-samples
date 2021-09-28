namespace IP1.Samples.Models
{
    public class FiveEmojiQuestion : ValueSelectionQuestion
    {
        public override int Min => -2;
        public override int Max => 2;
        public override int Step => 1;
        public override ValueSelectionVariant? Variant => null;
    }
}
