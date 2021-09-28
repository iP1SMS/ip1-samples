namespace IP1.Samples.Models
{
    public class SatisfactionIndexQuestion : MultipleValueSelectionQuestion
    {
        public override int Min => 10;
        public override int Max => 100;
        public override int Step => 1;
        public override ValueSelectionVariant? Variant => ValueSelectionVariant.Slider;
    }
}
