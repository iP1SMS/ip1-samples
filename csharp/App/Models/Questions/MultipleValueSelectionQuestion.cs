using System.Collections.Generic;

namespace IP1.Samples.Models
{
    public class MultipleValueSelectionQuestion : ValueSelectionQuestion
    {
        public List<string> Rows { get; set; } = new List<string>();
    }
}
