using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public bool Completed { get; set; }
    }
}
