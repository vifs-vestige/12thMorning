using System.ComponentModel.DataAnnotations;

namespace TwelfthMorning.Data
{
    public class Blog
    {
        public int Id { get; set; }
        [Required]
        public string Post { get; set; }
        public DateTime DateAdded { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string MainTag { get; set; }
    }
}
