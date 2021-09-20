using System;
using System.ComponentModel.DataAnnotations;

namespace IP1.Samples.Models
{
    public class DeliveryWindow
    {
        private DateTime? _opens;
        private DateTime? _closes;

        [DataType(DataType.DateTime)]
        public DateTime? Opens { get => _opens; set => _opens = value ?? _opens ?? DateTime.UtcNow; }

        [DataType(DataType.DateTime)]
        public DateTime? Closes
        {
            get => _closes;
            set
            {
                if (value != null)
                {
                    _closes = value;
                }
                else if (_opens.HasValue)
                {
                    _closes = _opens.Value.AddDays(7);
                }
                else
                {
                    _closes = DateTime.UtcNow.AddDays(7);
                }
            }
        }
    }
}
