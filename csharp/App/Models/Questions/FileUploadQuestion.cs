namespace IP1.Samples.Models
{
    public class FileUploadQuestion : Question
    {
        public string AcceptedTypes { get; set; }

        public int? MaxSizeBytes { get; set; }
    }
}
