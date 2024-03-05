using System.ComponentModel.DataAnnotations;

namespace ASP_Projects.Models
{
    public class Articles
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please fill out content.")]
        public string Content { get; set; } = "";

        [Required(ErrorMessage = "Please enter a title.")]
        [StringLength(40, ErrorMessage = "Title is too long")]
        public string Title { get; set; } = "";

        [Required(ErrorMessage = "Please write a description.")]
        [StringLength(76, ErrorMessage = "Description is too long")]
        public string Description { get; set; } = "";
    }
}
