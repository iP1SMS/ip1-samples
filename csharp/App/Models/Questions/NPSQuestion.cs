namespace IP1.Samples.Models
{
    public class NPSQuestion : ValueSelectionQuestion
    {
        public override int Min => 0;
        public override int Max => 10;
        public override int Step => 1;
        public override ValueSelectionVariant? Variant => ValueSelectionVariant.Slider;
    }
}
