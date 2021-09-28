namespace IP1.Samples.Models
{
    public class ThreeEmojiQuestion : ValueSelectionQuestion
    {
        public override int Min => -1;
        public override int Max => 1;
        public override int Step => 1;
        public override ValueSelectionVariant? Variant => null;
    }
}
