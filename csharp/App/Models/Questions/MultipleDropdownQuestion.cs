using System.Collections.Generic;

namespace IP1.Samples.Models
{
    public class MultipleDropdownQuestion : DropdownQuestion
    {
        public List<string> Rows { get; set; }

        public bool Distinct { get; set; } = false;
    }
}
