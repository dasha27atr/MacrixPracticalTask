using System.ComponentModel.DataAnnotations;

namespace MacrixPracticalTask.Models.Models
{
    public class Log
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? Level { get; set; }
        public string? Message { get; set; }
        public string? StackTrace { get; set; }
    }
}
