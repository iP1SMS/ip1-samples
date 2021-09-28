namespace IP1.Samples.Models
{
    public class ValueSelectionQuestion : Question
    {
        public virtual int Min { get; set; }

        public virtual int Max { get; set; }

        public virtual int Step { get; set; }

        public virtual ValueSelectionVariant? Variant { get; set; } = ValueSelectionVariant.Slider;
    }
}
