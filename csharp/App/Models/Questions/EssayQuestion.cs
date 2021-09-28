namespace IP1.Samples.Models
{
    public class EssayQuestion : TextQuestion
    {
        public int? MaxWords { get; set; }

        public int Rows { get; set; } = 5;

        public int Columns { get; set; } = 20;
    }
}
