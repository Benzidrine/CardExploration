using System.ComponentModel.DataAnnotations;
/// <summary>
/// Record Time
/// </summary>

namespace CardExploration.models
{
    public class TimeRecord
    {
        [Key]
        public int TimeRecordId { get; set; }

        [Required]
        [MaxLength(256)]
        public string Time { get; set; }

        [Required]
        [MaxLength(256)]
        public string Process { get; set; }
    }
}